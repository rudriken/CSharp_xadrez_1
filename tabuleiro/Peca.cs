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
         * Verifica se há, pelo menos, um movimento possível para uma peça qualquer.
         * Se houver, retornará 'true', se não, retornará 'false'.
         */
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();

            for (int i = 0; i < Tabuleiro.Linhas; i++)
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                    if (matriz[i, j])
                        return true;

            return false;
        }

        /*
         * Verifica se é possível mover uma peça qualquer na posição de destino.
         */
        public bool PodeMoverPara(Posicao destino)
        {
            return MovimentosPossiveis()[destino.Linha, destino.Coluna];
        }

        /*
         * Retorna uma matriz mostrando todos as posições possíveis de uma peça qualquer 
         * no tabuleiro.
         */
        public abstract bool[,] MovimentosPossiveis();
    }
}
