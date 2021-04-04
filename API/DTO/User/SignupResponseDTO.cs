using Metadata.Objects;

namespace API.DTO.User {
    public class SignupResponseDTO {
        public bool success { get; set; }
        public long uid { get; set; }
        public TokensDto tokens { get; set; }

        public SignupResponseDTO(bool success, long userId, AuthTokens tokens) {
            this.success = success;
            this.uid = userId;
            this.tokens = new() {
                access = tokens.Access,
                refresh = tokens.Refresh,
            };
        }

        public static BadRequestSignupResponseDTO BadRequest() => new ();
        public static EmailUniquenessErrorSignupResponseDTO EmailError() => new ();
        public static UsernameUniquenessErrorSignupResponseDTO UsernameError() => new ();
    }

    public sealed class BadRequestSignupResponseDTO {
        public bool success => false;
        public string error => "validation-error";
    }
    
    public sealed class EmailUniquenessErrorSignupResponseDTO {
        public bool success => false;
        public string error => "email-already-exists-error";
    }
    
    public sealed class UsernameUniquenessErrorSignupResponseDTO {
        public bool success => false;
        public string error => "username-already-exists-error";
    }
}
