using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculatorKata
{
    public class Calculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            var customSeparatorsDefinition = GetCustomSeparatorsDefinition(input);
            var numbersString = GetNumbersString(input, customSeparatorsDefinition);

            return Add(customSeparatorsDefinition, numbersString);
        }

        private static int Add(string customSeparatorsDefinition, string numbersString)
        {
            var numbers = GetNumbers(customSeparatorsDefinition, numbersString);

            var negativeNumbers = new StringBuilder();
            var sum = numbers.Sum(number => ProcessNumber(number, negativeNumbers));

            if (negativeNumbers.Length > 0)
                throw new ApplicationException("negatives not allowed" + negativeNumbers);

            return sum;
        }

        private static IEnumerable<string> GetNumbers(string customSeparatorsDefinition, string numbersString)
        {
            var separators = GetSeparators(customSeparatorsDefinition);

            return numbersString.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        private static int ProcessNumber(string number, StringBuilder negativeNumbers)
        {
            var value = Convert.ToInt32(number);

            if (value < 0)
                negativeNumbers.Append(" " + number);
            
            if (value > 1000)
                value = 0;

            return value;
        }

        private static string GetNumbersString(string input, string customSeparatorsDefinition)
        {
            if (!string.IsNullOrEmpty(customSeparatorsDefinition))
                return input.Replace(customSeparatorsDefinition, string.Empty);
            
            return input;
        }

        private static string GetCustomSeparatorsDefinition(string input)
        {
            if (input.Contains("//"))
                return input.Substring(0, input.IndexOf('\n') + 1);
            
            return string.Empty;
        }

        private static string[] GetSeparators(string customSeparatorsDefinition)
        {
            var separators = new List<string> {",", "\n"};

            if (!string.IsNullOrEmpty(customSeparatorsDefinition))
                separators.AddRange(GetCustomSeparators(customSeparatorsDefinition));

            return separators.ToArray();
        }

        private static IEnumerable<string> GetCustomSeparators(string customSeparatorsDefinition)
        {
            var cleanDefinition = customSeparatorsDefinition.Trim('/', '[', ']', '\n');

            return  cleanDefinition.Split(new[] {"]["}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}