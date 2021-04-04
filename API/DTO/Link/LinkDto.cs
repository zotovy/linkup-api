using API.DTO.User;

namespace API.DTO.Link {
    public class DetailLinkDto {
        public int id { get; set; }
        public int user { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string iconName { get; set; }
        public string href { get; set; }
        public string createdAt { get; set; }

        public DetailLinkDto(Domain.Link link) {
            id = link.Id;
            user = link.User.Id;
            title = link.Title.Value;
            subtitle = link.Subtitle.Value;
            iconName = link.IconName.Value;
            href = link.Href.Value;
            createdAt = link.CreatedAt.ToUniversalTime().ToString("O");
        }
    }
    
    public class DetailLinkDtoWithUser {
        public int id { get; set; }
        public UserTileDTO user { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string iconName { get; set; }
        public string href { get; set; }
        public string createdAt { get; set; }

        public DetailLinkDtoWithUser(Domain.Link link) {
            id = link.Id;
            user = new UserTileDTO(link.User.Model);
            title = link.Title.Value;
            subtitle = link.Subtitle.Value;
            iconName = link.IconName.Value;
            href = link.Href.Value;
            createdAt = link.CreatedAt.ToUniversalTime().ToString("O");
        }
    }
}