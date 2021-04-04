using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects {
    public sealed record ImagePath {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            @"(https?:\/\/.*\.(?:png|jpg|jpeg))",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public ImagePath() { }

        public ImagePath(string value) {
            if (value != null && !Validator.IsMatch(value)) {
                throw new ArgumentException($"{value} is invalid ImagePath value.");
            }

            Value = value;
        }
    }
}