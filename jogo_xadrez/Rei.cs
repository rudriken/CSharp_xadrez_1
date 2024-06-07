using tabuleiro;

namespace jogo_xadrez
{
    class Rei(Tabuleiro tabuleiro, Cor cor) : Peca(tabuleiro, cor)
    {
        public override string ToString()
        {
            return "R";
        }
    }
}
