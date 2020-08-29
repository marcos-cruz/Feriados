using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bigai.Tools.Feriados.Tests
{
    public class FeriadoTest
    {
        [Theory]
        [InlineData(2020)]
        public void Feriado_DeveCriarInstancia(int anoDoFeriado)
        {
            // Arrange
            Feriado feriado;

            // Act
            feriado = Feriado.Factory(anoDoFeriado);

            // Assert
            Assert.NotNull(feriado);
        }

        [Theory]
        [InlineData(2020)]
        public void Feriado_ListaDeveEstarOrdenadaEmOrdemCrescente(int anoDoFeriado)
        {
            // Arrange
            Feriado feriado = Feriado.Factory(anoDoFeriado);

            // Act
            List<FeriadoCelebrado> feriados = feriado.Feriados(anoDoFeriado).ToList();

            // Assert
            for (int i = 1, j = feriados.Count - 1; i < j; i++)
            {
                var feriado1 = feriados[i - 1];
                var feriado2 = feriados[i];

                Assert.True(feriado1.DataFeriado <= feriado2.DataFeriado);
            }
        }

        [Theory]
        [InlineData(2020, "BR", 14)]
        public void Feriado_DeveRetornarListaCom14FeriadosNacionais(int anoDoFeriado, string pais, int feriadosEsperados)
        {
            // Arrange
            Feriado feriado = Feriado.Factory(anoDoFeriado);

            // Act
            List<FeriadoCelebrado> feriadosNacionais = feriado.FeriadosNacionais(anoDoFeriado, pais).ToList();

            // Assert
            Assert.True(feriadosEsperados == feriadosNacionais.Count);
        }

        [Theory]
        [InlineData(2020, "BR")]
        public void Feriado_ListaDeFeriadosNacionaisDeveEstarEmOrdemCrescente(int anoDoFeriado, string pais)
        {
            // Arrange
            Feriado feriado = Feriado.Factory(anoDoFeriado);

            // Act
            List<FeriadoCelebrado> feriadosNacionais = feriado.FeriadosNacionais(anoDoFeriado, pais).ToList();

            // Assert
            for (int i = 1, j = feriadosNacionais.Count - 1; i < j; i++)
            {
                var feriado1 = feriadosNacionais[i - 1];
                var feriado2 = feriadosNacionais[i];

                Assert.True(feriado1.DataFeriado <= feriado2.DataFeriado);
            }
        }

        [Theory]
        [InlineData(2020, "BR", "RJ", 4)]
        public void Feriado_DeveRetornarListaDeFeriadosEstaduais(int anoDoFeriado, string pais, string estado, int feriadosEsperados)
        {
            // Arrange
            Feriado feriado = Feriado.Factory(anoDoFeriado);

            // Act
            List<FeriadoCelebrado> feriadosEstaduais = feriado.FeriadosEstaduais(anoDoFeriado, estado, pais).ToList();

            // Assert
            Assert.True(feriadosEsperados == feriadosEstaduais.Count);
        }

        [Theory]
        [InlineData(2020, "BR", "RJ")]
        public void Feriado_ListaDeFeriadosEstaduaisDeveEstarEmOrdemCrescente(int anoDoFeriado, string pais, string estado)
        {
            // Arrange
            Feriado feriado = Feriado.Factory(anoDoFeriado);

            // Act
            List<FeriadoCelebrado> feriadosEstaduais = feriado.FeriadosEstaduais(anoDoFeriado, estado, pais).ToList();

            // Assert
            for (int i = 1, j = feriadosEstaduais.Count - 1; i < j; i++)
            {
                var feriado1 = feriadosEstaduais[i - 1];
                var feriado2 = feriadosEstaduais[i];

                Assert.True(feriado1.DataFeriado <= feriado2.DataFeriado);
            }
        }

        [Theory]
        [InlineData(2020, "3550308", 1)]   // São Paulo, SP
        [InlineData(2020, "7107", 1)]      // São Paulo, SP
        public void Feriado_DeveRetornarListaDeFeriadosMunicipais(int anoDoFeriado, string codigoIbge, int feriadosEsperados)
        {
            // Arrange
            Feriado feriado = Feriado.Factory(anoDoFeriado);

            // Act
            List<FeriadoCelebrado> feriadosMunicipais = feriado.FeriadosMunicipais(anoDoFeriado, codigoIbge).ToList();

            // Assert
            Assert.True(feriadosEsperados == feriadosMunicipais.Count);
        }

        [Theory]
        [InlineData(2020, 01, 01, 2021)]
        public void Feriado_DeveRetornarListaDeFeriadosParaUmaData(int anoDoFeriado, int dia, int mes, int ano)
        {
            // Arrange
            Feriado feriado = Feriado.Factory(anoDoFeriado);
            DateTime dataDoFeriado = new DateTime(ano, mes, dia);

            // Act
            List<FeriadoCelebrado> feriadosNaData = feriado.EhFeriado(dataDoFeriado).ToList();

            // Assert
            Assert.True(feriadosNaData.Count > 0);
        }

        [Theory]
        [InlineData(2020, 01, "BR")]
        public void Feriado_DeveRetornarListaDeFeriadosDoMes(int anoDoFeriado, int mesFeriado, string pais)
        {
            // Arrange
            Feriado feriado = Feriado.Factory(anoDoFeriado);

            // Act
            List<FeriadoCelebrado> feriadosDoMes = feriado.FeriadosDoMes(anoDoFeriado, mesFeriado, pais).ToList();

            // Assert
            Assert.True(feriadosDoMes.Count > 0);
        }

        [Theory]
        [InlineData(2020, 01, "BR", "SP")]
        public void Feriado_DeveRetornarListaDeFeriadosDoMesDoEstado(int anoDoFeriado, int mesFeriado, string pais, string estado)
        {
            // Arrange
            Feriado feriado = Feriado.Factory(anoDoFeriado);

            // Act
            List<FeriadoCelebrado> feriadosDoMes = feriado.FeriadosDoMes(anoDoFeriado, mesFeriado, estado, pais).ToList();

            // Assert
            Assert.True(feriadosDoMes.Count > 0);
        }
    }
}
