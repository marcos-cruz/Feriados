using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Bigai.Tools.Feriados.Tests")]
namespace Bigai.Tools.Feriados.Helpers
{
    internal static class FeriadosMunicipaisHelper
    {
        #region Constantes

        private const string _feriadosMunicipaisCsv = @"assets/FeriadosMunicipaisBr.csv";

        #endregion

        /// <summary>
        /// Este método importa de uma planilha CSV, as informações sobre feriados municipais.
        /// </summary>
        /// <param name="anoFeriado">Ano com 4 digitos numéricos, para o qual esta sendo importado os feriados.</param>
        /// <param name="abrangencia">String que representa a abrangência dos feriados. Default "Municipal".</param>
        /// <param name="pais">Sigla do país com 2 letras. Default "BR".</param>
        /// <returns>Lista contendo os feriados municipais.</returns>
        internal static List<FeriadoCelebrado> ImportarFeriadosMunicipais(int anoFeriado, string abrangencia = "Municipal", string pais = "BR")
        {
            List<FeriadoCelebrado> feriadosMunicipais = null;

            try
            {
                string startupPath = AppDomain.CurrentDomain.BaseDirectory;
                string arquivo = Path.Combine(startupPath, _feriadosMunicipaisCsv);
                string[,] planilha = CsvHelper.LoadCsv(arquivo);

                if (planilha != null && planilha.GetLength(0) > 0)
                {
                    const int coluna1 = 0;
                    const int coluna2 = 1;
                    const int coluna3 = 2;
                    const int coluna4 = 3;
                    const int coluna5 = 4;
                    const int coluna6 = 5;
                    const int coluna7 = 6;
                    const int coluna8 = 7;
                    FeriadoCelebrado feriado;
                    feriadosMunicipais = new List<FeriadoCelebrado>();

                    if (anoFeriado < 1900)
                    {
                        anoFeriado = DateTime.Now.Year;
                    }

                    for (int linha = 0, totalDeLinhas = planilha.GetLength(0); linha < totalDeLinhas; linha++)
                    {
                        if (!string.IsNullOrEmpty(planilha[linha, coluna1]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna2]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna3]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna4]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna5]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna6]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna7]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna8]))
                        {
                            string codigoIbge = string.IsNullOrEmpty(planilha[linha, coluna1]) ? "" : (planilha[linha, coluna1]).Trim();
                            string codigoFederal = string.IsNullOrEmpty(planilha[linha, coluna2]) ? "" : (planilha[linha, coluna2]).Trim();
                            string codigoEstadual = string.IsNullOrEmpty(planilha[linha, coluna3]) ? "" : (planilha[linha, coluna3]).Trim();
                            string estado = string.IsNullOrEmpty(planilha[linha, coluna4]) ? "" : (planilha[linha, coluna4]).Trim().ToUpper();
                            string codigoUf = string.IsNullOrEmpty(planilha[linha, coluna5]) ? "" : (planilha[linha, coluna5]).Trim().ToUpper();
                            string cidade = string.IsNullOrEmpty(planilha[linha, coluna6]) ? "" : (planilha[linha, coluna6]).Trim();
                            int dia = string.IsNullOrEmpty(planilha[linha, coluna7]) ? 1 : int.Parse((planilha[linha, coluna7]).Trim());
                            int mes = string.IsNullOrEmpty(planilha[linha, coluna8]) ? 1 : int.Parse((planilha[linha, coluna8]).Trim());

                            if (!string.IsNullOrEmpty(cidade))
                            {
                                DateTime dataFeriado = new DateTime(anoFeriado, mes, dia);
                                var evento = "Aniversário de " + cidade;

                                feriado = FeriadoCelebrado.CriarFeriado(dataFeriado, evento, abrangencia, pais, estado, codigoFederal, codigoIbge, false, false);

                                feriadosMunicipais.Add(feriado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return feriadosMunicipais;
        }
        
        /// <summary>
        /// Este método libera a memória utilizada para a carga da planilha de feriados municipais.
        /// </summary>
        /// <param name="feriadosMunicipais">Lista contendo os feriados municipais.</param>
        internal static void Dispose(List<FeriadoCelebrado> feriadosMunicipais)
        {
            if (feriadosMunicipais != null)
            {
                feriadosMunicipais.Clear();
                feriadosMunicipais = new List<FeriadoCelebrado>();
            }
        }
    }
}
