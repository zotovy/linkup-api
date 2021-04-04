using System.Collections.Generic;
using Domain.ValueObjects.User;
using Metadata.Services.UserMetadata;

namespace Services.User {
    public interface IUserService {
        public IEnumerable<Domain.User> GetUsers();
        public LoginResponse LoginUser(Email email, Password password);
        public SignupResponse SignupUser(SignupRaw user);
        public ReauthenticateResponse ReauthenticateUser(ReauthenticateRequest data);
        #nullable enable
        public Domain.User? GetUser(long id);
        public string SaveUserProfileImage(long id, byte[] image);
        public void ChangeUserAvatarPath(long id, string path);
        public void UpdateUser(Domain.User user);
        public bool IsUserExists(long id);
    }
}