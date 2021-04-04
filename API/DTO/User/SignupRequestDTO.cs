using Domain.ValueObjects.User;
using Metadata.Services.UserMetadata;

namespace API.DTO.User {
    public class SignupRequestDTO {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        
        public bool Validate() {
            return Name.Validator.IsMatch(name ?? "")
                   && Email.Validator.IsMatch(email ?? "")
                   && Password.Validator.IsMatch(password ?? "")
                   && Username.Validator.IsMatch(username);
        }
    }
}