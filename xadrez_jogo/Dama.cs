using tabuleiro;

namespace xadrez_jogo
{
    class Dama(Tabuleiro tab, Cor cor) : Peca(tab, cor)
    {
        public override string ToString()
        {
            return "D";
        }

        /*
         * Verifica se a Dama pode ir numa determinada posição, ou seja, 
         * se tal posição está vaga ou se tem peça oponente.
         */
        private bool PodeMover(Posicao posicao)
        {
            Peca? pecaPerto = tab.RetornarUmaPeca(posicao);
            return pecaPerto == null || pecaPerto.Cor != cor;
        }

        /*
         * Retorna uma matriz mostrando todos as posições possíveis da Dama no tabuleiro.
         */
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new(0, 0);

            if (Posicao != null)
            {
                // "norte"
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                        break;

                    pos.Linha--;
                }

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

                // "leste"
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                        break;

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

                // "sul"
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                        break;

                    pos.Linha++;
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

                // "oeste"
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
                while (tab.VerificarPosicao(pos) && PodeMover(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;

                    if (
                        tab.RetornarUmaPeca(pos) != null &&
                        tab.RetornarUmaPeca(pos)?.Cor != cor
                    )
                        break;

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
