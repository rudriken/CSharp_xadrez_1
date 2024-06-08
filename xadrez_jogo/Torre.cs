using tabuleiro;

namespace xadrez_jogo
{
    class Torre(Tabuleiro tab, Cor cor) : Peca(tab, cor)
    {
        public override string ToString()
        {
            return "T";
        }

        /*
         * Verifica se uma peça pode ir numa determinada posição, ou seja, 
         * se tal posição está vaga ou se tem peça oponente.
         */
        private bool PodeMover(Posicao posicao)
        {
            Peca? pecaPerto = tab.RetornarUmaPeca(posicao);
            return pecaPerto == null || pecaPerto.Cor != cor;
        }

        /*
         * Retorna uma matriz mostrando todos as posições possíveis da Torre no tabuleiro.
         */
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new(0, 0);

            // "norte"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);

                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null && 
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                    {
                        break;
                    }

                    pos.Linha--;
                }
            }

            // "sul"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);

                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                    {
                        break;
                    }

                    pos.Linha++;
                }
            }

            // "leste"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);

                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                    {
                        break;
                    }

                    pos.Coluna++;
                }
            }

            // "oeste"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);

                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                    {
                        break;
                    }

                    pos.Coluna--;
                }
            }

            return matriz;
        }
    }
}
