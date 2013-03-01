using System;
using NUnit.Framework;

namespace StringCalculatorKata.Tests
{
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void TestInit()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_returns_0_when_input_is_empty()
        {
            var result = _calculator.Add(string.Empty);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Add_returns_0_when_input_is_null()
        {
            var result = _calculator.Add(null);

            Assert.AreEqual(0, result);
        }

        [TestCase("1", 1)]
        [TestCase("54", 54)]
        public void Add_returns_number_when_input_is_number(string input, int expected)
        {
            var result = _calculator.Add(input);

            Assert.AreEqual(expected, result);
        }

        [TestCase("1,2", 3)]
        [TestCase("1,2\n3", 6)]
        [TestCase("1,1001", 1)]
        [TestCase("//;\n1;2\n3", 6)]
        [TestCase("//[;]\n1;2\n3", 6)]
        [TestCase("//[***][;]\n1;2***3", 6)]
        public void Add_returns_sum_when_input_contains_numbers_separated_by_delimiters(string input, int expected)
        {
            var result = _calculator.Add(input);

            Assert.AreEqual(expected, result);
        }

        [TestCase("-1,2", "negatives not allowed -1")]
        [TestCase("-1,2,-3", "negatives not allowed -1 -3")]
        public void Add_throws_when_input_contains_negative_numbers(string input, string negativesNotAllowed)
        {
            Assert.That(Assert.Throws<ApplicationException>(() => _calculator.Add(input)).Message == negativesNotAllowed);
        }
    }
}
