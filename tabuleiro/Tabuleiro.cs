namespace tabuleiro
{
    class Tabuleiro(int linhas, int colunas)
    {
        public int Linhas { get; set; } = linhas;
        public int Colunas { get; set; } = colunas;
        private readonly Peca?[,] Pecas = new Peca[linhas, colunas];

        public Peca? RetornarUmaPeca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca? RetornarUmaPeca(Posicao posicao)
        {
            return Pecas[posicao.Linha, posicao.Coluna];
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return RetornarUmaPeca(posicao) != null;
        }

        public void ColocarPeca(Peca? peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
                throw new TabuleiroException("Jé existe uma peça nessa posição!");

            Pecas[posicao.Linha, posicao.Coluna] = peca;

            if (peca != null)
                peca.Posicao = posicao;
        }

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

        public bool VerificarPosicao(Posicao posicao)
        {
            if (
                posicao.Linha < 0 || posicao.Linha >= Linhas ||
                posicao.Coluna < 0 || posicao.Coluna >= Colunas
            )
                return false;

            return true;
        }

        public void ValidarPosicao(Posicao posicao)
        {
            if (!VerificarPosicao(posicao))
                throw new TabuleiroException("Posição inválida!");
        }
    }
}
