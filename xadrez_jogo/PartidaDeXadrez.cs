using tabuleiro;

namespace xadrez_jogo
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }

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
        }

        /*
         * Executa o movimento da peça, incrementa o turno e altera o jogador.
         * Se eu ficar em xeque com a minha própria jogada o movimento será desfeito. 
         */
        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca? pecaCapturada = ExecutarMovimento(origem, destino);

            // eu não posso ficar em xeque com a minha própria jogada ...
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            // ... mas o adversário, sim, pode ficar em xeque com a minha jogada.
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
        private Cor Adversaria(Cor cor)
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
            ColocarNovaPeca('c', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 7, new Torre(Tabuleiro, Cor.Branca));

            ColocarNovaPeca('a', 8, new Rei(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Torre(Tabuleiro, Cor.Preta));
        }
    }
}
