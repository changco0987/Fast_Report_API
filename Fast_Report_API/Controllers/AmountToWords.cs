namespace Fast_Report_API.Controllers
{
    public class AmountToWords
    {
        private static string[] units = {
        "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"
        };

        private static string[] teens = {
        "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
        "Seventeen", "Eighteen", "Nineteen"
        };

        private static string[] tens = {
        "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };

        private static string[] suffixes = {
        "", "Thousand", "Million", "Billion", "Trillion", "Quadrillion"
        };

        public static string ConvertAmountToWords(double amount)
        {
            long amount_int = (long)amount;
            int amount_dec = (int)Math.Round((amount - (double)amount_int) * 100);

            string words = "";

            if (amount_int == 0)
            {
                words = "Zero";
            }
            else
            {
                int count = 0;
                while (amount_int > 0)
                {
                    if (amount_int % 1000 != 0)
                    {
                        words = ConvertChunk((int)(amount_int % 1000)) + " " + suffixes[count] + " " + words;
                    }
                    amount_int /= 1000;
                    count++;
                }
            }

            if (amount_dec > 0)
            {
                words += "& " + amount_dec + "/100";
            }

            return words.Trim();
        }

        private static string ConvertTensUnits(int number)
        {
            if (number < 10)
            {
                return units[number];
            }
            else if (number < 20)
            {
                return teens[number - 10];
            }
            else
            {
                return tens[number / 10] + ((number % 10 > 0) ? " " + units[number % 10] : "");
            }
        }

        private static string ConvertChunk(int number)
        {
            string chunk = "";

            if (number >= 100)
            {
                chunk += units[number / 100] + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (chunk != "")
                {
                    chunk += "and ";
                }

                chunk += ConvertTensUnits(number);
            }

            return chunk;
        }
    }


}
