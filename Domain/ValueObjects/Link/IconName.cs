using System;

namespace Domain.ValueObjects.Link {
    public sealed record IconName {
        public string Value { get; }

        public IconName(string value) {
            
            // really hard to sync with icon pack, so no validator for this obj
            if (value.Length > 200) {
                throw new ArgumentException($"{value} is invalid IconName value.");
            }

            Value = value;
        }
    }
}