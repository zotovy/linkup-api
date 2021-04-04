using System;

namespace API.DTO.User {
    public sealed record LoginRequestDTO {
        public string email { get; set; }
        public string password { get; set; }
    }
}