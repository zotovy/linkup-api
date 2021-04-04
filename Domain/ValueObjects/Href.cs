using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects {
    public class Href {
        public string Value { get; }

        public static readonly Regex HrefValidator = new(
            @"(https?:\/\/.*\.(?:png|jpg|jpeg))",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public static readonly Regex EmailHrefValidator = new(
            @"^(?:mailto):[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"
        );

        public Href() {
        }

        public Href(string value) {
            if (value != null && (!HrefValidator.IsMatch(value) || !EmailHrefValidator.IsMatch(value) || value.Length > 1000)) {
                throw new ArgumentException($"{value} is invalid href value.");
            }

            Value = value;
        }
    }
}