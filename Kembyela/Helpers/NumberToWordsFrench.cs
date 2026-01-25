using System;
using System.Text;

namespace Kembyela.Helpers
{
    public static class NumberToWordsFrench
    {
        private static readonly string[] Units =
        {
            "zéro", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf",
            "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf"
        };

        private static readonly string[] Tens =
        {
            "", "", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante-dix", "quatre-vingt", "quatre-vingt-dix"
        };

        public static string ConvertToWords(decimal number)
        {
            if (number < 0)
                return "moins " + ConvertToWords(-number);

            if (number == 0)
                return "zéro dinar tunisien";

            try
            {
                long integerPart = (long)Math.Truncate(number);
                int decimalPart = (int)Math.Round((number - integerPart) * 1000); // Pour les millimes

                string result = ConvertIntegerToWords(integerPart) + " dinar" + (integerPart > 1 ? "s" : "") + " tunisien" + (integerPart > 1 ? "s" : "");

                if (decimalPart > 0)
                {
                    result += " et " + ConvertIntegerToWords(decimalPart) + " millime" + (decimalPart > 1 ? "s" : "");
                }

                // Capitaliser la première lettre
                if (!string.IsNullOrEmpty(result))
                {
                    result = char.ToUpper(result[0]) + result.Substring(1);
                }

                return result;
            }
            catch (Exception)
            {
                return "Montant non convertible";
            }
        }

        private static string ConvertIntegerToWords(long number)
        {
            if (number < 0)
                return "moins " + ConvertIntegerToWords(-number);

            if (number < 20)
                return Units[number];

            if (number < 100)
            {
                int tensDigit = (int)(number / 10);
                int unitsDigit = (int)(number % 10);
                string result = Tens[tensDigit];

                if (unitsDigit > 0)
                {
                    if (tensDigit == 7 || tensDigit == 9) // soixante-dix, quatre-vingt-dix
                    {
                        result += "-" + Units[unitsDigit + 10];
                    }
                    else if (tensDigit == 1) // dix, onze, douze...
                    {
                        result = Units[unitsDigit + 10];
                    }
                    else if (unitsDigit == 1 && tensDigit != 8) // vingt et un, trente et un...
                    {
                        result += " et un";
                    }
                    else
                    {
                        result += "-" + Units[unitsDigit];
                    }
                }
                else if (tensDigit == 8) // quatre-vingts
                {
                    result += "s";
                }

                return result;
            }

            if (number < 1000)
            {
                int hundreds = (int)(number / 100);
                long remainder = number % 100;
                string result = hundreds == 1 ? "cent" : Units[hundreds] + " cent";

                if (remainder > 0)
                {
                    result += " " + ConvertIntegerToWords(remainder);
                }
                else if (hundreds > 1)
                {
                    result += "s";
                }

                return result;
            }

            if (number < 1000000)
            {
                int thousands = (int)(number / 1000);
                long remainder = number % 1000;
                string result = thousands == 1 ? "mille" : ConvertIntegerToWords(thousands) + " mille";

                if (remainder > 0)
                {
                    result += " " + ConvertIntegerToWords(remainder);
                }

                return result;
            }

            if (number < 1000000000)
            {
                int millions = (int)(number / 1000000);
                long remainder = number % 1000000;
                string result = ConvertIntegerToWords(millions) + " million" + (millions > 1 ? "s" : "");

                if (remainder > 0)
                {
                    result += " " + ConvertIntegerToWords(remainder);
                }

                return result;
            }

            return "nombre trop grand";
        }
    }
}