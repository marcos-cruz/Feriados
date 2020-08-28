using System;

namespace Bigai.Tools.Feriados
{
    /// <summary>
    /// FeriadoCelebrado representa a informação detalhada de um <see cref="Feriado"/> celebrado.
    /// </summary>
    public class FeriadoCelebrado
    {
        #region Public Properties

        /// <summary>
        /// Data em que ocorre o feriado.
        /// </summary>
        public DateTime DataFeriado { get; private set; }

        /// <summary>
        /// Descrição sobre o que é comemorado no feriado.
        /// </summary>
        public string Descricao { get; private set; }

        /// <summary>
        /// Abrangência do feriado, podendo assumir Nacional, Estadual ou Municipal.
        /// </summary>
        public string Abrangencia { get; private set; }

        /// <summary>
        /// Sigla do país onde ocorre o feriado, composta por 2 letras, segundo a norma ISO-3166.
        /// </summary>
        public string Pais { get; private set; }

        /// <summary>
        /// Sigla do estado onde ocorre o feriado, composta por 2 letras, segundo a norma ISO-3166.
        /// </summary>
        public string Estado { get; private set; }

        /// <summary>
        /// Código Federal que identifica o Município, composto de 1 a 4 digitos numéricos.
        /// </summary>
        public string CodigoFederalMunicipio { get; private set; }

        /// <summary>
        /// Código Ibge que identifica o Município, composto 7 digitos numéricos.
        /// </summary>
        public string CodigoIbgeMunicipio { get; private set; }

        /// <summary>
        /// Indica se o feriado é um ponto facultativo.
        /// </summary>
        public bool PontoFacultativo { get; private set; }

        /// <summary>
        /// Indica se é um feriado móvel.
        /// </summary>
        public bool FeriadoMovel { get; private set; }

        #endregion

        #region Construtor

        private FeriadoCelebrado(DateTime dataFeriado, string descricao, string abrangencia, string pais, string estado, string codigoFederalMunicipio, string codigoIbgeMunicipio, bool pontoFacultativo, bool feriadoMovel)
        {
            DataFeriado = dataFeriado;
            Descricao = descricao;
            Abrangencia = abrangencia;
            Pais = pais;
            Estado = estado;
            CodigoFederalMunicipio = codigoFederalMunicipio;
            CodigoIbgeMunicipio = codigoIbgeMunicipio;
            PontoFacultativo = pontoFacultativo;
            FeriadoMovel = feriadoMovel;
        }

        public static FeriadoCelebrado CriarFeriado(DateTime dataFeriado, string descricao, string abrangencia, string pais, string estado, string codigoFederalMunicipio, string codigoIbgeMunicipio, bool pontoFacultativo, bool feriadoMovel)
        {
            return new FeriadoCelebrado(dataFeriado, descricao, abrangencia, pais, estado, codigoFederalMunicipio, codigoIbgeMunicipio, pontoFacultativo, feriadoMovel);
        }

        #endregion

        #region Comparação

        public override bool Equals(object obj)
        {
            return obj is FeriadoCelebrado celebrado &&
                   DataFeriado == celebrado.DataFeriado &&
                   Descricao == celebrado.Descricao &&
                   Abrangencia == celebrado.Abrangencia &&
                   Pais == celebrado.Pais &&
                   Estado == celebrado.Estado &&
                   CodigoFederalMunicipio == celebrado.CodigoFederalMunicipio &&
                   CodigoIbgeMunicipio == celebrado.CodigoIbgeMunicipio &&
                   PontoFacultativo == celebrado.PontoFacultativo &&
                   FeriadoMovel == celebrado.FeriadoMovel;
        }

        #endregion

        #region Hashcode

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();

            hash.Add(DataFeriado);
            hash.Add(Descricao);
            hash.Add(Abrangencia);
            hash.Add(Pais);
            hash.Add(Estado);
            hash.Add(CodigoFederalMunicipio);
            hash.Add(CodigoIbgeMunicipio);
            hash.Add(PontoFacultativo);
            hash.Add(FeriadoMovel);

            return hash.ToHashCode();
        }

        #endregion

        #region Sobrecarga de Operadores

        /// <summary>
        /// Determina se dois feriados são iguais.
        /// </summary>
        /// <param name="objA">Instância do FeriadoCelebrado para comparação.</param>
        /// <param name="objB">Instância do FeriadoCelebrado para comparação.</param>
        /// <returns>Retorna <c>true</c> se os feriados forem iguais, caso contrário <c>false</c>.</returns>
        public static bool operator ==(FeriadoCelebrado objA, FeriadoCelebrado objB)
        {
            if (objA is null && objB is null)
            {
                return true;
            }

            if (objA is null || objB is null)
            {
                return false;
            }

            return objA.Equals(objB);
        }

        /// <summary>
        /// Determina se dois feriados são diferentes.
        /// </summary>
        /// <param name="objA">Instância do FeriadoCelebrado para comparação.</param>
        /// <param name="objB">Instância do FeriadoCelebrado para comparação.</param>
        /// <returns>Retorna <c>true</c> se os feriados forem diferentes, caso contrário <c>false</c>.</returns>
        public static bool operator !=(FeriadoCelebrado objA, FeriadoCelebrado objB)
        {
            return !(objA == objB);
        }

        #endregion
    }
}
