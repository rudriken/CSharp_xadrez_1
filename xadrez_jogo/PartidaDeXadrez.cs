using tabuleiro;

namespace xadrez_jogo
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        /*
         * Constrói o tabuleiro, coloca as peças, e inicia o jogo.
         */
        public PartidaDeXadrez()
        {
            Tabuleiro = new(8, 8);
            Turno = 1;
            // Regra do xadrez: o primeiro jogador é das peças brancas
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca? p, pecaCapturada;

            p = Tabuleiro?.RetirarPeca(origem);
            p?.IncrementarQteMovimentos();
            pecaCapturada = Tabuleiro?.RetirarPeca(destino);
            Tabuleiro?.ColocarPeca(p, destino);
        }

        /*
         * Executa o movimento da peça, incrementa o turno e altera o jogador.
         */
        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudarJogador();
        }

        /*
         * Lança uma exceção nas três situações para a posição de origem escolhida:
         *   1-> se não existir peça;
         *   2-> se a peça for de outra cor;
         *   3-> se a peça está bloqueada, isto é, não existir movimentos possíveis para ela.
         */
        public void ValidarPosicaoDeOrigem(Posicao origem)
        {
            if (Tabuleiro.RetornarUmaPeca(origem) == null)
            {
                throw new TabuleiroException(
                    "Não existe peça na posição de origem escolhida!"
                );
            }

            if (JogadorAtual != Tabuleiro.RetornarUmaPeca(origem)?.Cor)
            {
                throw new TabuleiroException(
                    "A peça de origem escolhida não é sua!"
                );
            }

            if (!Tabuleiro.RetornarUmaPeca(origem)?.ExisteMovimentosPossiveis() ?? false)
            {
                throw new TabuleiroException(
                    "Não há movimentos possíveis para a peça de origem escolhida!"
                );
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.RetornarUmaPeca(origem)?.PodeMoverPara(destino) ?? false)
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }


        }

        /*
         * Altera o jogador para o próximo turno.
         */
        private void MudarJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }

        /*
         * Coloca as peças no tabuleiro.
         */
        private void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Branca),
                new PosicaoXadrez('c', 1).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Branca),
                new PosicaoXadrez('c', 2).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Branca),
                new PosicaoXadrez('d', 2).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Branca),
                new PosicaoXadrez('e', 2).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Branca),
                new PosicaoXadrez('e', 1).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Rei(Tabuleiro, Cor.Branca),
                new PosicaoXadrez('d', 1).ToPosicao()
            );

            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Preta),
                new PosicaoXadrez('c', 7).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Preta),
                new PosicaoXadrez('c', 8).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Preta),
                new PosicaoXadrez('d', 7).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Preta),
                new PosicaoXadrez('e', 7).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Torre(Tabuleiro, Cor.Preta),
                new PosicaoXadrez('e', 8).ToPosicao()
            );
            Tabuleiro.ColocarPeca(
                new Rei(Tabuleiro, Cor.Preta),
                new PosicaoXadrez('d', 8).ToPosicao()
            );
        }
    }
}
