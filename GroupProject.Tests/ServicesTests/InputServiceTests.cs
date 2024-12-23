using System;
using System.Text;

namespace ProjectManagementApp
{
    public static class InputService
    {
        
        // This is a delegate that takes no parameters and returns a string - Mocking Console
        public static Func<string> ReadLine = Console.ReadLine;
        public static Func<ConsoleKeyInfo> ReadKey = Console.ReadKey;

        public static string ReadValidString(string prompt)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.WriteLine("\n  Input cannot be empty.");
            }
        }

        public static int ReadValidInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("\n  Invalid number. Please try again.");
            }
        }

        public static double ReadValidDouble(string prompt)
        {
            double validDouble;
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(ReadLine(), out validDouble))
                {
                    return validDouble;
                }
                Console.WriteLine("\n  Invalid input. Please enter number.");
            }
        }

        public static DateTime ReadValidDateTime(string prompt)
        {
            DateTime validDateTime;
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParseExact(ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out validDateTime))
                {
                    if (validDateTime >= DateTime.Now.Date)
                    {
                        return validDateTime;
                    }
                }
                Console.WriteLine("\n  Invalid input. Please enter a valid date [yyyy-mm-dd] that's not in the past.");
            }
        }

        public static string ReadLineMasked()
        {
            var password = new StringBuilder();
            Console.Write("\n  Enter Password: ");
            while (true)
            {
                var key = ReadKey();
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
