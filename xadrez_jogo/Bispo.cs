using tabuleiro;

namespace xadrez_jogo
{
    class Bispo(Tabuleiro tab, Cor cor) : Peca(tab, cor)
    {
        public override string ToString()
        {
            return "B";
        }

        /*
         * Verifica se o Bispo pode ir numa determinada posição, ou seja, 
         * se tal posição está vaga ou se tem peça oponente.
         */
        private bool PodeMover(Posicao posicao)
        {
            Peca? pecaPerto = tab.RetornarUmaPeca(posicao);
            return pecaPerto == null || pecaPerto.Cor != cor;
        }

        /*
         * Retorna uma matriz mostrando todos as posições possíveis do Bispo no tabuleiro.
         */
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new(0, 0);

            if (Posicao != null)
            {
                // "nordeste"
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                        break;

                    pos.Linha--;
                    pos.Coluna++;
                }

                // "sudeste"
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                        break;

                    pos.Linha++;
                    pos.Coluna++;
                }

                // "sudoeste"
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                        break;

                    pos.Linha++;
                    pos.Coluna--;
                }

                // "noroeste"
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                        break;

                    pos.Linha--;
                    pos.Coluna--;
                }
            }

            return matriz;
        }
    }
}
