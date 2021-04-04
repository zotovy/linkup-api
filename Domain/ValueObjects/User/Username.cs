using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.User {
    public sealed record Username {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            @"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,29}$",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        public Username(string value) {
            if (!Validator.IsMatch(value)) {
                Console.WriteLine(Validator.Match(value));
                throw new ArgumentException($"{value} is invalid username value.");
            }

            Value = value;
        }
    }
}