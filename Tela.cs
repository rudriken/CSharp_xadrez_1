using tabuleiro;
using xadrez_jogo;

namespace xadrez
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tabuleiro);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);

            if (!partida.Terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);

                if (partida.Xeque)
                    Console.WriteLine("XEQUE! ");
            } else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.JogadorAtual);
            }
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            ConsoleColor aux = Console.ForegroundColor;

            Console.WriteLine("Peças capturadas: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nPretas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");

            foreach (Peca x in conjunto)
                Console.Write(x + " ");

            Console.Write("]");
        }

        /*
         * Imprime na tela todo o tabuleiro com as peças.
         */
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tabuleiro.Colunas; j++)
                    ImprimirPeca(tabuleiro.RetornarUmaPeca(i, j));

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        /*
         * Imprime na tela todo o tabuleiro com as peças, e as posições possíveis 
         * quando uma peça já foi escolhida para iniciar o movimento.
         */
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                        Console.BackgroundColor = fundoAlterado;
                    else
                        Console.BackgroundColor = fundoOriginal;

                    ImprimirPeca(tabuleiro.RetornarUmaPeca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        /*
         * Recebe a posição de uma peça na notação padrão de um jogo de zadrez.
         */
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

        /*
         * Imprime a peça com a sua cor e um espaço. Mas se não tiver peça, imprimirá "- ".
         * Peça branca: será amarela.
         * Peça preta: será azul.
         */
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
                Console.Write(" ");
            }
            else
            {
                Console.Write("- ");
            }
        }
    }
}
