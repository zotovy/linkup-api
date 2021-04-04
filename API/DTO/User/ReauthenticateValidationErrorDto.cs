using System.Collections.Generic;

namespace API.DTO.User {
    public sealed record ReauthenticateValidationErrorDto {
        public bool success => false;
        public List<ReauthenticateValidationError> errors { get; set; }

        public ReauthenticateValidationErrorDto(List<ReauthenticateValidationError> errors) {
            this.errors = errors;
        }
    }

    public sealed record ReauthenticateValidationError {
        public string path { get; set; }
        public string error { get; set; }
        #nullable enable
        public string? message { get; set; }

        public ReauthenticateValidationError(string path, string error) {
            this.path = path;
            this.error = error; 
        }

        public ReauthenticateValidationError(string path, string error, string? message) {
            this.path = path;
            this.error = error;
            this.message = message;
        }
    }
}