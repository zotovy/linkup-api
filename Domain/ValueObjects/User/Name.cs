using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.User {
    public sealed record Name {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            "^[^~,]*$",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Name(string value) {
            if (!Validator.IsMatch(value) || value.Length > 50) {
                throw new ArgumentException($"{value} is invalid name value.");
            }

            Value = value;
        }
    }
}
