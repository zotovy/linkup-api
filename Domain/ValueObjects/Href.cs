using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects {
    public class Href {
        public string Value { get; }

        public static readonly Regex HrefValidator = new(
            @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public static readonly Regex EmailHrefValidator = new(
            @"^mailto:(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)$"
        );

        public static readonly Regex PhoneHrefValidator = new(
            @"^tel:(\+?)(\d{1,16})$"
        );

        public Href() {
        }

        public Href(string value) {
            if (value != null) {
                if ((!HrefValidator.IsMatch(value) && !PhoneHrefValidator.IsMatch(value) && !EmailHrefValidator.IsMatch(value)) || value.Length > 1000) {
                    throw new ArgumentException($"{value} is invalid href value.");
                }
            }

            Value = value;
        }
    }
}
