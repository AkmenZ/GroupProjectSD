using System;
using System.Text;

namespace ProjectManagementApp
{
    public static class InputService
    {
        //Get valid string
        public static string ReadValidString(string prompt)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.WriteLine("\n  Input cannot be empty.");
            }
        }

        //Get valid int
        public static int ReadValidInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("\n  Invalid number. Please try again.");
            }
        }

        //Get valid double
        public static double? ReadValidDouble(string prompt)
        {

            double validDouble;

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                //Allow null input
                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                //If not null, validate and return double
                if (double.TryParse(input, out validDouble))
                {
                    return validDouble;
                }
                Console.WriteLine("\n  Invalid input. Please enter number.");
            }
        }

        //Get valid date & time or null
        public static DateTime? ReadValidDateTime(string prompt)
        {
            DateTime validDateTime;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                //Allow null input
                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }

                //If not null, validate and return date
                if (DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out validDateTime))
                {
                    if (validDateTime >= DateTime.Now.Date)
                    {
                        return validDateTime;
                    }
                }
                Console.WriteLine("\n  Invalid input. Please enter a valid date [dd-MM-yyyy] that's not in the past.");
            }
        }

        //Get password, masking input
        //https://stackoverflow.com/questions/3404421/password-masking-console-application/19770778#19770778
        public static string ReadLineMasked()
        {
            var password = new StringBuilder();
            Console.Write("\n  Enter Password: ");
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            return password.ToString();
        }
    }
}