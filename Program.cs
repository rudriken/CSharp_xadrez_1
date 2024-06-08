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

            try
            {
                partida = new();

                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tabuleiro);

                    Console.WriteLine();

                    Console.Write("Origem: ");
                    origem = Tela.LerPosicaoXadrez().ToPosicao();
                    Console.Write("Destino: ");
                    destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ExecutarMovimento(origem, destino);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}