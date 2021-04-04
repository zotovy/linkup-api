using Metadata.Objects;

namespace Metadata.Services.UserMetadata {
    public class SignupResponse {
        public bool Success { get; set; }
        public long UserId { get; set; }
        public AuthTokens Tokens { get; set; }
        public SignupResponseError Error { get; set; }

        public SignupResponse(bool success, SignupResponseError error) {
            Success = success;
        }

        public SignupResponse(bool success, long userId, string access, string refresh) {
            Success = success;
            UserId = userId;
            Tokens = new(access, refresh);
        }
    }

    public enum SignupResponseError {
        EmailUniqueness,
        UsernameUniqueness
    }
}