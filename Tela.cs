using tabuleiro;
using xadrez_jogo;

namespace xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            Peca? peca;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    peca = tabuleiro.RetornarUmaPeca(i, j);

                    if (peca == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(tabuleiro.RetornarUmaPeca(i, j));
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string? s = Console.ReadLine();
            char coluna;
            int linha;

            if (s != null)
            {
                coluna = s[0];
                linha = int.Parse(s[1] + "");
                return new PosicaoXadrez(coluna, linha);
            }

            return new PosicaoXadrez('a', 0);
        } 

        public static void ImprimirPeca(Peca? peca)
        {
            ConsoleColor aux;

            if (peca != null)
            {
                if (peca.Cor == Cor.Branca)
                {
                    aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                else
                {
                    aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
            }
        }
    }
}
