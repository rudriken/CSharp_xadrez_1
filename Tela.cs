using tabuleiro;

namespace xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            Peca peca;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    peca = tabuleiro.RetornarUmaPeca(i, j);

                    if (peca == null)
                        Console.Write("- ");
                    else
                        Console.Write(tabuleiro.RetornarUmaPeca(i, j) + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
