using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;

namespace HangMan
{
    public partial class Window : Form
    {
        int attemptsRemaining, textHeight, textWidth, textPadding, screenWidth, screenHeight;
        bool win = false;
        Word word = new Word();
        Image State;
        List<char> attemptedChars = new List<char>();
        int[] letterX, letterY;
        char[] buildingChars;
        Graphics canvas, sCanvas;
        Bitmap sPicture;
        public Window()
        {
            InitializeComponent();
        }

        private void GameTick_Tick(object sender, EventArgs e)
        {
            updateScreen();
        }

        private void quit()
        {
            updateScreen();
            DialogResult quit = MessageBox.Show("Do you really want to quit?", "Quit?", MessageBoxButtons.YesNo, MessageBoxIcon.None);
        }
        private void quit(bool win)
        {
            updateScreen();
            DialogResult quit;
            if (win)
            { quit = MessageBox.Show("You win, do you want to quit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None); }
            else
            { quit = MessageBox.Show("You lose, do you want to ragequit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None); }
            if (quit == DialogResult.Yes)
            { Application.Exit(); }
            else
            { reset(); }
        }
        private void checkLetters(Keys k)
        {
            //Checking against the word to see if you have a match or not
            int i = 0;
            bool matched = false;
            foreach (char c in word.getCharArray())
            {
                if (k.ToString().ToLower() == c.ToString())
                {
                    buildingChars[i] = c;
                    matched = true;
                }
                i++;
            }
            if (!matched)
            {
                if (!attemptedChars.Contains(Convert.ToChar(k)) && Char.IsLetter(Convert.ToChar(k)))
                { attemptedChars.Add(Convert.ToChar(k)); attemptsRemaining--; }
            }
            //Checking to see if you can haz win
            win = true;
            foreach (char c in buildingChars)
            {
                if (c == '\0')
                {
                    win = false;
                    break;
                }
            }
            if (win)
            { quit(win); }
            else if (!win && attemptsRemaining <= 0)
            { quit(win); }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            updateScreen();
            if (e.KeyCode == Keys.Escape)
            {
                quit();
            }
            else
            {
                checkLetters(e.KeyCode);
            }
        }
        private void updateScreen()
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            //Reset the canvas (mainly in case of moving images so its not super important here (I don't like the idea of having thousands of the same square drawn in the same space even though it makes no difference to the code or performance whatsoever)
            sCanvas.Clear(Color.DarkCyan);
            //Cheaty Cheaty
            sCanvas.DrawString("Totally not " + word.getWord(), new Font("Ariel", 10), Brushes.White, 10, 10);
            //Draw your failed attempts
            int charX = 40;
            int charY = 80;
            foreach (char c in attemptedChars)
            {
                sCanvas.DrawString(c.ToString(), new Font("Ariel", 40), Brushes.White, charX, charY, sf);
                charX = charX >= screenWidth - 180 ? 40 : charX + 60;
                charY = charX == 40 ? charY + 60 : charY;

            }
            sCanvas.DrawString(attemptsRemaining.ToString(), new Font("Ariel", 40), Brushes.White, screenWidth - 60, 60, sf);
            //Draw the partial word and underlines
            for (int i = 1; i < word.length() + 1; i++)
            {
                letterX[i - 1] = (screenWidth / 2) - ((textWidth + textPadding) * i) + (((textWidth + textPadding) * word.length() / 2) + textPadding / 2);
                letterY[i - 1] = screenHeight - 50;
                sCanvas.FillRectangle(Brushes.DarkRed, letterX[i - 1], screenHeight - 50, textWidth, 4);

                //Draw attempts remaining and the succeeded letters
                sCanvas.DrawString(buildingChars[word.length() - i].ToString(), new Font("Ariel", 40), Brushes.White, letterX[i - 1] + 30, letterY[i - 1] - 30, sf);
            }
            //Draw the noose/person
            if (attemptsRemaining < 10)
            {
                try
                { State = Image.FromFile(@"Assets\Step" + (10 - attemptsRemaining) + ".png"); }
                catch { }
                sCanvas.DrawImage(State, screenWidth / 2 - 450, screenHeight / 2, 250, 250);
            }
            canvas.DrawImage(sPicture, 0, 0, screenWidth, screenHeight);
        }
        private void reset()
        {
            //Graphics setup
            screenWidth = DrawWindow.Width; screenHeight = DrawWindow.Height;
            sPicture = new Bitmap(screenWidth, screenHeight); sCanvas = Graphics.FromImage(sPicture);
            canvas = DrawWindow.CreateGraphics();
            //Word and text recording setup
            word.PickNew();
            textWidth = 60; textPadding = 10;
            buildingChars = new char[word.length()];
            attemptedChars = new List<char>();
            attemptsRemaining = 10;
            //Letter underline X and Y
            letterX = new int[word.length()];
            letterY = new int[word.length()];
            updateScreen();
        }
        private void start(object sender, EventArgs e)
        { reset(); }
    }
}