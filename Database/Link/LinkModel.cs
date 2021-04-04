using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.User;
using Domain;
using Domain.ValueObjects;
using Domain.ValueObjects.Link;

namespace Database.Link {

    [Table("Links")]
    public sealed class LinkModel {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public int UserId;
        public UserModel? User { get; set; }

        [Required]
        [Column("Title", TypeName = "varchar(150)")]
        public string Title { get; set; }

        [Required]
        [Column("Subtitle", TypeName = "varchar(200)")]
        public string Subtitle { get; set; }

        [Required]
        [Column("IconName", TypeName = "varchar(200)")]
        public string IconName { get; set; }

        [Required]
        [Column("Href", TypeName = "varchar(1000)")]
        public string Href { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public LinkModel() { }

        public LinkModel(Domain.Link link) {
            Id = link.Id;
            UserId = link.User.Id;
            User = link.User.IsPopulated()
                ? new UserModel(link.User.Model)
                : null;
            Title = link.Title.Value;
            Subtitle = link.Subtitle.Value;
            IconName = link.IconName.Value;
            Href = link.Href.Value;
            CreatedAt = link.CreatedAt;
        }

        public Domain.Link ToDomain() {
            return new() {
                Id = Id,
                User = new Ref<Domain.User>(UserId, User?.ToDomain()),
                Title = new Title(Title),
                Subtitle = new Subtitle(Subtitle),
                IconName = new IconName(IconName),
                Href = new Href(Href),
                CreatedAt = CreatedAt,
            };
        }

    }
}