using Bigai.Tools.Feriados.Helpers;
using System;
using Xunit;

namespace Bigai.Tools.Feriados.Tests.HelpersTests
{
    public class PascoaHelperTest
    {
        [Theory]
        [InlineData(12, 04, 2020)]
        [InlineData(18, 04, 2049)]
        [InlineData(19, 04, 2076)]
        [InlineData(28, 03, 2100)]
        public void Pascoa_DeveRetornarDataCorretaDaPascoa(int diaDaPascoa, int mesDaPascoa, int anoDaPascoa)
        {
            // Arrange
            DateTime dataEsperadaDaPascoa = new DateTime(anoDaPascoa, mesDaPascoa, diaDaPascoa);

            // Act
            DateTime dataRetornadaDaPascoa = anoDaPascoa.Pascoa();

            // Assert
            Assert.Equal(dataEsperadaDaPascoa, dataRetornadaDaPascoa);
        }
    }
}
