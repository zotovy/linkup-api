namespace API.DTO.User {
    public sealed record ReauthenticateResponseDto {
        public TokensDto tokens { get; set; }
        public long uid { get; set; }

        public ReauthenticateResponseDto(TokensDto tokens, long uid) {
            this.tokens = tokens;
            this.uid = uid;
        }
    }
}