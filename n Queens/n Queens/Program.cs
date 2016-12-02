using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace n_Queens {
    class Program {
        static void Main(string[] args) {
            int a;
            Console.Write("Grid size: ");
            //a = Convert.ToInt16(Console.ReadLine());
            DateTime start = new DateTime();
            for (int i = 4; i < 6; i++) {
                ThreadPool.QueueUserWorkItem(
new WaitCallback(delegate (object state) { nQueensSetup((int)state); }), i);
            }

            Console.ReadLine();
        }
        public static void nQueensSetup(int n) {
            nQueens(0, new int[n, n], n);

        }
        public static bool checkSpace(int x, int y, int n, int[,] grid) {
            for (int i = 0; i < n; i++) {
                //Check Horizontal and Vertical
                if (grid[x, i] == 1) {
                    return false;
                }
                if (grid[i, y] == 1) {
                    return false;
                }
                //Angles are a little more complicated
                if (x + i < n && y + i < n && grid[x + i, y + i] == 1) {
                    return false;
                }
                if (x - i >= 0 && y - i >= 0 && grid[x - i, y - i] == 1) {
                    return false;
                }
                if (x + i < n && y - i >= 0 && grid[x + i, y - i] == 1) {
                    return false;
                }
                if (x - i >= 0 && y + i < n && grid[x - i, y + i] == 1) {
                    return false;
                }
            }
            return true;
        }
        public static void print(int[,] grid, int n) {
            for (int x = 0; x < n; x++) {
                for (int y = 0; y < n; y++) {
                    //Console.Write(grid[x, y] + " ");
                }
                //Console.Write("\n");
            }
            //Console.Write("\n");
        }
        public static void clearColumn(int x, int[,] lGrid, int n) {
            for (int y = 0; y < n; y++) {
                lGrid[x, y] = 0;
            }
        }
        public static void nQueens(int x, int[,] grid, int n) {
            if (x == n) {
                print(grid, n);
            } else {
                for (int i = 0; i < n; i++) {
                    if (checkSpace(x, i, n, grid)) {
                        grid[x, i] = 1;
                        nQueens(x + 1, grid, n);
                        //Clear the current working line (Basically the whole backtracking part of backtracking)
                        clearColumn(x, grid, n);
                    }
                }
            }
        }
    }
}