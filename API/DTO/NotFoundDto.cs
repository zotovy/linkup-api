namespace API.DTO {
    public class NotFoundDto {
        public bool success => false;
        public string error => "not-found-error";
    }
}