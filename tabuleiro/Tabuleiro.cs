namespace tabuleiro
{
    class Tabuleiro(int linhas, int colunas)
    {
        public int Linhas { get; set; } = linhas;
        public int Colunas { get; set; } = colunas;
        private Peca[,] Pecas = new Peca[linhas, colunas];
    }
}
