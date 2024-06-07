using tabuleiro;
using xadrez_jogo;

namespace xadrez
{
    class Program
    {
        static void Main()
        {
            PosicaoXadrez posicaoXadrez = new('h', 1);
            Console.WriteLine(posicaoXadrez);

            Console.WriteLine(posicaoXadrez.ToPosicao());
        }
    }
}