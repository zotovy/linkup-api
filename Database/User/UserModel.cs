using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Database.Link;
using Domain;
using Domain.ValueObjects;
using Domain.ValueObjects.User;

namespace Database.User {

    [Table("Users")]
    public sealed record UserModel {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;

        [Required]
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column("Username", TypeName = "varchar(30)")]
        public string Username { get; set; }

        [Required]
        [Column("Email", TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Required]
        [Column("Password", TypeName = "varchar(100)")]
        public string Password { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column("ProfileImagePath", TypeName = "varchar(255)")]
        public string? ProfileImagePath { get; set; }

        [Required]
        public List<int> LinkIds { get; set; }

        public List<LinkModel>? Links { get; set; }

        public UserModel() { }

        public UserModel(Domain.User user) {
            Id = user.Id;
            Name = user.Name.Value;
            Email = user.Email.Value;
            Password = user.Password.Value;
            CreatedAt = user.CreatedAt;
            ProfileImagePath = user.ProfileImagePath?.Value;
            Username = user.Username.Value;
            Links = user.Links?
                .Select(x => x.Model == null ? new LinkModel(x.Model) : null)
                .Where(x => x != null)
                .ToList() ?? new List<LinkModel>();
            LinkIds = user.Links == null
                ? new List<int>()
                : user.Links.Select(x => x.Id).ToList();
        }

        public static Domain.User ToDomain(UserModel model) {
            return new() {
                Id = model.Id,
                Email = new Email(model.Email),
                Password = new Password(model.Password),
                Name = new Name(model.Name),
                CreatedAt = model.CreatedAt,
                Username = new Username(model.Username),
                ProfileImagePath = model.ProfileImagePath != null
                    ? new ImagePath(model.ProfileImagePath)
                    : null,
                Links = model.Links == null | model.Links?.Count == 0
                    ? model.LinkIds.Select(x => new Ref<Domain.Link>(x)).ToList()
                    : model.Links.Select(x => new Ref<Domain.Link>(x.Id, x.ToDomain())).ToList(),
            };
        }

        public Domain.User ToDomain() {
            return ToDomain(this);
        }

        public bool CompareUsingEmailAndPassword(UserModel model) {
            return Email == model.Email && Password == model.Password;
        }

        public bool CompareUsingEmailAndPassword(Domain.User model) {
            return Email == model.Email.Value && Password == model.Password.Value;
        }

        public bool CompareUsingEmailAndPassword(Email email, Password password) {
            return Email == email.Value && Password == password.Value;
        }

        public void UseDataFrom(Domain.User user) {
            // this method will compare only email and name
            if (user.Email != null && user.Email.Value != Email) Email = user.Email.Value;
            if (user.Name != null && user.Name.Value != Name) Name = user.Name.Value;
        }
    }
}