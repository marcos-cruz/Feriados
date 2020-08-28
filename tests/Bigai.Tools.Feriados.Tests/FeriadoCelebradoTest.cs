using System;
using Xunit;

namespace Bigai.Tools.Feriados.Tests
{
    public class FeriadoCelebradoTest
    {
        [Theory]
        [InlineData(01, 01, 2020, "Confraternização Universal", "Nacional", "BR", "", "", "", false, false)]
        public void Equals_QuandoMesmaInformacao_DeveSerConsideradoIgual(int dia, int mes, int ano, string descricao, string abrangencia, string pais, string estado, string codigoFederalMunicipio, string codigoIbgeMunicipio, bool pontoFacultativo, bool feriadoMovel)
        {
            // Arrange
            DateTime dataFeriado = new DateTime(ano, mes, dia);
            FeriadoCelebrado valor1 = FeriadoCelebrado.CriarFeriado(dataFeriado, descricao, abrangencia, pais, estado, codigoFederalMunicipio, codigoIbgeMunicipio, pontoFacultativo, feriadoMovel);
            FeriadoCelebrado valor2 = FeriadoCelebrado.CriarFeriado(dataFeriado, descricao, abrangencia, pais, estado, codigoFederalMunicipio, codigoIbgeMunicipio, pontoFacultativo, feriadoMovel);

            // Act
            bool igual = valor1.Equals(valor2);

            // Assert
            Assert.True(igual);
            Assert.True(valor1 == valor2);
        }

        [Theory]
        [InlineData(01, 01, 2020, "Confraternização Universal", "Nacional", "BR", "", "", "", false, false)]
        public void Equals_QuandoNaoEhMesmaInformacao_DeveSerConsideradoDiferente(int dia, int mes, int ano, string descricao, string abrangencia, string pais, string estado, string codigoFederalMunicipio, string codigoIbgeMunicipio, bool pontoFacultativo, bool feriadoMovel)
        {
            // Arrange
            DateTime dataFeriado = new DateTime(ano, mes, dia);
            FeriadoCelebrado valor1 = FeriadoCelebrado.CriarFeriado(dataFeriado, descricao, abrangencia, pais, estado, codigoFederalMunicipio, codigoIbgeMunicipio, pontoFacultativo, feriadoMovel);
            FeriadoCelebrado valor2 = FeriadoCelebrado.CriarFeriado(dataFeriado.AddYears(-1), descricao, abrangencia, pais, estado, codigoFederalMunicipio, codigoIbgeMunicipio, pontoFacultativo, feriadoMovel);

            // Act
            bool igual = valor1.Equals(valor2);

            // Assert
            Assert.False(igual);
            Assert.True(valor1 != valor2);
        }

        [Theory]
        [InlineData(01, 01, 2020, "Confraternização Universal", "Nacional", "BR", "", "", "", false, false)]
        public void GetHashCode_QuandoObjetosSaoIguais_DeveGerarMesmoHashCode(int dia, int mes, int ano, string descricao, string abrangencia, string pais, string estado, string codigoFederalMunicipio, string codigoIbgeMunicipio, bool pontoFacultativo, bool feriadoMovel)
        {
            // Arrange
            DateTime dataFeriado = new DateTime(ano, mes, dia);
            FeriadoCelebrado valor1 = FeriadoCelebrado.CriarFeriado(dataFeriado, descricao, abrangencia, pais, estado, codigoFederalMunicipio, codigoIbgeMunicipio, pontoFacultativo, feriadoMovel);
            FeriadoCelebrado valor2 = FeriadoCelebrado.CriarFeriado(dataFeriado, descricao, abrangencia, pais, estado, codigoFederalMunicipio, codigoIbgeMunicipio, pontoFacultativo, feriadoMovel);

            // Act
            var hashcode1 = valor1.GetHashCode();
            var hashcode2 = valor2.GetHashCode();

            // Assert
            Assert.Equal(hashcode1, hashcode2);
        }

        [Theory]
        [InlineData(01, 01, 2020, "Confraternização Universal", "Nacional", "BR", "", "", "", false, false)]
        public void GetHashCode_QuandoObjetosSaoDiferentes_DeveGerarHashCodeDiferentes(int dia, int mes, int ano, string descricao, string abrangencia, string pais, string estado, string codigoFederalMunicipio, string codigoIbgeMunicipio, bool pontoFacultativo, bool feriadoMovel)
        {
            // Arrange
            DateTime dataFeriado = new DateTime(ano, mes, dia);
            FeriadoCelebrado valor1 = FeriadoCelebrado.CriarFeriado(dataFeriado, descricao, abrangencia, pais, estado, codigoFederalMunicipio, codigoIbgeMunicipio, pontoFacultativo, feriadoMovel);
            FeriadoCelebrado valor2 = FeriadoCelebrado.CriarFeriado(dataFeriado.AddYears(-1), descricao, abrangencia, pais, estado, codigoFederalMunicipio, codigoIbgeMunicipio, pontoFacultativo, feriadoMovel);

            // Act
            var hashcode1 = valor1.GetHashCode();
            var hashcode2 = valor2.GetHashCode();

            // Assert
            Assert.NotEqual(hashcode1, hashcode2);
        }
    }
}
