using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Isbn
    {
        public string Value { get; }

        public Isbn(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException("ISBN cannot be empty.");

            var normalized = Normalize(value);

            if (!IsValid(normalized))
                throw new InvalidOperationException("Invalid Isbn.");

            Value = normalized;
        }

        public static string Normalize(string value)
        {
            return value.Replace("-", "").Replace(" ", "");
        }

        public static bool IsValid(string value)
        {
            if (value.Length != 10 && value.Length != 13)
                return false;

            return value.All(char.IsDigit);
        }

        public override string ToString() => Value;

        public bool Equals(Isbn? other)
        {
            return (other is not null && Value ==  other.Value);
        }

        public override bool Equals(object? obj)
        {
            return (obj is Isbn other && Equals(other));
        }

        public override int GetHashCode()
        {  
            return Value.GetHashCode(); 
        }
    }
}
