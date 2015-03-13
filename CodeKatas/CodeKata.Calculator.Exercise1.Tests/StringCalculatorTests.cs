using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeKata.Calculator.Exercise1.Tests
{
    //An empty string returns zero
    //A single number returns the value
    //Two numbers, comma delimited, returns the sum
    //Two numbers, newline delimited, returns the sum
    //Three numbers, delimited either way, returns the sum
    //Negative numbers throw an exception
    //Numbers greater than 1000 are ignored
    //A single char delimiter can be defined on the first line (e.g. //# for a ‘#’ as the delimiter)
    //A multi char delimiter can be defined on the first line (e.g. //[###] for ‘###’ as the delimiter)
    //Many single or multi-char delimiters can be defined (each wrapped in square brackets)

    [TestClass]
    public class StringCalculatorTests
    {
        private StringCalculator _sut;
        
        [TestInitialize]
        public void SetUp()
        {
            _sut = new StringCalculator();
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_EmptyStringInput_ReturnsZero()
        {
            // Arrange
            var expected = 0;
            var input = "";

            // Act
            var output = _sut.Calculate(input);

            // Assert
            Assert.AreEqual(expected, output);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_SingleNumberInput_ReturnsTheValueAsInt()
        {
            // Arrange
            var expected = 3;
            var input = "3";

            // Act
            var output = _sut.Calculate(input);

            // Assert
            Assert.AreEqual(expected, output);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_TwoNumbersCommaDelimited_ReturnTheSumOfInput()
        {
            // Arrange
            var expected = 13;
            var input = "2,11";

            // Act
            var output = _sut.Calculate(input);

            // Assert
            Assert.AreEqual(expected, output);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_TwoNumbersNewlineDelimited_ReturnsTheSumOfInput()
        {
            // Arrange
            var expected = 19;
            var input = "4\n15";

            // Act
            var output = _sut.Calculate(input);

            // Assert
            Assert.AreEqual(expected, output);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_ThreeNumbersBothCommaAndNewlineDelimited_ReturnsTheSumOfInput()
        {
            // Arrange
            var expected = 34;
            var input = "19\n3,12";

            // Act
            var ouput = _sut.Calculate(input);

            // Assert
            Assert.AreEqual(expected, ouput);
        }

        [TestMethod, TestCategory("CI")]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_NegativeNumberInput_Throws()
        {
            // Arrange
            var input = "-4";

            // Act
            var ouput = _sut.Calculate(input);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_NumberGreatherThanThousand_IsIgnored()
        {
            // Arrange
            var expected = 2;
            var input = "2,1003";

            // Act
            var ouput = _sut.Calculate(input);

            // Assert
            Assert.AreEqual(expected, ouput);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_SingleCharDelimiterSpecified_ReturnsSumOfInput()
        {
            // Arrange
            var expected = 34;
            var input = "//#2#5#10#17";

            // Act
            var output = _sut.Calculate(input);

            // Assert
            Assert.AreEqual(expected, output);
        }

        [TestMethod, TestCategory("CI")]
        public void Calculate_MultipleCharDelimiterSpecified_ReturnsSumOfInput()
        {
            // Arrange
            var expected = 55;
            var input = "//[%%%]33%%%12%%%10";

            // Act
            var output = _sut.Calculate(input);

            // Assert
            Assert.AreEqual(expected, output);
        }
    }

    public class StringCalculator
    {
        public int Calculate(string input)
        {
            var delimiters = ",\n";

            if (input.StartsWith("//["))
            {
                var firstSpecifier = input.IndexOf('[');
                var lastSpecifier = input.IndexOf(']') - firstSpecifier;

                delimiters += "(" + input.Substring(firstSpecifier + 1, lastSpecifier - 1) + ")";

                input = input.Replace(input.Substring(0, input.IndexOf(']')+1), "");
            }
            else if (input.StartsWith("//"))
            {
                delimiters += "(" + input.Substring(2, 1) + ")";

                input = input.Replace(input.Substring(0, 3), "");
            }

            var digits = Regex.Split(input, @"[" + delimiters + "]");

            var numbers = digits
                .Select(n =>
                {
                    int num;
                    int.TryParse(n, out num);

                    if (num > 999)
                        return 0;

                    if (num >= 0)
                        return num;
                    
                    throw new ArgumentException("Number cannot be negative");
                })
                .ToArray();

            return numbers.Sum();
        }
    }
}
