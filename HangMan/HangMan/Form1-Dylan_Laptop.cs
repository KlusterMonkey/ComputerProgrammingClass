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
        Brush tBrush = new SolidBrush(Color.FromArgb(158, 168, 100));
        Font tFont = new Font("Segoe UI Light", 40);
        Font smallTFont = new Font("Segoe UI Light", 10);
        StringFormat sf = new StringFormat();
        public Window()
        {
            InitializeComponent();
        }
        private void quit()
        {
            DialogResult quit = MessageBox.Show("Do you really want to quit?", "Quit?", MessageBoxButtons.YesNo, MessageBoxIcon.None);
        }
        private void quit(bool win)
        {
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
                if (!attemptedChars.Contains(Convert.ToChar(k)))
                {
                    attemptedChars.Add(Convert.ToChar(k));
                }
                attemptsRemaining--;
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
            if (e.KeyCode == Keys.Escape)
            {
                quit();
            }
            else
            {
                checkLetters(e.KeyCode);
            }

        }
        private void Update_Tick(object sender, EventArgs e)
        { updateScreen(); }
        private void updateScreen()
        {
            //Setting up the canvas every time to allow for dynamic screen width
            DrawWindow.Width = this.Width - 15; DrawWindow.Height = this.Height - 15;
            screenWidth = DrawWindow.Width; screenHeight = DrawWindow.Height;
            sPicture = new Bitmap(screenWidth, screenHeight);
            sCanvas = Graphics.FromImage(sPicture);
            canvas = DrawWindow.CreateGraphics();
            //Reset the canvas (mainly in case of moving images so its not super important here (I don't like the idea of having thousands of the same square drawn in the same space even though it makes no difference to the code or performance whatsoever)
            sCanvas.Clear(Color.FromArgb(29, 17, 66));
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            sCanvas.DrawString(attemptsRemaining.ToString(), tFont, tBrush, screenWidth - 60, 60, sf);
            //Cheaty Cheaty
            sCanvas.DrawString("Totally not " + word.getWord(), smallTFont, tBrush, 10, 10);
            //Draw all the stuff
            int charX = 40;
            int charY = 80;
            foreach (char c in attemptedChars)
            {
                sCanvas.DrawString(c.ToString(), tFont, tBrush, charX, charY, sf);
                if (charX >= screenWidth - 180)
                { charX = 40; charY += 60; }
                else
                { charX += 60; }

            }
            for (int i = 1; i < word.length() + 1; i++)
            {
                letterX[i - 1] = (screenWidth / 2) - ((textWidth + textPadding) * i) + (((textWidth + textPadding) * word.length() / 2) + textPadding / 2);
                letterY[i - 1] = screenHeight - 50;
                sCanvas.FillRectangle(tBrush, letterX[i - 1], screenHeight - 50, textWidth, 4);
                //Draw attempts remaining and the succeeded letters
                sCanvas.DrawString(buildingChars[word.length() - i].ToString(), tFont, tBrush, letterX[i - 1] + 30, letterY[i - 1] - 30, sf);
            }
            if (attemptsRemaining < 10)
            {
                try
                { State = Image.FromFile(@"Assets\Step" + (10 - attemptsRemaining) + ".png"); }
                catch { }
                sCanvas.DrawImage(State, screenWidth / 2 - 250, screenHeight / 2 - 350, 500, 500);
            }
            canvas.DrawImage(sPicture, 0, 0, screenWidth, screenHeight);
        }
        private void reset()
        {
            //Word and text recording setup
            word.PickNew();
            textWidth = 60; textPadding = 10;
            buildingChars = new char[word.length()];
            attemptedChars = new List<char>();
            attemptsRemaining = 10;
            //Enable the timer
            Update.Enabled = true;
            //Letter underline X and Y
            letterX = new int[word.length()];
            letterY = new int[word.length()];
            updateScreen();
        }
        private void start(object sender, EventArgs e)
        { reset(); }
    }
}