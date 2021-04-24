using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Link {
    public sealed record Subtitle {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            "^[^~,]*$",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Subtitle(string value) {
            if (!Validator.IsMatch(value) || value.Length > 200) {
                throw new ArgumentException($"{value} is invalid subtitle value.");
            }

            Value = value;
        }
    }
}
