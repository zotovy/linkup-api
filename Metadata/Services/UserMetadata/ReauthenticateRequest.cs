namespace Metadata.Services.UserMetadata {
    public sealed record ReauthenticateRequest {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public long uid { get; set; }
    }
}