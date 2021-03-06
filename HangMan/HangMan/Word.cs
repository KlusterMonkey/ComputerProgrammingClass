﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    class Word
    {
        static List<string> items;
        static string lines;
        static string word;
        public Word()
        {
            lines = File.ReadAllText(@"Assets\dictionary.dict");
            items = new List<string>();
            items.Clear();
            int i = 0;
            while (true)
            {
                try
                {
                    items.Add(lines.Split(',')[i]);
                    i++;
                }
                catch (Exception e)
                {
                    break;
                }
            }
        }
        public string PickNew()
        {
            StreamWriter output = new StreamWriter(@"Assets\plays.txt", true);
            DateTime time = DateTime.Now;
            if (time.Hour <= 12)
            {
                output.WriteLine(time.Hour + ":" + time.Minute + ":" + time.Second);
            }
            else if (time.Hour > 12)
            {
                output.WriteLine((time.Hour - 12) + ":" + time.Minute + ":" + time.Second + " PM");
            }
            output.Close();
            Random r = new Random();
            word = items[r.Next(0, items.Count - 1)];
            return word;
        }
        public string getWord()
        {
            return word;
        }
        public char[] getCharArray()
        {
            return word.ToCharArray();
        }
        public int length()
        {
            return word.Length;
        }
    }
}
