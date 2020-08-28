using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Bigai.Tools.Feriados.Tests")]
namespace Bigai.Tools.Feriados.Helpers
{
    /// <summary>
    /// Esta classe fornece métodos para importar feriados nacionais de uma planilha no formato CSV. A primera linha da
    /// planilha sempre é desprezada, pois deve conter o header da planilha.
    /// </summary>
    /// <remarks>A planilha deve conter um header com 3 colunas para DIA MÊS EVENTO, e as colunas devem ser separadas
    /// pelo caracter ';'.</remarks>
    internal static class FeriadosNacionaisHelper
    {
        #region Constantes

        private const string _feriadosNacionaisCsv = @"assets/FeriadosNacionaisBr.csv";

        #endregion

        /// <summary>
        /// Este método importa de uma planilha CSV, as informações sobre feriados nacionais.
        /// </summary>
        /// <param name="anoFeriado">Ano com 4 digitos numéricos, para o qual esta sendo importado os feriados.</param>
        /// <param name="abrangencia">String que representa a abrangência dos feriados. Default "Nacional".</param>
        /// <param name="pais">Sigla do país com 2 letras. Default "BR".</param>
        /// <returns>Lista contendo os feriados nacionais.</returns>
        internal static List<FeriadoCelebrado> ImportarFeriadosNacionais(int anoFeriado, string abrangencia = "Nacional", string pais = "BR")
        {
            List<FeriadoCelebrado> feriadosNacionais = null;

            try
            {
                string startupPath = AppDomain.CurrentDomain.BaseDirectory;
                string arquivo = Path.Combine(startupPath, _feriadosNacionaisCsv);
                string[,] planilha = CsvHelper.LoadCsv(arquivo);

                if (planilha != null && planilha.GetLength(0) > 0)
                {
                    const int coluna1 = 0;
                    const int coluna2 = 1;
                    const int coluna3 = 2;
                    FeriadoCelebrado feriado;
                    feriadosNacionais = new List<FeriadoCelebrado>();

                    if (anoFeriado < 1900)
                    {
                        anoFeriado = DateTime.Now.Year;
                    }

                    for (int linha = 0, totalDeLinhas = planilha.GetLength(0); linha < totalDeLinhas; linha++)
                    {
                        if (!string.IsNullOrEmpty(planilha[linha, coluna1]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna2]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna3]))
                        {
                            int dia = string.IsNullOrEmpty(planilha[linha, coluna1]) ? 1 : int.Parse((planilha[linha, coluna1]).Trim());
                            int mes = string.IsNullOrEmpty(planilha[linha, coluna2]) ? 1 : int.Parse((planilha[linha, coluna2]).Trim());
                            var evento = string.IsNullOrEmpty(planilha[linha, coluna3]) ? "" : (planilha[linha, coluna3]).Trim();

                            if (!string.IsNullOrEmpty(evento))
                            {
                                DateTime dataFeriado = new DateTime(anoFeriado, mes, dia);

                                var codigoFederal = "";
                                var codigoIbge = "";
                                var estado = "";

                                feriado = FeriadoCelebrado.CriarFeriado(dataFeriado, evento, abrangencia, pais, estado, codigoFederal, codigoIbge, false, false);

                                feriadosNacionais.Add(feriado);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return feriadosNacionais;
        }

        /// <summary>
        /// Este método libera a memória utilizada para a carga da planilha de feriados nacionais.
        /// </summary>
        /// <param name="feriadosNacionais">Lista contendo os feriados nacionais.</param>
        internal static void Dispose(List<FeriadoCelebrado> feriadosNacionais)
        {
            if (feriadosNacionais != null)
            {
                feriadosNacionais.Clear();
                feriadosNacionais = new List<FeriadoCelebrado>();
            }
        }
    }
}
