namespace tabuleiro
{
    class Peca(Tabuleiro tabuleiro, Cor cor)
    {
        public Posicao? Posicao { get; set; } = null;
        public Cor Cor { get; protected set; } = cor;
        public int QteMovimentos { get; protected set; } = 0;
        public Tabuleiro Tabuleiro { get; protected set; } = tabuleiro;
    }
}
