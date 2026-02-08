using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Tests.Helpers
{
    public static class TestIsbn
    {
        private static int _counter = 0;

        public static string Next()
        {
            var next = Interlocked.Increment(ref _counter);
            return $"9780000000{next:D3}";
        }
    }
}
