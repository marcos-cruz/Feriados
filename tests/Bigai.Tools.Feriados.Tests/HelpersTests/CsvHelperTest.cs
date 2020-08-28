using Bigai.Tools.Feriados.Helpers;
using Xunit;

namespace Bigai.Tools.Feriados.Tests.HelpersTests
{
    public class CsvHelperTest
    {
        [Theory]
        [InlineData(@"assets/FeriadosEstaduaisBr.csv", 48)]
        [InlineData(@"assets/FeriadosMunicipaisBr.csv", 5564)]
        [InlineData(@"assets/FeriadosNacionaisBr.csv", 8)]
        public void LoadCsv_DeveCarregarArquivosCsv_True(string fileName, int registrosEsperados)
        {
            //
            // Arrange
            //
            string[,] planilha;
            int registrosRetornados;

            //
            // Act
            //
            planilha = CsvHelper.LoadCsv(fileName);
            registrosRetornados = planilha != null ? planilha.GetLength(0) : 0;

            //
            // Assert
            //
            Assert.NotNull(planilha);
            Assert.Equal(registrosEsperados, registrosRetornados);
        }

        [Theory]
        [InlineData(@"assettests/FeriadosEstaduaisBr.csv", 0)]
        [InlineData(@"assettests/FeriadosMunicipaisBr.csv", 0)]
        [InlineData(@"assettests/FeriadosNacionaisBr.csv", 0)]
        public void LoadCsv_DeveCarregarArquivosCsv_False(string fileName, int registrosEsperados)
        {
            //
            // Arrange
            //
            string[,] planilha;
            int registrosRetornados;

            //
            // Act
            //
            planilha = CsvHelper.LoadCsv(fileName);
            registrosRetornados = planilha != null ? planilha.GetLength(0) : 0;

            //
            // Assert
            //
            Assert.Null(planilha);
            Assert.Equal(registrosEsperados, registrosRetornados);
        }

        [Theory]
        [InlineData(@"assets/FeriadosEstaduaisBr.csv")]
        [InlineData(@"assets/FeriadosMunicipaisBr.csv")]
        [InlineData(@"assets/FeriadosNacionaisBr.csv")]
        public void LerArquivo_DeveLerArquivosCsv_True(string fileName)
        {
            //
            // Arrange
            //
            string conteudo;

            //
            // Act
            //
            conteudo = CsvHelper.LerArquivo(fileName);

            //
            // Assert
            //
            Assert.False(string.IsNullOrEmpty(conteudo));
        }

        [Theory]
        [InlineData(@"assettests/FeriadosEstaduaisBr.csv")]
        [InlineData(@"assettests/FeriadosMunicipaisBr.csv")]
        [InlineData(@"assettests/FeriadosNacionaisBr.csv")]
        public void LerArquivo_DeveLerArquivosCsv_False(string fileName)
        {
            //
            // Arrange
            //
            string conteudo;

            //
            // Act
            //
            conteudo = CsvHelper.LerArquivo(fileName);

            //
            // Assert
            //
            Assert.True(string.IsNullOrEmpty(conteudo));
        }

        [Theory]
        [InlineData("Isto deve ser a linha 1\u000D\u000AEsta é a linha 2", 2)]
        public void ConverterEmLinhas_DeveConverterEmDuasLinhas(string conteudo, int linhasEsperadas)
        {
            // Arrange
            string[] linhas;
            int linhasRetornadas;

            // Act
            linhas = CsvHelper.ConverterEmLinhas(conteudo);
            linhasRetornadas = linhas != null ? linhas.Length : 0;

            // Assert
            Assert.NotNull(linhas);
            Assert.Equal(linhasEsperadas, linhasRetornadas);
        }
    }
}
