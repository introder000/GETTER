using System.Globalization;
using System.Text;

namespace GETTER.Domain.Common
{
    public static class StringNormalizer
    {
        public static string? Normalize(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            var builder = new StringBuilder(input.Length);
            var pendingSpace = false;

            foreach (var rune in input.EnumerateRunes())
            {
                if (Rune.IsWhiteSpace(rune))
                {
                    pendingSpace = builder.Length > 0;
                    continue;
                }

                if (ShouldRemove(rune))
                    continue;

                if (pendingSpace)
                {
                    builder.Append(' ');
                    pendingSpace = false;
                }

                builder.Append(rune.ToString());
            }

            return builder.Length == 0 ? null : builder.ToString().Normalize(NormalizationForm.FormC);
        }

        private static bool ShouldRemove(Rune rune)
        {
            if (rune == Rune.ReplacementChar)   // uszkodzony ciąg znaków
                return true;

            var category = Rune.GetUnicodeCategory(rune);

            return (category is UnicodeCategory.Control
                             or UnicodeCategory.Format
                             or UnicodeCategory.PrivateUse
                             or UnicodeCategory.OtherNotAssigned)
                   || IsEmoji(rune.Value);
        }

        private static bool IsEmoji(int codePoint) => codePoint is
            (>= 0x1F000 and <= 0x1FAFF)    // piktogramy, emotikony, flagi, transport
            or (>= 0x2600 and <= 0x27BF)   // symbole i dingbaty (☀ ☎ ✔ ❤)
            or (>= 0x2B00 and <= 0x2BFF)   // symbole i strzałki (⭐ ⬛)
            or (>= 0x231A and <= 0x23FF)   // symbole techniczne (⌚ ⏰)
            or (>= 0xFE00 and <= 0xFE0F)   // selektory wariantu (kolorowa wersja symbolu)
            or 0x20E3;                     // łącznik "keycap"
    }
}

