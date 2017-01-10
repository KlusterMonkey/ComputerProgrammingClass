using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Number_Four {
    class Program {
        static void Main(string[] args) {
            int x = 113456789;
            while (x != 4) {
                string spelled = SpellNumber.spellRecursive(x, 0);
                Console.WriteLine(spelled);
                x = spelled.Replace(" ", "").Length;
                Console.WriteLine(x);
                for (int i = 0; i < Console.WindowWidth; i++) {
                    Console.Write("-");
                }
            }
            Console.ReadLine();

        }
    }
}
