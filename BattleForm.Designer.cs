namespace ElementalGUI
{
    partial class BattleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleForm));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            hpBar1 = new ElementalGUI.CartoonUI.HpBar();
            hpBar2 = new ElementalGUI.CartoonUI.HpBar();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            pictureBox1 = new BufferedPictureBox();
            pictureBox2 = new BufferedPictureBox();
            pictureBox3 = new BufferedPictureBox();
            pictureBox4 = new BufferedPictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Marykate", 25.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(87, 105);
            label1.Name = "label1";
            label1.Size = new Size(196, 45);
            label1.TabIndex = 0;
            label1.Text = "CHARACTER";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Marykate", 25.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(863, 105);
            label2.Name = "label2";
            label2.Size = new Size(243, 45);
            label2.TabIndex = 1;
            label2.Text = "CHARACTER";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Marykate", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(255, 244, 100);
            label3.Location = new Point(291, 30);
            label3.Name = "label3";
            label3.Size = new Size(600, 50);
            label3.TabIndex = 2;
            label3.Text = "CHARACTER'S TURN";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += label3_Click;
            // 
            // hpBar1
            // 
            hpBar1.Location = new Point(87, 165);
            hpBar1.Name = "hpBar1";
            hpBar1.Size = new Size(254, 30);
            hpBar1.TabIndex = 3;
            // 
            // hpBar2
            // 
            hpBar2.Location = new Point(852, 165);
            hpBar2.Name = "hpBar2";
            hpBar2.Size = new Size(254, 30);
            hpBar2.TabIndex = 4;
            // 
            // label4
            // 
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Comic Sans MS", 13F, FontStyle.Bold);
            label4.ForeColor = Color.White;
            label4.Location = new Point(347, 163);
            label4.Name = "label4";
            label4.Size = new Size(130, 33);
            label4.TabIndex = 5;
            label4.Text = "100/100";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Comic Sans MS", 13F, FontStyle.Bold);
            label5.ForeColor = Color.White;
            label5.Location = new Point(716, 163);
            label5.Name = "label5";
            label5.Size = new Size(130, 33);
            label5.TabIndex = 6;
            label5.Text = "100/100";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Marykate", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(255, 244, 100);
            label6.Location = new Point(191, 606);
            label6.Name = "label6";
            label6.Size = new Size(800, 36);
            label6.TabIndex = 7;
            label6.Text = "Choose your action!";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom;
            button1.BackColor = Color.FromArgb(255, 209, 61);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Comic Sans MS", 13.8F, FontStyle.Bold);
            button1.ForeColor = Color.FromArgb(28, 20, 44);
            button1.Location = new Point(158, 673);
            button1.Name = "button1";
            button1.Size = new Size(184, 45);
            button1.TabIndex = 8;
            button1.Text = "BASIC ATTACK";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom;
            button2.BackColor = Color.FromArgb(74, 144, 217);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Comic Sans MS", 13.8F, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Location = new Point(499, 673);
            button2.Name = "button2";
            button2.Size = new Size(184, 45);
            button2.TabIndex = 9;
            button2.Text = "DEFEND";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom;
            button3.BackColor = Color.FromArgb(255, 99, 92);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Comic Sans MS", 13.8F, FontStyle.Bold);
            button3.ForeColor = Color.White;
            button3.Location = new Point(840, 673);
            button3.Name = "button3";
            button3.Size = new Size(184, 45);
            button3.TabIndex = 10;
            button3.Text = "SPECIAL (3)";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Location = new Point(94, 282);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(293, 301);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Location = new Point(813, 282);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(293, 301);
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Location = new Point(312, 382);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(556, 172);
            pictureBox3.TabIndex = 13;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Location = new Point(274, 346);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(150, 150);
            pictureBox4.TabIndex = 14;
            pictureBox4.TabStop = false;
            // 
            // BattleForm
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1182, 753);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(hpBar2);
            Controls.Add(hpBar1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            DoubleBuffered = true;
            Name = "BattleForm";
            Text = "Elemental Battle";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private CartoonUI.HpBar hpBar1;
        private CartoonUI.HpBar hpBar2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
        private Button button2;
        private Button button3;
        private BufferedPictureBox pictureBox1;
        private BufferedPictureBox pictureBox2;
        private BufferedPictureBox pictureBox3;
        private BufferedPictureBox pictureBox4;
    }
}
