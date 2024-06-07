using tabuleiro;

namespace xadrez_jogo
{
    class Torre(Tabuleiro tabuleiro, Cor cor) : Peca(tabuleiro, cor)
    {
        public override string ToString()
        {
            return "T";
        }
    }
}
