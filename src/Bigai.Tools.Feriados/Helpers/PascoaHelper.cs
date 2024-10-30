using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Bigai.Tools.Feriados.Tests")]
namespace Bigai.Tools.Feriados.Helpers;

/// <summary>
/// Pascoahelper com métodos para identificar a data em que ocorre a Páscoa, Ressureição de Nosso Senhor Jesus Cristo,
/// utilizando o algoritmo de Gauss.
/// </summary>
internal static class PascoaHelper
{
    private static int AnoInicial { get; set; }

    private static int AnoFinal { get; set; }

    private static int X { get; set; }

    private static int Y { get; set; }

    private static readonly int[,] _fatorGauss =
    {
         // AnoInicial, AnoFinal, X, Y
         { 1582, 1599, 22, 2 },
         { 1600, 1699, 22, 2 },
         { 1700, 1799, 23, 3 },
         { 1800, 1899, 24, 4 },
         { 1900, 1999, 24, 5 },
         { 2000, 2099, 24, 5 },
         { 2100, 2199, 24, 6 },
         { 2200, 2299, 25, 7 }
     };

    /// <summary>
    /// Determina a data em que ocorre a celebração da Páscoa.
    /// </summary>
    /// <param name="ano">Representa o ano em que se deseja conhecer a data das Páscoa.</param>
    /// <returns>Retorna a data em que a Páscoa ocorre para o ano solicitado.</returns>
    /// <remarks>Quando o domingo de Páscoa calculado for 26/4, corrige-se para uma semana antes, ou seja, 19 de Abril (ocorre em 2076).</remarks>
    /// <remarks>Quando o domingo de Páscoa calculado for 25/4 e 'd' = 28 e 'a' > 10, então a Páscoa é em 18 de Abril (ocorre em 2049)</remarks>
    internal static DateTime Pascoa(this int ano)
    {
        DateTime dataPascoa;

        SetFaixaByAno(ano);

        if (AnoInicial != 0)
        {
            int a = ano % 19;
            int b = ano % 4;
            int c = ano % 7;
            int d = (19 * a + X) % 30;
            int e = (6 * d + 4 * c + 2 * b + Y) % 7;
            dataPascoa = new DateTime(ano, 3, 22).AddDays(d + e);
            int dia = dataPascoa.Day;
            switch (dia)
            {
                case 26:
                    dataPascoa = new DateTime(ano, 4, 19);
                    break;
                case 25:
                    if (a > 10)
                        dataPascoa = new DateTime(ano, 4, 18);
                    break;
            }
        }
        else
            dataPascoa = new DateTime(1, 1, 1);

        return dataPascoa.Date;
    }

    /// <summary>
    /// Esta função retorna a faixa em que o ano solicitado se encaixa.
    /// </summary>
    /// <param name="ano">ano que deve ser pesquisado no repositório.</param>
    internal static void SetFaixaByAno(int ano)
    {
        AnoFinal = X = Y = 0;

        for (int i = 0, j = _fatorGauss.GetLength(0); i < j; i++)
        {
            if (ano >= _fatorGauss[i, 0] && ano <= _fatorGauss[i, 1])
            {
                AnoInicial = _fatorGauss[i, 0];
                AnoFinal = _fatorGauss[i, 1];
                X = _fatorGauss[i, 2];
                Y = _fatorGauss[i, 3];
                i = j;
            }
        }
    }
}
