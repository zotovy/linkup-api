using Domain.ValueObjects;
using Domain.ValueObjects.Link;
using FluentValidation;

namespace API.DTO.Link {
    public class UpdateLinkRequestDto {
        public string? title { get; set; }
        public string? subtitle { get; set; }
        public string? href { get; set; }
        public string? iconName { get; set; }

        public ValidateErrorDto? Validate() {
            var validator = new UpdateLinkRequestDtoValidator();
            var validation = validator.Validate(this);
            if (validation.IsValid) return null;
            return new ValidateErrorDto(validation.Errors);
        }

        public Domain.Link ToDomain() => new() {
            Href = href != null ? new Href(href) : null,
            Subtitle = subtitle != null ? new Subtitle(subtitle) : null,
            Title = title != null ? new Title(title) : null,
            IconName = iconName != null ? new IconName(iconName) : null,
        };
    }

    public class UpdateLinkRequestDtoValidator : AbstractValidator<UpdateLinkRequestDto> {
        public UpdateLinkRequestDtoValidator() {
            RuleFor(x => x.title)
                .Matches(Title.Validator)
                .When(x => x.title != null);

            RuleFor(x => x.subtitle)
                .Matches(Subtitle.Validator)
                .When(x => x.subtitle != null);

            RuleFor(x => x.href)
                .MaximumLength(1000)
                .Matches(Href.EmailHrefValidator)
                .When(x => x.href != null && x.href[0] == 'm');

            RuleFor(x => x.href)
                .MaximumLength(1000)
                .Matches(Href.HrefValidator)
                .When(x => x.href != null && x.href[0] != 'm');

            RuleFor(x => x.iconName)
                .MaximumLength(200)
                .When(x => x.iconName != null);
        }
    }
}