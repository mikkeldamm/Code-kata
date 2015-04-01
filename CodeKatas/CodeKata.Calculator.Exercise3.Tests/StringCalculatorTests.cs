using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeKata.Calculator.Exercise3.Tests
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
        public void Calculate_SingleNumberInput_ReturnTheValue()
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
        public void Calculate_TwoNumbersInput_ReturnsSumOfNumbers()
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
        public void Calculate_TwoNumbersInputNewlineDelimited_ReturnsSumOfNumbers()
        {
            // Arrange
            var expected = 5;
            var input = @"2\n3";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_ThreeNumbersDelimitedCommaAndNewline_RetunsSumOfNumbers()
        {
            // Arrange
            var expected = 7;
            var input = @"2,4\n1";

            // Act
            var result = _stringCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("CI"), ExpectedException(typeof(ArgumentException))]
        public void Calculate_NegativeNumberInput_ThrowsArgumentException()
        {
            // Arrange
            var input = "-3";

            // Act
            var result = _stringCalculator.Calculate(input);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_HighNumberInput_ReturnsSumOfNumbersWithoutHighNumber()
        {
            // Arrange
            var expected = 20;
            var input = "1001,20";

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
            var allDigitsFound = FindAllDigitsInString(input);

            if (IsNegativeNumberInList(allDigitsFound))
            {
                throw new ArgumentException("Number cannot be negative");
            }

            RemoveNumbersHigherThanThousand(allDigitsFound);

            return allDigitsFound.Sum();
        }

        private List<int> FindAllDigitsInString(string input)
        {
            var digitsInString = input.Split(_separators, StringSplitOptions.None);
            var allDigitsFound = digitsInString.Select(ParseStringToInteger).ToList();

            return allDigitsFound;
        }

        private static void RemoveNumbersHigherThanThousand(List<int> allDigitsFound)
        {
            allDigitsFound.RemoveAll(d => d > 1000);
        }

        private static bool IsNegativeNumberInList(IEnumerable<int> allDigitsFound)
        {
            return allDigitsFound.Any(d => d < 0);
        }

        private static int ParseStringToInteger(string inputString)
        {
            var parsedInt = 0;
            int.TryParse(inputString, out parsedInt);
            return parsedInt;
        }
    }
}
