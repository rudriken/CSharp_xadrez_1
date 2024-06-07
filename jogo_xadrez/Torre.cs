using tabuleiro;

namespace jogo_xadrez
{
    class Torre(Tabuleiro tabuleiro, Cor cor) : Peca(tabuleiro, cor)
    {
        public override string ToString()
        {
            return "T";
        }
    }
}
