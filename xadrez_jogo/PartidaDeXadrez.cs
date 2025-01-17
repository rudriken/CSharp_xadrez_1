﻿using tabuleiro;

namespace xadrez_jogo
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private readonly HashSet<Peca> Pecas;
        private readonly HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }
        public Peca? VulneravelEnPassant { get; private set; }

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
            Xeque = false;
            VulneravelEnPassant = null;
            Pecas = [];
            Capturadas = [];
            ColocarPecas();
        }

        public Peca? ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca? p, pecaCapturada;

            p = Tabuleiro.RetirarPeca(origem);
            p?.IncrementarQteMovimentos();
            pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);

            if (pecaCapturada != null)
                Capturadas.Add(pecaCapturada);

            // #jogadaespecial: roque
            /* Jogada em que tanto o Rei quanto a Torre movimentam casas entre si 
             * cada um, tendo as suas posições invertidas. Essa jogada pode ocorrer 
             * somente quando: 
             *  1 -> for o primeiro movimento do Rei e da Torre;
             *  2 -> as casas entre o Rei e a Torre estão vagas;
             *  3 -> o Rei não pode ficar em xeque.
             */

            // #jogadaespecial: roque pequeno
            /* 
             * O Rei movimenta duas casas à direita e a Torre movimenta duas  
             * casas à esquerda ficando à esquerda do Rei 
             * (antes a Torre estava três casas à direita do Rei).
             */
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna + 1);
                Peca? t = Tabuleiro.RetirarPeca(origemTorre);
                t?.IncrementarQteMovimentos();
                Tabuleiro.ColocarPeca(t, destinoTorre);
            }

            // #jogadaespecial: roque grande
            /* 
             * O Rei movimenta duas casas à esquerda e a Torre movimenta três  
             * casas à direita ficando à direita do Rei 
             * (antes a Torre estava quatro casas à esquerda do Rei).
             */
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna - 1);
                Peca? t = Tabuleiro.RetirarPeca(origemTorre);
                t?.IncrementarQteMovimentos();
                Tabuleiro.ColocarPeca(t, destinoTorre);
            }

            // #jogadaespecial: en-passant
            // Peão branco: sempre pode ocorrer na linha 3(5) para a 2(6) na matriz,
            // e vai para a coluna do Peão preto capturado, e uma linha acima.
            // Peão preto: sempre pode ocorrer na linha 4(4) para a 5(3) na matriz,
            // e vai para a coluna do Peão branco capturado, e uma linha abaixo.
            /* 
             *  1 -> se as duas peças envolvidas for um Peão e de cores diferentes;
             *  2 -> se o Peão a ser capturado for a primeira vez que se movimenta;
             *  3 -> se o Peão a ser capturado se movimentou duas casas;
             *  4 -> se o Peão a ser capturado está do lado do Peão que vai capturar.
             */
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posDoPeaoCapturado;

                    if (p.Cor == Cor.Branca)
                        posDoPeaoCapturado = new(destino.Linha + 1, destino.Coluna);
                    else
                        posDoPeaoCapturado = new(destino.Linha - 1, destino.Coluna);

                    pecaCapturada = Tabuleiro.RetirarPeca(posDoPeaoCapturado);

                    if (pecaCapturada != null)
                        Capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        /*
         * Desfaz o movimento da peça, colocando-a na sua posição original, e se houver 
         * captura de peça oponente, essa é devolvida ao seu lugar no tabuleiro. 
         */
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca? pecaCapturada)
        {
            Peca? p = Tabuleiro.RetirarPeca(destino);
            p?.DecrementarQteMovimentos();

            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }

            Tabuleiro.ColocarPeca(p, origem);

            // #jogadaespecial: roque pequeno
            /* 
             * O Rei e a Torre tem que voltar para as suas posições originais.
             */
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna + 1);
                Peca? t = Tabuleiro.RetirarPeca(destinoTorre);
                t?.DecrementarQteMovimentos();
                Tabuleiro.ColocarPeca(t, origemTorre);
            }

            // #jogadaespecial: roque grande
            /* 
             * O Rei e a Torre tem que voltar para as suas posições originais.
             */
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna - 1);
                Peca? t = Tabuleiro.RetirarPeca(destinoTorre);
                t?.DecrementarQteMovimentos();
                Tabuleiro.ColocarPeca(t, origemTorre);
            }

            // #jogadaespecial: en-passant
            /* 
             * O Peão que capturou e o Peão capturado tem que voltar para as suas 
             * posições originais. 
             */
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca? peao = Tabuleiro.RetirarPeca(destino);
                    Posicao posPeao;

                    if (p.Cor == Cor.Branca)
                        posPeao = new(3, destino.Coluna);
                    else
                        posPeao = new(4, destino.Coluna);

                    Tabuleiro.ColocarPeca(peao, posPeao);
                }
            }
        }

        /*
         * Executa o movimento da peça, incrementa o turno e altera o jogador.
         * Se eu ficar em xeque com a minha própria jogada o movimento será desfeito. 
         */
        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca? pecaCapturada = ExecutarMovimento(origem, destino);
            Peca? p = Tabuleiro.RetornarUmaPeca(destino);

            // eu não posso ficar em xeque com a minha própria jogada, portanto ...
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            // #jogadaespecial: promoção
            /* 
             * Um peão, ao alcançar a última linha do tabuleiro é promovido, 
             * o jogador é obrigado a escolher entre uma das seguintes peças para 
             * substituí-lo: Dama, Torre, Bispo ou Cavalo. Dama costuma ser o padrão.
             */
            if (p is Peao)
            {
                if (
                    (p.Cor == Cor.Branca && destino.Linha == 0) || 
                    (p.Cor == Cor.Preta && destino.Linha == 7)
                )
                {
                    p = Tabuleiro.RetirarPeca(destino);

                    if (p != null)
                    {
                        Pecas.Remove(p);
                        Peca dama = new Dama(Tabuleiro, p.Cor);
                        Tabuleiro.ColocarPeca(dama, destino);
                        Pecas.Add(dama);
                    }
                }
            }

            // o adversário, sim, pode ficar em xeque com a minha jogada, portanto ...
            if (EstaEmXeque(Adversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            if (TesteXequeMate(Adversaria(JogadorAtual)))
                Terminada = true;
            else
            {
                Turno++;
                MudarJogador();
            }

            // #jogadaespecial: en-passant
            // Peão branco: sempre pode ocorrer na linha 3(5) para a 2(6) na matriz,
            // e vai para a coluna do Peão preto capturado.
            // Peão preto: sempre pode ocorrer na linha 4(4) para a 5(3) na matriz,
            // e vai para a coluna do Peão branco capturado.
            /* 
             *  1 -> se as duas peças envolvidas for um Peão e de cores diferentes;
             *  2 -> se o Peão a ser capturado for a primeira vez que se movimenta;
             *  3 -> se o Peão a ser capturado se movimentou duas casas;
             *  4 -> se o Peão a ser capturado está do lado do Peão que vai capturar.
             */
            
            if (
                p is Peao &&
                (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2)
            )
                VulneravelEnPassant = p;
            else
                VulneravelEnPassant = null;
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
                throw new TabuleiroException(
                    "Não existe peça na posição de origem escolhida!"
                );

            if (JogadorAtual != Tabuleiro.RetornarUmaPeca(origem)?.Cor)
                throw new TabuleiroException(
                    "A peça de origem escolhida não é sua!"
                );

            if (!Tabuleiro.RetornarUmaPeca(origem)?.ExisteMovimentosPossiveis() ?? false)
                throw new TabuleiroException(
                    "Não há movimentos possíveis para a peça de origem escolhida!"
                );
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.RetornarUmaPeca(origem)?.MovimentoPossivel(destino) ?? false)
                throw new TabuleiroException("Posição de destino inválida!");
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
        * Retorna somente as peças capturadas da cor escolhida. 
        */
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = [];

            foreach (Peca x in Capturadas)
                if (x.Cor == cor)
                    aux.Add(x);

            return aux;
        }

        /*
        * Retorna somente as peças em jogo da cor escolhida. 
        */
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = [];

            foreach (Peca x in Pecas)
                if (x.Cor == cor)
                    aux.Add(x);

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        /*
        * Define o adversário do jogador atual pela cor da peça.
        */
        private static Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
                return Cor.Preta;

            return Cor.Branca;
        }

        /*
        * Pega o rei de uma cor.
        */
        private Peca? PegarRei(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
                if (peca is Rei)
                    return peca;

            return null;
        }

        /*
        * Verifica se o rei de uma determinada cor está ou não em xeque, verificando todas
        * os movimentos possíveis de todas as peças em jogo.
        */
        public bool EstaEmXeque(Cor cor)
        {
            bool[,] movPossiveisDaPecaEmJogo;
            Peca? rei = PegarRei(cor)
                ?? throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro! ");

            if (rei.Posicao != null)
                foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
                {
                    movPossiveisDaPecaEmJogo = peca.MovimentosPossiveis();

                    if (movPossiveisDaPecaEmJogo[rei.Posicao.Linha, rei.Posicao.Coluna])
                        return true;
                }

            return false;
        }

        /*
        * Verifica se não há mais movimentos possíveis para salvar o Rei, ou seja, se todas 
        * as peças do jogador atual são incapazes de tirar o seu Rei do "xeque".
        * Se não houver mais, significa "xeque mate".
        */
        public bool TesteXequeMate(Cor cor)
        {
            bool[,] movPossiveisDaPecaEmJogo;
            Peca? pecaCapturada;
            Posicao origem, destino;
            bool testeXeque;

            if (!EstaEmXeque(cor))
                return false;

            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca.Posicao == null)
                    continue;

                movPossiveisDaPecaEmJogo = peca.MovimentosPossiveis();

                for (int i = 0; i < Tabuleiro.Linhas; i++)
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                        if (movPossiveisDaPecaEmJogo[i, j])
                        {
                            origem = peca.Posicao;
                            destino = new Posicao(i, j);
                            pecaCapturada = ExecutarMovimento(origem, destino);
                            testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque)
                                return false;
                        }
            }

            return true;
        }

        /*
         * Coloca uma peça no tabuleiro na posição escolhida.
         */
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        /*
         * Coloca as peças no tabuleiro.
         */
        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));

            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));

        }
    }
}
