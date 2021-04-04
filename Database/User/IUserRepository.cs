using System.Collections.Generic;
using Domain.ValueObjects.User;

namespace Database.User {
    public interface IUserRepository {
        public IReadOnlyList<UserModel> GetModels();
        public bool ExistsWithSameEmailAndPassword(Email email, Password password);
        public bool ExistsWithSameEmailAndPassword(Domain.User user);
        public UserModel? FoundWithSameEmailAndPassword(Email email, Password password);
        public void SaveChanges();
        public UserModel Add(Domain.User user);
        public bool CheckEmailUniqueness(Email email);
        public bool CheckUsernameUniqueness(Username username);
        #nullable enable
        public UserModel? GetUserById(long id);
        public void ChangeUserAvatarPath(long id, string path);
        public void UpdateUser(Domain.User user);
    }
}