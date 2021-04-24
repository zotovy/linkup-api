using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Link {
    public sealed record Title {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            "^[^~,]*$",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Title(string value) {
            if (!Validator.IsMatch(value) || value.Length > 150) {
                throw new ArgumentException($"{value} is invalid title value.");
            }

            Value = value;
        }
    }
}
