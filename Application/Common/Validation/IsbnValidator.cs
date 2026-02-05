using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Validation
{
    public static class IsbnHelper
    {
        public static string? Normalize(string? isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return null;
            return isbn.Replace("-", "").Replace(" ", "");
        }

        public static bool IsValid(string? isbn)
        {
           var normalizedIsbn = Normalize(isbn);

           if (normalizedIsbn == null)
            return false;

           if (normalizedIsbn.Length != 10 && normalizedIsbn.Length != 13)
                return false;

           if (!normalizedIsbn.All(char.IsDigit))
                return false;

           return true;


        }
    }
}
