using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Database;
using Database.User;
using Domain.ValueObjects;
using Domain.ValueObjects.User;
using Metadata.Exceptions;
using Metadata.Objects;
using Metadata.Services.UserMetadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Data;
using Services.Media;
using Services.Render;

namespace Services.User {
    public class UserServices : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IMediaService _mediaService;
        private readonly IRenderService _renderService;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;


        public UserServices(IUserRepository userRepository, IMediaService mediaService, IRenderService renderService,
            IConfiguration configuration, DatabaseContext context) {
            _userRepository = userRepository;
            _mediaService = mediaService;
            _renderService = renderService;
            _configuration = configuration;
            _context = context;
        }

        public IEnumerable<Domain.User> GetUsers() {
            var models = _userRepository.GetModels();
            return models.Select(UserModel.ToDomain);
        }

        private string _createToken(TokenType type, long id) {
            var claims = new List<Claim> {
                new("uid", id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType
            );

            // Calculate token life time
            var now = DateTime.UtcNow;
            DateTime expires = type == TokenType.Access
                ? now.Add(TimeSpan.FromMinutes(10))
                : now.Add(TimeSpan.FromDays(365));

            var jwt = new JwtSecurityToken(
                "Movieman",
                "Favourite users",
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:secret"])),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public LoginResponse LoginUser(Email email, Password password) {
            // Check is user exists and credentials are fine
            var user = _userRepository.FoundWithSameEmailAndPassword(email, password);
            if (user == null) {
                return new LoginResponse { Success = false };
            }

            // Create tokens for this user
            string access = _createToken(TokenType.Access, user.Id);
            string refresh = _createToken(TokenType.Refresh, user.Id);

            return new LoginResponse {
                Success = true,
                AuthTokens = new AuthTokens(access, refresh),
                UserId = user.Id,
            };
        }

        public SignupResponse SignupUser(SignupRaw raw) {
            // Convert from Raw --> Domain Entity
            var user = new Domain.User {
                Email = new Email(raw.Email),
                Name = new Name(raw.Name),
                Password = new Password(raw.Password),
                CreatedAt = DateTime.Now,
                ProfileImagePath = new ImagePath(),
                Username = new Username(raw.Username),
            };

            // Check uniqueness of email
            if (!_userRepository.CheckEmailUniqueness(user.Email)) {
                return new SignupResponse(false, SignupResponseError.EmailUniqueness);
            }

            // Check uniqueness of username
            if (!_userRepository.CheckUsernameUniqueness(user.Username)) {
                return new SignupResponse(false, SignupResponseError.UsernameUniqueness);
            }

            // Add user to context and save to DB
            var model = _userRepository.Add(user);
            _userRepository.SaveChanges();

            // create tokens
            var access = _createToken(TokenType.Access, model.Id);
            var refresh = _createToken(TokenType.Refresh, model.Id);

            return new SignupResponse(true, model.Id, access, refresh);
        }

        public ReauthenticateResponse ReauthenticateUser(ReauthenticateRequest data) {
            // Validate giving token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:secret"]));

            try {
                var a = tokenHandler.ValidateToken(
                    data.refreshToken,
                    new TokenValidationParameters {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                    },
                    out var validatedToken
                );

                if (a.Claims.ToList()[0].Value != data.uid.ToString()) {
                    throw new ArgumentException($"{validatedToken.Id} != {data.uid.ToString()}");
                }
            } catch {
                return new ReauthenticateResponse {
                    Success = false,
                };
            }

            // Create new tokens
            var access = _createToken(TokenType.Access, data.uid);
            var refresh = _createToken(TokenType.Refresh, data.uid);

            return new ReauthenticateResponse {
                Success = true,
                uid = data.uid,
                Tokens = new AuthTokens(access, refresh),
            };
        }

        public Domain.User? GetUser(long id) => _userRepository.GetUserById(id);

        public string SaveUserProfileImage(long id, byte[] image) {
            var filename = $"{id}.jpg";

            // check is user already have custom avatar
            if (_mediaService.CheckExistUserProfilePicture(filename)) {
                // delete this file if exists
                _mediaService.DeleteUserProfilePicture(filename);
            }

            // save new file
            _mediaService.SaveUserProfilePicture(image, filename);

            return $"{_configuration["Server"]}/static/profile-image/{filename}";
        }

        public void ChangeUserAvatarPath(long id, string path) => _userRepository.ChangeUserAvatarPath(id, path);

        public void UpdateUser(Domain.User user) => _userRepository.UpdateUser(user);

        public bool IsUserExists(long id) => GetUser(id) != null;

        public void ChangeTheme(int id, Theme theme) {
            var model = _context.Users.FirstOrDefault(x => x.Id == id);
            if (model == null) throw new NoUserFoundException(id);

            model.Theme = theme;
            _context.SaveChanges();
            
            _renderService.BuildUserPage(id);
        }
    }
}