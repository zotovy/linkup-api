using System;
using Domain.ValueObjects.User;
using FluentValidation;

namespace API.DTO.User {
    public sealed class UpdateUserDto {
        public string name { get; set; }
        public string email { get; set; }

#nullable enable
        public ValidateErrorDto? Validate() {
            var validator = new UpdateUserDtoValidator();
            var result = validator.Validate(this);
            if (result.IsValid) return null;
            return new ValidateErrorDto(result.Errors);
        }
        
        public Domain.User ToDomain() {

            return new() {
                Email = email != null ? new Email(email) : null,
                Name = name != null ? new Name(name) : null,
            };
        }
    }

    public sealed class UpdateUserDtoValidator: AbstractValidator<UpdateUserDto> {
        public UpdateUserDtoValidator() {
            RuleFor(x => x.email).EmailAddress().MaximumLength(1024);
            RuleFor(x => x.name).MaximumLength(1024);
        }
    }
}