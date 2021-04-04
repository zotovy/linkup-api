
using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.User {
    public sealed record Password {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            @"",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Password(string value) {
            if (!Validator.IsMatch(value ?? "") || value.Length > 100) {
                throw new ArgumentException($"{value} is invalid Password value.");
            }

            Value = value;
        }
    }
}

