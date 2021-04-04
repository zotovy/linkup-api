using System.Collections.Generic;
using Domain;
using Domain.ValueObjects;
using Domain.ValueObjects.Link;
using FluentValidation;

namespace API.DTO.Link {
    public class CreateLinkRequestDto {
        public int userId { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string href { get; set; }
        public string iconName { get; set; }
        
        public ValidateErrorDto? Validate() {
            var validator = new CreateLinkRequestDtoValidator();
            var validation = validator.Validate(this);
            if (validation.IsValid) return null;
            return new ValidateErrorDto(validation.Errors);
        }

        public Domain.Link ToDomain() => new() {
            Href = new Href(href),
            Subtitle = new Subtitle(subtitle),
            Title = new Title(title),
            User = new Ref<Domain.User>(userId),
            IconName = new IconName(iconName),
        };
    }

    public class CreateLinkRequestDtoValidator : AbstractValidator<CreateLinkRequestDto> {
        public CreateLinkRequestDtoValidator() {
            RuleFor(x => x.userId).NotEmpty();
            RuleFor(x => x.title).NotEmpty().Matches(Title.Validator);
            RuleFor(x => x.subtitle).NotEmpty().Matches(Subtitle.Validator);
            RuleFor(x => x.href).NotEmpty().MaximumLength(1000).Matches(Href.EmailHrefValidator).When(x => x.href[0] == 'm');
            RuleFor(x => x.href).NotEmpty().MaximumLength(1000).Matches(Href.HrefValidator).When(x => x.href[0] != 'm');
            RuleFor(x => x.iconName).NotEmpty().MaximumLength(200);
        }
    }

    public static class CreateLinkResponseDtoCreator {

        public static Dictionary<string, dynamic> AuthorNotFound() => new() {
            { "success", false },
            { "error", "no-user-found" }
        };
    }
}