namespace API.DTO.User {
    public class ChangeThemeErrorRequestDto {
        public bool success => false;
        public string error => "invalid-theme";
    }
}