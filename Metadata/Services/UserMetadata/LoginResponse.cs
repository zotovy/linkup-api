using Metadata.Objects;

namespace Metadata.Services.UserMetadata {
    public sealed record LoginResponse {
        public bool Success;
        public AuthTokens AuthTokens;
        public long UserId;
    }
}