namespace tabuleiro
{
    abstract class Peca(Tabuleiro tabuleiro, Cor cor)
    {
        public Posicao? Posicao { get; set; } = null;
        public Cor Cor { get; protected set; } = cor;
        public int QteMovimentos { get; protected set; } = 0;
        public Tabuleiro Tabuleiro { get; protected set; } = tabuleiro;

        /*
         * Incrementa a quantidade de movimentos de uma peça qualquer numa partida.
         */
        public void IncrementarQteMovimentos()
        {
            QteMovimentos++;
        }

        /*
         * Retorna uma matriz mostrando todos as posições possíveis de uma peça qualquer 
         * no tabuleiro.
         */
        public abstract bool[,] MovimentosPossiveis();
    }
}
