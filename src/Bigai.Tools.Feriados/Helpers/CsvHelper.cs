using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Bigai.Tools.Feriados.Tests")]
namespace Bigai.Tools.Feriados.Helpers;

/// <summary>
/// CsvHelper fornece metodos para a leitura de arquivos no formato CSV e conversão para array de strings.
/// </summary>
internal static class CsvHelper
{
    /// <summary>
    /// Este método faz a leitura de um arquivo CSV e retorna o seu conteúdo em um array de linhas e colunas.
    /// </summary>
    /// <param name="fileName">Path contendo o nome do arquivo CSV para leitura.</param>
    /// <returns>Retorna um array multidimensional com as linhas e colunas que foram lidas do arquivo CSV.</returns>
    /// <remarks>Para criar as colunas é assumido que cada linha do arquivo possui o mesmo número de colunas.</remarks>
    internal static string[,]? LoadCsv(string fileName)
    {
        string[,]? planilha = null;
        try
        {
            string? todoConteudo = LerArquivo(fileName);

            if (!string.IsNullOrEmpty(todoConteudo))
            {
                string[] linhas = ConverterEmLinhas(todoConteudo);
                planilha = CriarArray(linhas);
            }
        }
        catch (Exception)
        {
            throw;
        }

        return planilha;
    }

    /// <summary>
    /// Este método faz a leitura de todo o conteúdo do arquivo CSV de uma única vez.
    /// </summary>
    /// <param name="pathFile">Path contendo o nome do arquivo CSV para leitura.</param>
    /// <returns>Retorna uma string com todo o conteúdo do arquivo. Em caso de erro retorna <c>null</c>.</returns>
    internal static string? LerArquivo(string pathFile)
    {
        string? todoArquivo = null;

        try
        {
            if (File.Exists(pathFile))
            {
                var fileStream = new FileStream(pathFile, FileMode.Open, FileAccess.Read);

                using var streamReader = new StreamReader(fileStream, Encoding.GetEncoding("iso-8859-1"));
                todoArquivo = streamReader.ReadToEnd();
            }
        }
        catch (Exception)
        {
            throw;
        }

        return todoArquivo;
    }

    /// <summary>
    /// Este método retorna uma matriz de linhas quebrada pelo caracter '\r'.
    /// </summary>
    /// <param name="conteudo">String contendo o conteúdo que será "splitado".</param>
    /// <returns>Retorna o conteúdo no formato de um array de linhas.</returns>
    internal static string[] ConverterEmLinhas(string conteudo)
    {
        conteudo = conteudo.Replace('\n', '\r');

        return conteudo.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
    }

    /// <summary>
    /// Este método converte um array de linhas em um array de linhas e colunas.
    /// </summary>
    /// <param name="linhasDaPlanilha">Array com as linhas da planilha.</param>
    /// <returns>Retorna o conteúdo da planilha em um array de linhas e colunas.</returns>
    private static string[,]? CriarArray(string[] linhasDaPlanilha)
    {
        string[,]? planilha = null;

        if (linhasDaPlanilha != null && linhasDaPlanilha.Length > 0)
        {
            //
            // Subtrai 1 para desprezar a linha do header da planilha.
            //
            int numeroLinhas = linhasDaPlanilha.Length - 1;

            //
            // Assume que todas as linhas tem o mesmo número de colunas.
            //
            int numeroColunas = linhasDaPlanilha[0].Split(';').Length;

            //
            // Cria array para reproduzir a planilha.
            //
            planilha = new string[numeroLinhas, numeroColunas];

            //
            // Carrega o conteúdo da planilha.
            //
            for (int linha = 0; linha < numeroLinhas; linha++)
            {
                string[] colunas = linhasDaPlanilha[linha + 1].Split(';');

                for (int coluna = 0; coluna < numeroColunas; coluna++)
                {
                    planilha[linha, coluna] = colunas[coluna];
                }
            }
        }

        return planilha;
    }
}
