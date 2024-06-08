using tabuleiro;

namespace xadrez_jogo
{
    class PosicaoXadrez(char coluna, int linha)
    {
        public char Coluna { get; set; } = coluna;
        public int Linha { get; set; } = linha;

        /*
         * Converte uma posição na notação padrão de um jogo de xadrez para a notação 
         * de uma matriz na programação.
         */
        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
