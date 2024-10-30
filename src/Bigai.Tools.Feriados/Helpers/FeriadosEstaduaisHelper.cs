using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Bigai.Tools.Feriados.Tests")]
namespace Bigai.Tools.Feriados.Helpers
{
    /// <summary>
    /// Esta classe fornece métodos para importar feriados estaduais de uma planilha no formato CSV. A primera linha da
    /// planilha sempre é desprezada, pois deve conter o header da planilha.
    /// </summary>
    /// <remarks>A planilha deve conter um header com 4 colunas para DIA MÊS UF EVENTO, e as colunas devem ser separadas
    /// pelo caracter ';'.</remarks>
    internal static class FeriadosEstaduaisHelper
    {
        #region Constantes

        private const string _feriadosEstaduaisCsv = @"assets/FeriadosEstaduaisBr.csv";

        #endregion

        /// <summary>
        /// Este método importa de uma planilha CSV, as informações sobre feriados estaduais.
        /// </summary>
        /// <param name="anoFeriado">Ano com 4 digitos numéricos, para o qual esta sendo importado os feriados.</param>
        /// <param name="abrangencia">String que representa a abrangência dos feriados. Default "Estadual".</param>
        /// <param name="pais">Sigla do país com 2 letras. Default "BR".</param>
        /// <returns>Lista contendo os feriados estaduais.</returns>
        internal static List<FeriadoCelebrado> ImportarFeriadosEstaduais(int anoFeriado, string abrangencia = "Estadual", string pais = "BR")
        {
            List<FeriadoCelebrado> feriadosEstaduais = [];

            try
            {
                string startupPath = AppDomain.CurrentDomain.BaseDirectory;
                string arquivo = Path.Combine(startupPath, _feriadosEstaduaisCsv);
                string[,]? planilha = CsvHelper.LoadCsv(arquivo);

                if (planilha != null && planilha.GetLength(0) > 0)
                {
                    const int coluna1 = 0;
                    const int coluna2 = 1;
                    const int coluna3 = 2;
                    const int coluna4 = 3;
                    FeriadoCelebrado feriado;
                    feriadosEstaduais = new List<FeriadoCelebrado>();

                    if (anoFeriado < 1900)
                    {
                        anoFeriado = DateTime.Now.Year;
                    }

                    for (int linha = 0, totalDeLinhas = planilha.GetLength(0); linha < totalDeLinhas; linha++)
                    {
                        if (!string.IsNullOrEmpty(planilha[linha, coluna1]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna2]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna3]) &&
                            !string.IsNullOrEmpty(planilha[linha, coluna4]))
                        {
                            int dia = string.IsNullOrEmpty(planilha[linha, coluna1]) ? 1 : int.Parse((planilha[linha, coluna1]).Trim());
                            int mes = string.IsNullOrEmpty(planilha[linha, coluna2]) ? 1 : int.Parse((planilha[linha, coluna2]).Trim());
                            string estado = string.IsNullOrEmpty(planilha[linha, coluna3]) ? "" : (planilha[linha, coluna3]).Trim().ToUpper();
                            string evento = string.IsNullOrEmpty(planilha[linha, coluna4]) ? "" : (planilha[linha, coluna4]).Trim();

                            if (!string.IsNullOrEmpty(evento))
                            {
                                DateTime dataFeriado = new DateTime(anoFeriado, mes, dia);

                                var codigoFederal = "";
                                var codigoIbge = "";

                                feriado = FeriadoCelebrado.CriarFeriado(dataFeriado, evento, abrangencia, pais, estado, codigoFederal, codigoIbge, false, false);

                                feriadosEstaduais.Add(feriado);
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return feriadosEstaduais;
        }

        /// <summary>
        /// Este método libera a memória utilizada para a carga da planilha de feriados estaduais.
        /// </summary>
        /// <param name="feriadosEstaduais">Lista contendo os feriados estaduais.</param>
        internal static void Dispose(List<FeriadoCelebrado> feriadosEstaduais)
        {
            if (feriadosEstaduais != null)
            {
                feriadosEstaduais.Clear();
                feriadosEstaduais = new List<FeriadoCelebrado>();
            }
        }
    }
}
