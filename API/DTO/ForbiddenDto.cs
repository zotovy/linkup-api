namespace API.DTO {
    public sealed class ForbiddenDto {
        public bool success => false;
        public string error => "forbidden";
    }
}