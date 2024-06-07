using tabuleiro;

namespace xadrez_jogo
{
    class Rei(Tabuleiro tabuleiro, Cor cor) : Peca(tabuleiro, cor)
    {
        public override string ToString()
        {
            return "R";
        }
    }
}
