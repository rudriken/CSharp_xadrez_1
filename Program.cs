using tabuleiro;
using xadrez_jogo;

namespace xadrez
{
    class Program
    {
        static void Main()
        {
            PartidaDeXadrez partida;
            Posicao origem, destino;
            bool[,] posicoesPossiveis;

            partida = new();

            while (!partida.Terminada)
            {
                try
                {
                    Console.Clear();
                    Tela.ImprimirPartida(partida);

                    Console.WriteLine();

                    Console.Write("Origem: ");
                    origem = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ValidarPosicaoDeOrigem(origem);

                    posicoesPossiveis =
                        partida.Tabuleiro.RetornarUmaPeca(origem)?.MovimentosPossiveis() ??
                        new bool[partida.Tabuleiro.Linhas, partida.Tabuleiro.Colunas];

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                    Console.Write("Destino: ");
                    destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ValidarPosicaoDeDestino(origem, destino);

                    partida.RealizarJogada(origem, destino);

                    Console.Clear();
                    Tela.ImprimirPartida(partida);
                }
                catch (TabuleiroException erro)
                {
                    Console.WriteLine(erro.Message);
                    Console.ReadLine();
                }

            }
        }
    }
}