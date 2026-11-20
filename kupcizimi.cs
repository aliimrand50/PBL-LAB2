


using System;

namespace Cubes
{
    class Program
    {
        static void Main(string[] args)
        {

            int nx = 5, ny = 5, nz = 5;  // küpün x, y, z boyutu (z = derinlik)

       // tek bir küpün şablonu (# = küp numarası)
            string[] template = {
                "  +-----+",
                " /     /|",
                "+-----+ |",
                "| # # | +",
                "| # # |/",
                "+-----+"
            };

            int H = (ny - 1) * 3 + (nz - 1) * 2 + 6; // y'de her adım 3 satır, z'de 2 satır, tek küp 6 satır (5x5x5 için 26)
            int W = (nx - 1) * 6 + (nz - 1) * 2 + 9; // x'te her adım 6 sütun, z'de 2 sütun, tek küp 9 sütun (5x5x5 için 41)
            char[,] ch = new char[H, W];
            for (int i = 0; i < H; i++)
                for (int j = 0; j < W; j++)
                    ch[i, j] = ' ';


            for (int z = nz - 1; z >= 0; z--) // arkadan öne (büyük z arkada)
                for (int y = 0; y < ny; y++)// alttan üste
                    for (int x = 0; x < nx; x++) // soldan sağa
                    {
                        int c = x * 6 + z * 2; // x'te her adım 6 sütun, z'de her adım 2 sütun sağa.
                        int r = (ny - 1 - y) * 3 + (nz - 1 - z) * 2; // ekranda satırlar aşağı doğru arttığı için y ters çevrilir. Yukarı çıkmak satır numarasını azaltır.


                        for (int i = 0; i < 6; i++) // şablonun 6 satırını tek tek tampona kopyala
                        {
                            bool started = false; // baştaki boşluklar arkadaki küpün çizgisini silmesin diye atlanır
                            for (int k = 0; k < template[i].Length; k++)
                            {
                                char t = template[i][k];
                                if (t != ' ') started = true;
                                if (!started) continue; //Satırın başındaki boşluk karakterlerindeysem, bunları tampona yazma, sonrakine geç.(yarıda kesip bir sonraki karaktere geçer)
                                if (t == '#') t = '1'; // # yerine küpün numarası
                                ch[r + i, c + k] = t; // karakteri tamponda küpün yerine yaz
                            }
                        }
                    }
            // 6x6x6 gibi büyük küplerde (31 satır) pencere yetmez, o zaman bunları aç(burasını şimdilik es geçicez ama ileride lazım olabilir)
            //Console.SetBufferSize(120, 50);
            //Console.SetWindowSize(120, 40);


            Console.CursorVisible = false;
            Console.Clear();
            for (int i = 0; i < H; i++)
                for (int j = 0; j < W; j++)
                {
                    Console.SetCursorPosition(2 + j, 2 + i); // imleci bu karakterin yerine götür (2: kenardan boşluk)
                    Console.Write(ch[i, j]); // ekrana yazdırıyoruz
                }

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Cikmak icin bir tusa bas...");
            Console.ReadKey(true);
        }
    }
}