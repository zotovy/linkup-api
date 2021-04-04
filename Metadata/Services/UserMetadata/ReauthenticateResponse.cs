
using Metadata.Objects;

namespace Metadata.Services.UserMetadata {
    public sealed record ReauthenticateResponse {
        public bool Success { get; set; }
        public long uid { get; set; }
        public AuthTokens Tokens { get; set; }
    }
}
