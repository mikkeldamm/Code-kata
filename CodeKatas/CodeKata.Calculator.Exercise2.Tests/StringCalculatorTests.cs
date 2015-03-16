using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeKata.Calculator.Exercise2.Tests
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

    [TestClass]
    public class StringCalculatorTests
    {
        private StringCalculator _sut;

        [TestInitialize]
        public void Setup()
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
        public void Calculate_SingleNumberInput_ReturnsTheValue()
        {
            // Arrange
            var expected = 1;
            var input = "1";

            // Act
            var output = _sut.Calculate(input);

            // Assert
            Assert.AreEqual(expected, output);
        }
    }

    internal class StringCalculator
    {
        public int Calculate(string input)
        {
            int num;

            int.TryParse(input, out num);

            return num;
        }
    }
}
