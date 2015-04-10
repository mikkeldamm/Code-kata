using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeKata.Calculator.Exercise5.Tests
{
    [TestClass]
    public class StringCalculatorTests
    {
        private IStringCalculator _stringCalculator;

        [TestInitialize]
        public void Setup()
        {
            _stringCalculator = new StringCalculator();
        }

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
        public void Calculate_SingleNumberInput_ReturnsNumber()
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
        public void Calculate_TwoNumbersDelimitedByComma_ReturnsSumOfNumbers()
        {
            // Arrange
            var expected = 5;
            var input = "2,3";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_TwoNumbersDelimitedByNewline_ReturnsSumOfNumbers()
        {
            // Arrange
            var expected = 5;
            var input = @"2\n3";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("CI"), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Calculate_NumberLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var input = "-1";

            // Act
            var result = _stringCalculator.Calculate(input);
        }
    }

    internal interface IStringCalculator
    {
        int Calculate(string input);
    }

    internal class StringCalculator : IStringCalculator
    {
        private readonly string[] _separators = {",", @"\n"};

        public int Calculate(string input)
        {
            var digitsSplitted = SplitStringToArrayOfDigits(input);
            var parsedInts = ParseDigitsToListOfInts(digitsSplitted);

            ThrowOutOfRangeIfNegativeNumberExistsIn(parsedInts);

            return parsedInts.Sum();
        }

        private IEnumerable<string> SplitStringToArrayOfDigits(string input)
        {
            return input.Split(_separators, StringSplitOptions.None);
        }

        private IEnumerable<int> ParseDigitsToListOfInts(IEnumerable<string> digits)
        {
            return digits.Select(ParseStringDigitToInt).ToList();
        }

        private static int ParseStringDigitToInt(string digit)
        {
            int parsedNumber;
            int.TryParse(digit, out parsedNumber);
            return parsedNumber;
        }

        private static void ThrowOutOfRangeIfNegativeNumberExistsIn(IEnumerable<int> allInts)
        {
            if (allInts.Any(i => i < 0))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

    }
}
