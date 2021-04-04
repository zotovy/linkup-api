using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore;

namespace Database.User {
    public class UserRepository : IUserRepository {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) {
            _context = context;
        }

        public IReadOnlyList<UserModel> GetModels() {
            return _context.Users.ToList();
        }

        public UserModel? FoundWithSameEmailAndPassword(Email userEmail, Password userPassword) {
            var email = userEmail.Value;
            var password = userPassword.Value;

            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool ExistsWithSameEmailAndPassword(Email email, Password password) {
            return FoundWithSameEmailAndPassword(email, password) != null;
        }

        public bool ExistsWithSameEmailAndPassword(Domain.User user) {
            return FoundWithSameEmailAndPassword(user.Email, user.Password) != null;
        }

        public void SaveChanges() => _context.SaveChanges();

        public UserModel Add(Domain.User user) {
            var model = new UserModel(user);
            _context.Add(model);
            return model;
        }

        public bool CheckEmailUniqueness(Email email) {
            var founded = _context.Users
                .FirstOrDefault(u => u.Email == email.Value);
            return founded == null;
        }

        public bool CheckUsernameUniqueness(Username username) {
            var founded = _context.Users
                .FirstOrDefault(u => u.Username == username.Value);
            return founded == null;
        }

        public Domain.User? GetUserById(long id) {
            var model = _context.Users
                .Include(x => x.Links)
                .FirstOrDefault(u => u.Id == id);

            if (model == null) return null;
            model.Links?.ForEach(x => x.User = null);
            return model.ToDomain();
        }

        public void ChangeUserAvatarPath(long id, string path) {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            // if no user found --> throw ArgumentException
            if (user == null) throw new ArgumentException("Invalid id");

            // Change profile image path & save
            user.ProfileImagePath = path;
            _context.SaveChanges();
        }

        public void UpdateUser(Domain.User user) {
            var model = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            // if no user found --> throw ArgumentException
            if (model == null) throw new ArgumentException("Invalid id");

            try {
                // Compare data and save changes
                model.UseDataFrom(user);
                _context.SaveChanges();
            } catch (DbUpdateException e) {
                throw new ArgumentException("Email already in used");
            }
        }
    }
}