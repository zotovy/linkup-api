namespace API.DTO.User {
    public class UserTileDTO {
        public long id { get; init; }
        public string name { get; init; }
        public string email { get; init; }
        public string profileImagePath { get; init; }

        public UserTileDTO(Domain.User user) {
            id = user.Id;
            name = user.Name.Value;
            email = user.Email.Value;
            profileImagePath = user.ProfileImagePath.Value;
        }
    }
}
