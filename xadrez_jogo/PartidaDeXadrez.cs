﻿using tabuleiro;

namespace xadrez_jogo
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get; private set; }

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
