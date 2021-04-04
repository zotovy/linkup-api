namespace Metadata.Objects {
    public record AuthTokens {
        public AuthTokens(string access, string refresh) {
            Access = access;
            Refresh = refresh;
        }

        public string Access { get; set; }
        public string Refresh { get; set; }
    }
}