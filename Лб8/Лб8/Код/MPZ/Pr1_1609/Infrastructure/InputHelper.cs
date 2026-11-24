using System;
using System.Linq;

namespace Pr1_1609.Infrastructure
{
    public static class InputHelper
    {
        public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;

                Console.WriteLine(min == int.MinValue && max == int.MaxValue
                    ? "[Помилка] Введіть ціле число."
                    : $"[Помилка] Введіть ціле число в діапазоні {min}..{max}.");
            }
        }

        public static string ReadString(string prompt, bool allowEmpty = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string value = new string((Console.ReadLine() ?? "").Where(c => !char.IsControl(c)).ToArray()).Trim();
                if (allowEmpty || value.Length > 0) return value;

                Console.WriteLine("[Помилка] Значення не може бути порожнім.");
            }
        }
    }
}