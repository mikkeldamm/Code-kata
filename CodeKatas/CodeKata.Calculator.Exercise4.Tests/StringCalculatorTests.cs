using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeKata.Calculator.Exercise4.Tests
{
    [TestClass]
    public class StringCalculatorTests
    {
        private StringCalculator _stringCalculator;

        [TestInitialize]
        public void Setup()
        {
            _stringCalculator = new StringCalculator();
        }

        //--An empty string returns zero
        //--A single number returns the value
        //--Two numbers, comma delimited, returns the sum
        //--Two numbers, newline delimited, returns the sum
        //--Three numbers, delimited either way, returns the sum
        //--Negative numbers throw an exception
        //--Numbers greater than 1000 are ignored
        //A single char delimiter can be defined on the first line (e.g. //# for a ‘#’ as the delimiter)
        //A multi char delimiter can be defined on the first line (e.g. //[###] for ‘###’ as the delimiter)

        [TestMethod, TestCategory("CI")]
        public void Calculate_EmptyStringInput_ReturnsZero()
        {
            // Arrange
            var expected = 0;
            var input = "";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_SingleNumberInput_ReturnsParsedNumber()
        {
            // Arrange
            var expected = 2;
            var input = "2";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_TwoNumbersCommaDelimited_ReturnsSumOfNumbers()
        {
            // Arrange
            var expected = 8;
            var input = "3,5";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_TwoNumbersNewlineDelimited_ReturnsSumOfNumbers()
        {
            // Arrange
            var expected = 8;
            var input = @"3\n5";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_ThreeNumbersDelimitedEitherWay_ReturnsSumOfNumbers()
        {
            // Arrange
            var expected = 13;
            var input = @"3,8\n2";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("CI"), ExpectedException(typeof(ArgumentException))]
        public void Calculate_NegativeNumberExists_ThrowsArgumentException()
        {
            // Arrange
            var input = "-4,2";

            // Act
            var result = _stringCalculator.Calculate(input);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_HighNumberExists_IgnoresHighNumber()
        {
            // Arrange
            var expected = 8;
            var input = "3,1000,5";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }

    internal class StringCalculator
    {
        private readonly string[] _separators = { ",", @"\n" };

        public int Calculate(string input)
        {
            var digits = input.Split(_separators, StringSplitOptions.None).Select(ParseIntegerAndReturnZeroIfEmpty).ToList();

            ThrowArgumentExceptionIfNegativeNubmerExists(digits);

            RemoveDigitsHigherThanThousand(digits);

            return digits.Sum();
        }

        private static int ParseIntegerAndReturnZeroIfEmpty(string input)
        {
            int parsedInt;
            int.TryParse(input, out parsedInt);
            return parsedInt;
        }

        private static void ThrowArgumentExceptionIfNegativeNubmerExists(IEnumerable<int> digits)
        {
            if (digits.Any(d => d < 0))
            {
                throw new ArgumentException();
            }
        }

        private static void RemoveDigitsHigherThanThousand(List<int> digits)
        {
            digits.RemoveAll(d => d > 999);
        }
    }
}
