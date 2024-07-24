using Microsoft.QualityTools.Testing.Fakes;

namespace Calculator.Tests
{
    public class CalcTests
    {
        [Fact]
        public void Sum_3_and_4_results_7()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(3, 4);

            //Assert
            Assert.Equal(7, result);
        }

        [Fact]
        public void Sum_0_and_0_results_0()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(2, 2, 4)]
        [InlineData(-2, 5, 3)]
        [InlineData(-2, -5, -7)]
        public void Sum_tests_with_results(int a, int b, int exp)
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(a, b);

            //Assert
            Assert.Equal(exp, result);
        }


        [Fact]
        public void Sum_MAX_and_1_throws_OverflowEx()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }

        [Fact]
        public void Calc_IsWeekend()
        {
            var calc = new Calc();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 7, 22);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 7, 23);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 7, 24);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 7, 25);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 7, 26);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 7, 27);
                Assert.True(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 7, 28);
                Assert.True(calc.IsWeekend());
            }
        }
    }
}