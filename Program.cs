using tabuleiro;
using xadrez_jogo;

namespace xadrez
{
    class Program
    {
        static void Main()
        {
            Tabuleiro tabuleiro;

            tabuleiro = new(8, 8);

            try
            {
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1, 3));
                tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(0, 2));

                tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branca), new Posicao(3, 5));

                Tela.ImprimirTabuleiro(tabuleiro);

                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}