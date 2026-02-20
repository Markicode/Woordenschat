using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public readonly struct Optional<T>
    {
        public bool HasValue { get; }
        public T? Value { get; }

        public Optional(T? value)
        {
            HasValue = true;
            Value = value;
        }
    }
}
