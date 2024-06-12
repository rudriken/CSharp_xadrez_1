using tabuleiro;

namespace xadrez_jogo
{
    class Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : Peca(tab, cor)
    {
        private readonly PartidaDeXadrez Partida = partida;

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca? p = Tabuleiro.RetornarUmaPeca(pos);
            return p != null && p.Cor != cor;
        }

        private bool Livre(Posicao pos)
        {
            return Tabuleiro.RetornarUmaPeca(pos) == null;
        }

        /*
         * Retorna uma matriz mostrando todos as posições possíveis do Peão no tabuleiro.
         */
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new(0, 0);

            if (Posicao != null)
            {
                if (Cor == Cor.Branca)
                {
                    // Branco indo 1 casa para "norte" se ela estiver livre
                    pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                    if (Tabuleiro.VerificarPosicao(pos) && Livre(pos))
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Branco indo 2 casas para "norte" se ela estiver livre e 1º movimento, 
                    // e também se a próxima casa do "norte" estiver livre.
                    pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                    if (
                        Tabuleiro.VerificarPosicao(pos) &&
                        Livre(pos) &&
                        QteMovimentos == 0 &&
                        Livre(new Posicao(Posicao.Linha - 1, Posicao.Coluna))
                    )
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Branco indo 1 casa para "noroeste" se nela existir inimigo
                    pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                    if (Tabuleiro.VerificarPosicao(pos) && ExisteInimigo(pos))
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Branco indo 1 casa para "nordeste" se nela existir inimigo
                    pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                    if (Tabuleiro.VerificarPosicao(pos) && ExisteInimigo(pos))
                        matriz[pos.Linha, pos.Coluna] = true;

                    // #jogadaespecial: en-passant
                    // Peão branco: sempre pode ocorrer na linha 3(5) para a 2(6) na matriz,
                    // e vai para a coluna do Peão preto capturado, e uma linha acima.
                    /* 
                     *  1 -> se as duas peças envolvidas for um Peão e de cores diferentes;
                     *  2 -> se o Peão a ser capturado for a primeira vez que se movimenta;
                     *  3 -> se o Peão a ser capturado se movimentou duas casas;
                     *  4 -> se o Peão a ser capturado está do lado do Peão que vai capturar.
                     */
                    if (Posicao.Linha == 3)
                    {
                        // posição do Peão preto à esquerda que está vulnerável
                        Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);

                        if (
                            tab.VerificarPosicao(esquerda) &&
                            ExisteInimigo(esquerda) &&
                            tab.RetornarUmaPeca(esquerda) == Partida.VulneravelEnPassant
                        )
                            matriz[esquerda.Linha - 1, esquerda.Coluna] = true;

                        // posição do Peão preto à direita que está vulnerável
                        Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);

                        if (
                            tab.VerificarPosicao(direita) &&
                            ExisteInimigo(direita) &&
                            tab.RetornarUmaPeca(direita) == Partida.VulneravelEnPassant
                        )
                            matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
                else
                {
                    // Preto indo 1 casa para "sul" se ela estiver livre
                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                    if (Tabuleiro.VerificarPosicao(pos) && Livre(pos))
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Preto indo 2 casas para "sul" se ela estiver livre e 1º movimento, 
                    // e também se a próxima casa do "sul" estiver livre.
                    pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                    if (
                        Tabuleiro.VerificarPosicao(pos) && 
                        Livre(pos) && 
                        QteMovimentos == 0 && 
                        Livre(new Posicao(Posicao.Linha + 1, Posicao.Coluna))
                    )
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Preto indo 1 casa para "sudoeste" se nela existir inimigo
                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                    if (Tabuleiro.VerificarPosicao(pos) && ExisteInimigo(pos))
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Preto indo 1 casa para "sudeste" se nela existir inimigo
                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                    if (Tabuleiro.VerificarPosicao(pos) && ExisteInimigo(pos))
                        matriz[pos.Linha, pos.Coluna] = true;

                    // #jogadaespecial: en-passant
                    // Peão preto: sempre pode ocorrer na linha 4(4) para a 5(3) na matriz,
                    // e vai para a coluna do Peão branco capturado, e uma linha abaixo.
                    /* 
                     *  1 -> se as duas peças envolvidas for um Peão e de cores diferentes;
                     *  2 -> se o Peão a ser capturado for a primeira vez que se movimenta;
                     *  3 -> se o Peão a ser capturado se movimentou duas casas;
                     *  4 -> se o Peão a ser capturado está do lado do Peão que vai capturar.
                     */
                    if (Posicao.Linha == 4)
                    {
                        // posição do Peão branco à esquerda que está vulnerável
                        Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);

                        if (
                            tab.VerificarPosicao(esquerda) &&
                            ExisteInimigo(esquerda) &&
                            tab.RetornarUmaPeca(esquerda) == Partida.VulneravelEnPassant
                        )
                            matriz[esquerda.Linha + 1, esquerda.Coluna] = true;

                        // posição do Peão branco à direita que está vulnerável
                        Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);

                        if (
                            tab.VerificarPosicao(direita) &&
                            ExisteInimigo(direita) &&
                            tab.RetornarUmaPeca(direita) == Partida.VulneravelEnPassant
                        )
                            matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return matriz;
        }
    }
}
