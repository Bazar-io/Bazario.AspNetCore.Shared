using System.Text;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Helpers
{
    internal static class KebabCaseFormater
    {
        public static string ToKebabCase<TMessage>()
        {
            var name = typeof(TMessage).Name;

            var words = name.Select((c, i) => i > 0 && char.IsUpper(c) ? $"-{c}" : c.ToString())
                .Select(w => w.Trim())
                .Where(w => !string.IsNullOrEmpty(w))
                .ToArray();

            var kebabCase = string
                .Join(string.Empty, words)
                .ToLowerInvariant();

            return kebabCase;
        }
    }
}
