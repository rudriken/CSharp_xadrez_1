using tabuleiro;

namespace xadrez_jogo
{
    class Cavalo(Tabuleiro tab, Cor cor) : Peca(tab, cor)
    {
        public override string ToString()
        {
            return "C";
        }

        /*
         * Verifica se o Cavalo pode ir numa determinada posição, ou seja, 
         * se tal posição está vaga ou se tem peça oponente.
         */
        private bool PodeMover(Posicao posicao)
        {
            Peca? pecaPerto = tab.RetornarUmaPeca(posicao);
            return pecaPerto == null || pecaPerto.Cor != cor;
        }

        /*
         * Retorna uma matriz mostrando todos as posições possíveis do Cavalo no tabuleiro.
         */
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new(0, 0);

            if (Posicao != null)
            {
                // ###
                //   #
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                //  #
                //  #
                // ##
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // #
                // ###
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // ##
                // #
                // #
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // ###
                // #
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // ##
                //  #
                //  #
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                //   #
                // ###
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // #
                // #
                // ##
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;
            }

            return matriz;
        }
    }
}
