﻿using tabuleiro;

namespace xadrez
{
    class Program
    {
        static void Main()
        {
            Tabuleiro tabuleiro;

            tabuleiro = new(8, 8);

            Tela.ImprimirTabuleiro(tabuleiro);

            Console.WriteLine();
        }
    }
}