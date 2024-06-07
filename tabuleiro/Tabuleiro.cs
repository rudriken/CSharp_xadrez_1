namespace tabuleiro
{
    class Tabuleiro(int linhas, int colunas)
    {
        public int Linhas { get; set; } = linhas;
        public int Colunas { get; set; } = colunas;
        private readonly Peca[,] Pecas = new Peca[linhas, colunas];

        public Peca RetornarUmaPeca(int linha, int coluna) { 
            return Pecas[linha, coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao) {
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }
    }
}
