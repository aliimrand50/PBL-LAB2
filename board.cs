using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CUBES1
{
    class Program
    {
        static void Main(string[] args)
        {
            int nx = 5, ny = 2, nz = 3;  // küpün x, y, z boyutu (z = derinlik)

            // g[x, y, z] = parça numarası, 0 = boş hücre
            int[,,] g = new int[nx, ny, nz];

            // z = 0 (en ön katman)
            g[0, 0, 0] = 5; g[1, 0, 0] = 5; g[0, 1, 0] = 5;
            g[4, 0, 0] = 6; g[4, 1, 0] = 6;

            // z = 1 (orta katman)
            g[3, 0, 1] = 6; g[4, 0, 1] = 6;
            g[4, 1, 1] = 7; g[0, 1, 1] = 1;

            // z = 2 (en arka katman)
            g[1, 0, 2] = 2; g[2, 0, 2] = 2; g[1, 1, 2] = 2;
            g[3, 1, 2] = 7; g[4, 1, 2] = 7; g[4, 0, 2] = 7;
            g[0, 1, 2] = 1;

            // tek bir küpün şablonu (# = küp numarası)
            string[] template = {
                "  +-----+",
                " /     /|",
                "+-----+ |",
                "| # # | +",
                "| # # |/",
                "+-----+"
            };

            int H = (ny - 1) * 3 + (nz - 1) * 2 + 6; // 2x3 için 13 satır
            int W = (nx - 1) * 6 + (nz - 1) * 2 + 9; // 5x3 için 37 sütun
            char[,] ch = new char[H, W];
            for (int i = 0; i < H; i++)
                for (int j = 0; j < W; j++)
                    ch[i, j] = ' ';

            for (int z = nz - 1; z >= 0; z--)    // arkadan öne (büyük z arkada)
                for (int y = 0; y < ny; y++)     // alttan üste
                    for (int x = 0; x < nx; x++) // soldan sağa
                    {
                        if (g[x, y, z] == 0) continue; // boş hücreyi hiç çizme

                        int c = x * 6 + z * 2;
                        int r = (ny - 1 - y) * 3 + (nz - 1 - z) * 2;

                        for (int i = 0; i < 6; i++)
                        {
                            bool started = false;
                            for (int k = 0; k < template[i].Length; k++)
                            {
                                char t = template[i][k];
                                if (t != ' ') started = true;
                                if (!started) continue;
                                if (t == '#') t = (char)('0' + g[x, y, z]); // # yerine parça numarası
                                ch[r + i, c + k] = t;
                            }
                        }
                    }

            Console.CursorVisible = false;
            Console.Clear();
            for (int i = 0; i < H; i++)
                for (int j = 0; j < W; j++)
                {
                    Console.SetCursorPosition(2 + j, 2 + i);
                    Console.Write(ch[i, j]);
                }

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Cikmak icin bir tusa bas...");
            Console.ReadKey(true);
        }
    }
}