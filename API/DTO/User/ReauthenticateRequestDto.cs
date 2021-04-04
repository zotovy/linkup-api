using System.Collections.Generic;
using FluentValidation;

namespace API.DTO.User {
    public sealed record ReauthenticateRequestDto {
        public TokensDto tokens { get; set; }
        public long uid { get; set; }
        
        public ReauthenticateValidationErrorDto Validate() {
            var validator = new ReauthenticateRequestValidator();
            var result = validator.Validate(this);
            if (result.IsValid) return null;

            List<ReauthenticateValidationError> errors = new ();
            foreach (var error in result.Errors) {
                errors.Add(new ReauthenticateValidationError(error.PropertyName, error.ErrorCode, error.ErrorMessage));
            }

            return new ReauthenticateValidationErrorDto(errors);
        }
    }

    public class ReauthenticateRequestValidator : AbstractValidator<ReauthenticateRequestDto> {
        public ReauthenticateRequestValidator() {
            RuleFor(x => x.tokens).NotEmpty();
            RuleFor(x => x.tokens.access).NotEmpty().When(x => x.tokens != null);
            RuleFor(x => x.tokens.refresh).NotEmpty().When(x => x.tokens != null);
            RuleFor(x => x.uid).NotEmpty();
        }
    }
}