using tabuleiro;

namespace xadrez_jogo
{
    class Peao(Tabuleiro tab, Cor cor) : Peca(tab, cor)
    {
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
         * Verifica se o Peão pode ir numa determinada posição, ou seja, 
         * se tal posição está vaga ou se tem peça oponente.
         */
        private bool PodeMover(Posicao posicao)
        {
            Peca? pecaPerto = tab.RetornarUmaPeca(posicao);
            return pecaPerto == null || pecaPerto.Cor != cor;
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

                    // Branco indo 2 casas para "norte" se ela estiver livre e 1º movimento
                    pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                    if (Tabuleiro.VerificarPosicao(pos) && Livre(pos) && QteMovimentos == 0)
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Branco indo 1 casa para "noroeste" se nela existir inimigo
                    pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                    if (Tabuleiro.VerificarPosicao(pos) && ExisteInimigo(pos))
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Branco indo 1 casa para "nordeste" se nela existir inimigo
                    pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                    if (Tabuleiro.VerificarPosicao(pos) && ExisteInimigo(pos))
                        matriz[pos.Linha, pos.Coluna] = true;
                }
                else
                {
                    // Preto indo 1 casa para "sul" se ela estiver livre
                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                    if (Tabuleiro.VerificarPosicao(pos) && Livre(pos))
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Preto indo 2 casas para "sul" se ela estiver livre e 1º movimento
                    pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                    if (Tabuleiro.VerificarPosicao(pos) && Livre(pos) && QteMovimentos == 0)
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Preto indo 1 casa para "sudoeste" se nela existir inimigo
                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                    if (Tabuleiro.VerificarPosicao(pos) && ExisteInimigo(pos))
                        matriz[pos.Linha, pos.Coluna] = true;

                    // Preto indo 1 casa para "sudeste" se nela existir inimigo
                    pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                    if (Tabuleiro.VerificarPosicao(pos) && ExisteInimigo(pos))
                        matriz[pos.Linha, pos.Coluna] = true;
                }
            }

            return matriz;
        }
    }
}
