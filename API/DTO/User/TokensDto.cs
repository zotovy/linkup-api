namespace API.DTO.User {
    public sealed record TokensDto {
        public string access { get; set; }
        public string refresh { get; set; }
    }
}