using Application.Common;
using System.Text.Json;

namespace WebApi.Helpers
{
    public static class PatchParsingHelper
    {
        public static Optional<T?> ParseOptional<T>(JsonElement? element, Func<JsonElement, T?> parser)
        {
            if (!element.HasValue)
                return default;

            var el = element.Value;

            if (el.ValueKind == JsonValueKind.Null)
                return new Optional<T?>(default);

            return new Optional<T?>(parser(el));
        }
    }
}
