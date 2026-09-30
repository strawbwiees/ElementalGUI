namespace ElementalGUI
{
    partial class ChooseCharacter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseCharacter));
            label1 = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            //
            // label1
            //
            label1.AutoSize = false;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Comic Sans MS", 40F, FontStyle.Bold);
            label1.ForeColor = CartoonUI.HighlightColor;
            label1.Location = new Point(141, 45);
            label1.Name = "label1";
            label1.Size = new Size(900, 90);
            label1.TabIndex = 0;
            label1.Text = "CHOOSE YOUR CHARACTER";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            //
            // label2
            //
            label2.AutoSize = false;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Comic Sans MS", 26F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(126, 187, 255);
            label2.Location = new Point(441, 150);
            label2.Name = "label2";
            label2.Size = new Size(300, 55);
            label2.TabIndex = 1;
            label2.Text = "PLAYER 1";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Click += label2_Click;
            //
            // pictureBox1
            //
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(26, 238);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(250, 330);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            //
            // pictureBox2
            //
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(321, 238);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(250, 330);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            //
            // pictureBox3
            //
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(613, 238);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(250, 330);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            //
            // pictureBox4
            //
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(901, 238);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(250, 330);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 5;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            //
            // label3
            //
            label3.AutoSize = false;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(26, 571);
            label3.Name = "label3";
            label3.Size = new Size(250, 45);
            label3.TabIndex = 6;
            label3.Text = "Lumen";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            //
            // label4
            //
            label4.AutoSize = false;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(321, 571);
            label4.Name = "label4";
            label4.Size = new Size(250, 45);
            label4.TabIndex = 7;
            label4.Text = "Ripple";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            //
            // label5
            //
            label5.AutoSize = false;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            label5.ForeColor = Color.White;
            label5.Location = new Point(613, 571);
            label5.Name = "label5";
            label5.Size = new Size(250, 45);
            label5.TabIndex = 8;
            label5.Text = "Grunch";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            //
            // label6
            //
            label6.AutoSize = false;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Comic Sans MS", 20F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(901, 571);
            label6.Name = "label6";
            label6.Size = new Size(250, 45);
            label6.TabIndex = 9;
            label6.Text = "Gale";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            //
            // button1
            //
            button1.BackColor = CartoonUI.GoodColor;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Comic Sans MS", 16.2F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(471, 648);
            button1.Name = "button1";
            button1.Size = new Size(240, 51);
            button1.TabIndex = 10;
            button1.Text = "SELECT";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            //
            // ChooseCharacter
            //
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1182, 753);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ChooseCharacter";
            Text = "Choose Your Character";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
    }
}
