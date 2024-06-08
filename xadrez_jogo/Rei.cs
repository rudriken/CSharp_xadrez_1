using tabuleiro;

namespace xadrez_jogo
{
    class Rei(Tabuleiro tab, Cor cor) : Peca(tab, cor)
    {
        public override string ToString()
        {
            return "R";
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
         * Retorna uma matriz mostrando todos as posições possíveis do Rei no tabuleiro.
         */
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new(0, 0);

            // "norte"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);

                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
            }

            // "nordeste"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);

                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
            }

            // "leste"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);

                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                }
            }

            // "sudeste"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);

                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                }
            }

            // "sul"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);

                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                }
            }

            // "sudoeste"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);

                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                }
            }

            // "oeste"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);

                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                }
            }

            // "noroeste"
            if (Posicao != null)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);

                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                }
            }

            return matriz;
        }
    }
}
