using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects.Link {
    public sealed record Subtitle {
        public string Value { get; }

        public static readonly Regex Validator = new Regex(
            "^([\x00-\x7F]|([\xC2-\xDF]|\xE0[\xA0-\xBF]|\xED[\x80-\x9F]|(|[\xE1-\xEC]|[\xEE-\xEF]|\xF0[\x90-\xBF]|\xF4[\x80-\x8F]|[\xF1-\xF3][\x80-\xBF])[\x80-\xBF])[\x80-\xBF])+$",
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
