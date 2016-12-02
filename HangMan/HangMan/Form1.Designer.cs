namespace HangMan
{
    partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DrawWindow = new System.Windows.Forms.PictureBox();
            this.GameTick = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DrawWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawWindow
            // 
            this.DrawWindow.Location = new System.Drawing.Point(0, 0);
            this.DrawWindow.Margin = new System.Windows.Forms.Padding(0);
            this.DrawWindow.Name = "DrawWindow";
            this.DrawWindow.Size = new System.Drawing.Size(1280, 720);
            this.DrawWindow.TabIndex = 0;
            this.DrawWindow.TabStop = false;
            // 
            // GameTick
            // 
            this.GameTick.Enabled = true;
            this.GameTick.Interval = 1;
            this.GameTick.Tick += new System.EventHandler(this.GameTick_Tick);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1281, 722);
            this.Controls.Add(this.DrawWindow);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1299, 769);
            this.MinimumSize = new System.Drawing.Size(1299, 769);
            this.Name = "Window";
            this.Text = "Hangman";
            this.Load += new System.EventHandler(this.start);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DrawWindow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawWindow;
        private System.Windows.Forms.Timer GameTick;
    }
}

