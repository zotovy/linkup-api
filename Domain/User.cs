using System;
using System.Collections.Generic;
using Domain.ValueObjects;
using Domain.ValueObjects.User;

namespace Domain {
    public class User {
        public int Id;
        public Name Name;
        public Username Username;
        public Email Email;
        public Password Password;
        public DateTime CreatedAt;
        public ImagePath? ProfileImagePath;
        public List<Ref<Link>> Links;
        public Theme Theme;

        public bool CompareUsingEmailAndPassword(User user) {
            return Email == user.Email && Password == user.Password;
        }

        public bool CompareUsingId(User user) {
            return Id == user.Id;
        }
    }
}