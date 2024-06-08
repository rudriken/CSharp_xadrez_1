namespace tabuleiro
{
    class Tabuleiro(int linhas, int colunas)
    {
        public int Linhas { get; set; } = linhas;
        public int Colunas { get; set; } = colunas;
        private readonly Peca?[,] Pecas = new Peca[linhas, colunas];

        /*
         * Retorna uma peça que está na linha e coluna de entrada.
         * Se tal posição estiver vaga retornará 'null'.
         */
        public Peca? RetornarUmaPeca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        /*
         * Retorna uma peça que está na posição de entrada.
         * Se tal posição estiver vaga retornará 'null'.
         */
        public Peca? RetornarUmaPeca(Posicao posicao)
        {
            return Pecas[posicao.Linha, posicao.Coluna];
        }

        /*
         * Verifica se existe uma peça na posição de entrada.
         * Se sim, retornará 'true', se não, retornará 'false'.
         */
        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return RetornarUmaPeca(posicao) != null;
        }

        /*
         * Coloca uma peça no tabuleiro, e sua posição é atualizada. 
         * Contudo, se na posição de entrada já existir uma peça, a peça de entrada será 
         * ignorada e uma exceção será lançada. 
         */
        public void ColocarPeca(Peca? peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
                throw new TabuleiroException("Jé existe uma peça nessa posição!");

            Pecas[posicao.Linha, posicao.Coluna] = peca;

            if (peca != null)
                peca.Posicao = posicao;
        }

        /*
         * Retira uma peça do tabuleiro na posição de entrada, 
         * atualizando sua posição como 'null' e a peça em si como 'null', 
         * e retorna essa peça retirada.
         * Se não existir peça na posição de entrada, retornará 'null'.
         */
        public Peca? RetirarPeca(Posicao posicao)
        {
            Peca? aux;

            aux = RetornarUmaPeca(posicao);

            if (aux == null)
                return null;

            aux.Posicao = null;
            Pecas[posicao.Linha, posicao.Coluna] = null;
            return aux;
        }

        /*
         * Verifica se a posição de entrada está dentro dos parâmetros de linhas e colunas 
         * definidos na construção do tabuleiro.
         */
        public bool VerificarPosicao(Posicao posicao)
        {
            if (
                posicao.Linha < 0 || posicao.Linha >= Linhas ||
                posicao.Coluna < 0 || posicao.Coluna >= Colunas
            )
                return false;

            return true;
        }

        /*
         * Lança uma exceção se a posição de entrada não estiver dentro dos parâmetros 
         * definidos na construção do tabuleiro.
         */
        public void ValidarPosicao(Posicao posicao)
        {
            if (!VerificarPosicao(posicao))
                throw new TabuleiroException("Posição inválida!");
        }
    }
}
