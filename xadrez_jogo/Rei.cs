using tabuleiro;

namespace xadrez_jogo
{
    class Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : Peca(tab, cor)
    {
        private readonly PartidaDeXadrez Partida = partida;

        public override string ToString()
        {
            return "R";
        }

        /*
         * Verifica se o Rei pode ir numa determinada posição, ou seja, 
         * se tal posição está vaga ou se tem peça oponente.
         */
        private bool PodeMover(Posicao posicao)
        {
            Peca? pecaPerto = tab.RetornarUmaPeca(posicao);
            return pecaPerto == null || pecaPerto.Cor != cor;
        }

        /*
         * Verifica se a Torre é elegível para a jogada especial roque.
         */
        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca? p = tab.RetornarUmaPeca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QteMovimentos == 0;
        }

        /*
         * Retorna uma matriz mostrando todos as posições possíveis do Rei no tabuleiro.
         */
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new(0, 0);

            if (Posicao != null)
            {
                // "norte"
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // "nordeste"
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // "leste"
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // "sudeste"
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // "sul"
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // "sudoeste"
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // "oeste"
                pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // "noroeste"
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (tab.VerificarPosicao(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // #jogadaespecial: roque
                /* Jogada em que tanto o Rei quanto a Torre movimentam casas entre si 
                 * cada um, tendo as suas posições invertidas. Essa jogada pode ocorrer 
                 * somente quando: 
                 *  1 -> for o primeiro movimento do Rei e da Torre;
                 *  2 -> as casas entre o Rei e a Torre estão vagas;
                 *  3 -> o Rei não pode ficar em xeque.
                 */
                if (QteMovimentos == 0 && !Partida.Xeque)
                {
                    // #jogadaespecial: roque pequeno
                    /* 
                     * O Rei movimenta duas casas à direita e a Torre movimenta duas  
                     * casas à esquerda ficando à esquerda do Rei 
                     * (antes a Torre estava três casas à direita do Rei).
                     */
                    Posicao posTorreP = new(Posicao.Linha, Posicao.Coluna + 3);

                    if (TesteTorreParaRoque(posTorreP))
                    {
                        Posicao p1 = new(Posicao.Linha, Posicao.Coluna + 1);
                        Posicao p2 = new(Posicao.Linha, Posicao.Coluna + 2);

                        if (
                            tab.RetornarUmaPeca(p1) == null &&
                            tab.RetornarUmaPeca(p2) == null
                        )
                            matriz[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }

                    // #jogadaespecial: roque grande
                    /* 
                     * O Rei movimenta duas casas à esquerda e a Torre movimenta três  
                     * casas à direita ficando à direita do Rei 
                     * (antes a Torre estava quatro casas à esquerda do Rei).
                     */
                    Posicao posTorreG = new(Posicao.Linha, Posicao.Coluna - 4);

                    if (TesteTorreParaRoque(posTorreG))
                    {
                        Posicao p1 = new(Posicao.Linha, Posicao.Coluna - 1);
                        Posicao p2 = new(Posicao.Linha, Posicao.Coluna - 2);
                        Posicao p3 = new(Posicao.Linha, Posicao.Coluna - 3);

                        if (
                            tab.RetornarUmaPeca(p1) == null &&
                            tab.RetornarUmaPeca(p2) == null && 
                            tab.RetornarUmaPeca(p3) == null
                        )
                            matriz[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return matriz;
        }
    }
}
