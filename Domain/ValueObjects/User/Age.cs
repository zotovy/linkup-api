using System;

namespace Domain.ValueObjects.User {
    public sealed record Age {
        public int Value { get; }

        public Age(int value) {
            if (!Validate(value)) {
                throw new ArgumentException($"{value} is invalid Age value.");
            }

            Value = value;
        }

        public static bool Validate(int value) {
            return value >= 1 && value <= 120;
        }
    }
}





