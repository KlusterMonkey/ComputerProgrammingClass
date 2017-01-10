using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Number_Four {
    class SpellNumber {
        static Dictionary<int, string> numberToName = new Dictionary<int, string>() {
            { 0, "zero" },

        };

        static string[] ones = new string[10] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        static string[] tens = new string[10] { "", "", "twenty-", "thirty-", "forty-", "fiftey-", "sixtey-", "seventy-", "eighty-", "ninety-" };
        static string[] teens = new string[10] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        static string[] places = new string[] { "hundred", "thousand", "million", "billion"};
        public static string spell(int num) {
            int len = num.ToString().Length;
            string word = null;

            for (int i = 0; i < len; i++) {
                int current = ((int)(num / Math.Pow(10, i)) % 10);
                int previous = ((int)(num / Math.Pow(10, i - 1)) % 10);
                if (i == 0) {
                    word = ones[current];
                } else if (i == 1) {
                    if (current == 1) {
                        word = teens[previous];
                    } else {
                        word = tens[current] + word;
                    }
                } else if (i == 2) {
                    word = ones[current] + " " + places[i - 2] + " " + word;
                } else if (i == 3) {
                    word = ones[current] + " " + places[i - 2] + " " + word;
                } else if (i == 4) {
                    word = ones[current] + " " + places[i - 2] + " " + ones[current] + " " + places[i - 2] + " " + word;
                }
            }
            if (word.EndsWith("zero") && !word.StartsWith("zero")) {
                return word.Replace("zero", "");
            }
            return word;
        }
        public static string spellRecursive(int num, int place) {
            string word = "";
            int len = num.ToString().Length;
            int x = 3;
            if (len - (place * 3) <= 2) {
                x = len - (place * 3);
            }
            for (int i = 0; i < x; i++) {
                int current = ((int)(num / Math.Pow(10, (place * 3) + i)) % 10);
                int previous = ((int)(num / Math.Pow(10, (place * 3) + i - 1)) % 10);
                if (i == 0) {
                    word = ones[current];
                } else if (i == 1) {
                    if (current == 1) {
                        word = teens[previous];
                    } else {
                        word = tens[current] + word;
                    }
                } else if (i == 2) {
                    word = ones[current] + " " + places[i - 2] + " " + word;
                }
            }
            if (place > 0) {
                word += " " + places[place];
            }
            if (len > place * 3) {
                word = spellRecursive(num, place + 1) + " " + word;
            }
            foreach (string s in places) {
                if (word.StartsWith(" " + s)) {
                    word = word.Substring(s.Length + 1);
                }
            }
            return word;
        }
    }
}
