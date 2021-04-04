using System.Collections.Generic;
using System.Linq;
using API.DTO.Link;

namespace API.DTO.User {
    public sealed class UserDetailDtoWithLinks {
        public int id { get; set; } 
        public string name { get; set; }
        public string username { get; set; } 
        public string email  { get; set; } 
        public string createdAt  { get; set; } 
        public string? profileImagePath  { get; set; }
        public List<DetailLinkDto> links { get; set; }
        
        public UserDetailDtoWithLinks(Domain.User user) {
            id = user.Id;
            name = user.Name.Value;
            username = user.Username.Value;
            email = user.Email.Value;
            createdAt = user.CreatedAt.ToUniversalTime().ToString("O");
            profileImagePath = user.ProfileImagePath?.Value;
            links = user.Links.Select(x => new DetailLinkDto(x.Model)).ToList();
        }
    }
    
    public sealed class UserDetailDto {
        public int id { get; set; } 
        public string name { get; set; }
        public string username { get; set; } 
        public string email  { get; set; } 
        public string createdAt  { get; set; } 
        public string? profileImagePath  { get; set; }
        public List<int> links { get; set; }
        
        public UserDetailDto(Domain.User user) {
            id = user.Id;
            name = user.Name.Value;
            username = user.Username.Value;
            email = user.Email.Value;
            createdAt = user.CreatedAt.ToUniversalTime().ToString("O");
            profileImagePath = user.ProfileImagePath?.Value;
            links = user.Links.Select(x => x.Id).ToList();
        }
    }
}