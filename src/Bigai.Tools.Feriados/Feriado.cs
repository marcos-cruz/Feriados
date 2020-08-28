using Bigai.Tools.Feriados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bigai.Tools.Feriados
{
    /// <summary>
    /// Estaá classe fornece métodos para pesquisa de feriados.
    /// </summary>
    public class Feriado
    {
        #region Variaveis privadas

        private const string _brasil = "BR";
        private const string _nacional = "Nacional";
        private const string _estadual = "Estadual";
        private const string _municipal = "Municipal";

        /// <summary>
        /// Quantidade de dias que deve ser adicionado a data da Páscoa, se obtém a segunda-feira de carnaval.
        /// </summary>
        private const int _segundaDeCarnaval = -48;

        /// <summary>
        /// Quantidade de dias que deve ser adicionado a data da Páscoa, se obtém a terça-feira de carnaval.
        /// </summary>
        private const int _tercaDeCarnaval = -47;

        /// <summary>
        /// Quantidade de dias que deve ser adicionado a data da Páscoa, se obtém a quarta-feira de cinzas.
        /// </summary>
        private const int _quartaDeCinzas = -46;

        /// <summary>
        /// Quantidade de dias que deve ser adicionado a data da Páscoa, se obtém a sexta-feira santa.
        /// </summary>
        private const int _sextaSanta = -2;

        /// <summary>
        /// Quantidade de dias que deve ser adicionado a data da Páscoa, se obtém a quinta-feira de Corpus Christi.
        /// </summary>
        private const int _corpusChristi = 60;

        /// <summary>
        /// Lista com todos os feriados que ocorrem no ano instânciado.
        /// </summary>
        private List<FeriadoCelebrado> _feriados;

        #endregion

        #region Public Properties

        /// <summary>
        /// Ano em que ocorre o feriado.
        /// </summary>
        public int AnoFeriado { get; private set; }

        /// <summary>
        /// Retorna uma lista com todos os feriados.
        /// </summary>
        public IReadOnlyCollection<FeriadoCelebrado> Feriados => _feriados.ToArray();

        #endregion

        #region Construtor

        private Feriado(int anoFeriado)
        {
            if (AnoFeriado != anoFeriado)
            {
                Dispose();

                GerarFeriadosNacionais(anoFeriado);
                GerarFeriadosEstaduais(anoFeriado);
                GerarFeriadosMunicipais(anoFeriado);

                _feriados.Sort((x, y) => DateTime.Compare(x.DataFeriado, y.DataFeriado));
            }

            AnoFeriado = anoFeriado;
        }

        /// <summary>
        /// Retorna uma instância de Feriado.
        /// </summary>
        /// <param name="anoFeriado">Ano do calendário, composto de 4 digitos númericos.</param>
        /// <returns></returns>
        public static Feriado Factory(int anoFeriado)
        {
            return new Feriado(anoFeriado);
        }

        #endregion

        #region Métodos Privados

        private void GerarFeriadosNacionais(int anoFeriado)
        {
            AdicionarFeriadosNacionaisComDataFixa(anoFeriado);

            AdicionarFeriadosNacionaisComDatasMoveis(anoFeriado);
        }

        private void GerarFeriadosEstaduais(int anoFeriado)
        {
            List<FeriadoCelebrado> feriadosEstaduais = FeriadosEstaduaisHelper.ImportarFeriadosEstaduais(anoFeriado, _estadual, _brasil);

            AdicionarFeriados(feriadosEstaduais);

            FeriadosEstaduaisHelper.Dispose(feriadosEstaduais);
        }

        private void GerarFeriadosMunicipais(int anoFeriado)
        {
            List<FeriadoCelebrado> feriadosMunicipais = FeriadosMunicipaisHelper.ImportarFeriadosMunicipais(anoFeriado, _municipal, _brasil);

            AdicionarFeriados(feriadosMunicipais);

            FeriadosEstaduaisHelper.Dispose(feriadosMunicipais);
        }

        private void AdicionarFeriados(List<FeriadoCelebrado> feriados)
        {
            if (feriados != null && feriados.Count > 0)
            {
                for (int i = 0, j = feriados.Count; i < j; i++)
                {
                    AdicionarFeriado(feriados[i]);
                }
            }
        }

        private void AdicionarFeriado(FeriadoCelebrado feriado)
        {
            if (_feriados != null && !_feriados.Contains(feriado))
            {
                _feriados.Add(feriado);
            }
        }

        private void AdicionarFeriadosNacionaisComDataFixa(int anoFeriado)
        {
            List<FeriadoCelebrado> feriadosNacionais = FeriadosNacionaisHelper.ImportarFeriadosNacionais(anoFeriado, _nacional, _brasil);

            AdicionarFeriados(feriadosNacionais);

            FeriadosNacionaisHelper.Dispose(feriadosNacionais);
        }

        private void AdicionarFeriadosNacionaisComDatasMoveis(int anoFeriado)
        {
            DateTime domingoDePascoa = anoFeriado.Pascoa();

            FeriadoCelebrado feriado = ObterSegundaDeCarnaval(domingoDePascoa);
            if (feriado != null)
            {
                _feriados.Add(feriado);
            }

            feriado = ObterTercaDeCarnaval(domingoDePascoa);
            if (feriado != null)
            {
                _feriados.Add(feriado);
            }

            feriado = ObterQuartaDeCinzas(domingoDePascoa);
            if (feriado != null)
            {
                _feriados.Add(feriado);
            }

            feriado = ObterSextaSanta(domingoDePascoa);
            if (feriado != null)
            {
                _feriados.Add(feriado);
            }

            feriado = ObterDomingoPascoa(domingoDePascoa);
            if (feriado != null)
            {
                _feriados.Add(feriado);
            }

            feriado = ObterCorpusChristi(domingoDePascoa);
            if (feriado != null)
            {
                _feriados.Add(feriado);
            }
        }

        private FeriadoCelebrado ObterSegundaDeCarnaval(DateTime domingoDePascoa)
        {
            DateTime erro = new DateTime(1, 1, 1);
            FeriadoCelebrado feriado = null;
            const string descricao = "Segunda-Feira de Carnaval";

            if (domingoDePascoa != erro)
            {
                DateTime segundaFeiraDeCarnaval = domingoDePascoa.AddDays(_segundaDeCarnaval);
                var abrangencia = _nacional;
                var pais = _brasil;
                var estado = "";
                var codigoFederal = "";
                var codigoIbge = "";

                feriado = FeriadoCelebrado.CriarFeriado(segundaFeiraDeCarnaval, descricao, abrangencia, pais, estado, codigoFederal, codigoIbge, true, true);
            }

            return feriado;
        }

        private FeriadoCelebrado ObterTercaDeCarnaval(DateTime domingoDePascoa)
        {
            DateTime erro = new DateTime(1, 1, 1);
            FeriadoCelebrado feriado = null;
            const string descricao = "Terça-Feira de Carnaval";

            if (domingoDePascoa != erro)
            {
                DateTime tercaFeiraDeCarnaval = domingoDePascoa.AddDays(_tercaDeCarnaval);
                var abrangencia = _nacional;
                var pais = _brasil;
                var estado = "";
                var codigoFederal = "";
                var codigoIbge = "";

                feriado = FeriadoCelebrado.CriarFeriado(tercaFeiraDeCarnaval, descricao, abrangencia, pais, estado, codigoFederal, codigoIbge, true, true);
            }

            return feriado;
        }

        private FeriadoCelebrado ObterQuartaDeCinzas(DateTime domingoDePascoa)
        {
            DateTime erro = new DateTime(1, 1, 1);
            FeriadoCelebrado feriado = null;
            const string descricao = "Quarta-feira de Cinzas";

            if (domingoDePascoa != erro)
            {
                DateTime quartaDeCinzas = domingoDePascoa.AddDays(_quartaDeCinzas);
                var abrangencia = _nacional;
                var pais = _brasil;
                var estado = "";
                var codigoFederal = "";
                var codigoIbge = "";

                feriado = FeriadoCelebrado.CriarFeriado(quartaDeCinzas, descricao, abrangencia, pais, estado, codigoFederal, codigoIbge, true, true);
            }

            return feriado;
        }

        private FeriadoCelebrado ObterSextaSanta(DateTime domingoDePascoa)
        {
            DateTime erro = new DateTime(1, 1, 1);
            FeriadoCelebrado feriado = null;
            const string descricao = "Paixão de Cristo";

            if (domingoDePascoa != erro)
            {
                DateTime sextaSanta = domingoDePascoa.AddDays(_sextaSanta);
                var abrangencia = _nacional;
                var pais = _brasil;
                var estado = "";
                var codigoFederal = "";
                var codigoIbge = "";

                feriado = FeriadoCelebrado.CriarFeriado(sextaSanta, descricao, abrangencia, pais, estado, codigoFederal, codigoIbge, false, true);
            }

            return feriado;
        }

        private FeriadoCelebrado ObterDomingoPascoa(DateTime domingoDePascoa)
        {
            DateTime erro = new DateTime(1, 1, 1);
            FeriadoCelebrado feriado = null;
            const string descricao = "Páscoa";

            if (domingoDePascoa != erro)
            {
                var abrangencia = _nacional;
                var pais = _brasil;
                var estado = "";
                var codigoFederal = "";
                var codigoIbge = "";

                feriado = FeriadoCelebrado.CriarFeriado(domingoDePascoa, descricao, abrangencia, pais, estado, codigoFederal, codigoIbge, false, true);
            }

            return feriado;
        }

        private FeriadoCelebrado ObterCorpusChristi(DateTime domingoDePascoa)
        {
            DateTime erro = new DateTime(1, 1, 1);
            FeriadoCelebrado feriado = null;
            const string descricao = "Corpus Christi";

            if (domingoDePascoa != erro)
            {
                DateTime quintaCorpusChristi = domingoDePascoa.AddDays(_corpusChristi);
                var abrangencia = _nacional;
                var pais = _brasil;
                var estado = "";
                var codigoFederal = "";
                var codigoIbge = "";

                feriado = FeriadoCelebrado.CriarFeriado(quintaCorpusChristi, descricao, abrangencia, pais, estado, codigoFederal, codigoIbge, false, true);
            }

            return feriado;
        }

        private void Dispose()
        {
            if (_feriados != null)
            {
                _feriados.Clear();
            }

            _feriados = new List<FeriadoCelebrado>();
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Retorna uma lista com todos os feriados nacionais, para um país específico.
        /// </summary>
        /// <param name="pais">Sigla do estado onde ocorre o feriado, composta por 2 letras. Default 'BR'.</param>
        /// <returns>Retorna uma lista com todos os feriados nacionais do país solicitado.</returns>
        public IReadOnlyCollection<FeriadoCelebrado> FeriadosNacionais(string pais = _brasil)
        {
            return _feriados.Where(feriado => feriado.Abrangencia == _nacional && feriado.Pais == pais).ToArray();
        }

        /// <summary>
        /// Retorna uma lista com todos os feriados estaduais, para um estado específico.
        /// </summary>
        /// <param name="estado">Sigla do estado onde ocorre o feriado, composta por 2 letras.</param>
        /// <param name="pais">Sigla do estado onde ocorre o feriado, composta por 2 letras. Default 'BR'.</param>
        /// <returns>Retorna uma lista com todos os feriados estaduais.</returns>
        public IReadOnlyCollection<FeriadoCelebrado> FeriadosEstaduais(string estado, string pais = _brasil)
        {
            return _feriados.Where(feriado => feriado.Abrangencia == _estadual && feriado.Estado == estado && feriado.Pais == pais).ToArray();
        }

        /// <summary>
        /// Retorna uma lista com todos os feriados municipais, para um município especifico.
        /// </summary>
        /// <param name="codigoMunicipio">Código do município, podendo ser informado o Código Federal ou Código do Ibge.</param>
        /// <returns>Retorna uma lista com todos os feriados municipais.</returns>
        public IReadOnlyCollection<FeriadoCelebrado> FeriadosMunicipais(string codigoMunicipio)
        {
            return _feriados.Where(feriado => feriado.Abrangencia == _municipal && feriado.CodigoFederalMunicipio == codigoMunicipio || feriado.CodigoIbgeMunicipio == codigoMunicipio).ToArray();
        }

        /// <summary>
        /// Retorna uma lista com os feriados de uma data.
        /// </summary>
        /// <param name="data">Data que se deseja saber se é feriado.</param>
        /// <returns>Retorna uma lista com os feridos que coincidem com a data solicitada.</returns>
        public IReadOnlyCollection<FeriadoCelebrado> EhFeriado(DateTime data)
        {
            FeriadoCelebrado[] feriados = _feriados.Where(feriado => feriado.DataFeriado == data).ToArray();

            if (feriados == null || feriados.Length == 0)
            {
                Feriado feriado = Factory(data.Year);
                feriados = feriado._feriados.Where(feriado => feriado.DataFeriado == data).ToArray();
                feriado.Dispose();
            }

            return feriados;
        }

        #endregion
    }
}
