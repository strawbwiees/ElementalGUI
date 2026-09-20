namespace ElementalGUI
{
    partial class ChooseCharacters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseCharacters));
            Fire = new PictureBox();
            Water = new PictureBox();
            EArth = new PictureBox();
            Wind = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            back = new Button();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)Fire).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Water).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EArth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Wind).BeginInit();
            SuspendLayout();
            // 
            // Fire
            // 
            Fire.BackColor = Color.Transparent;
            Fire.Cursor = Cursors.Hand;
            Fire.Image = (Image)resources.GetObject("Fire.Image");
            Fire.Location = new Point(93, 213);
            Fire.Name = "Fire";
            Fire.Size = new Size(225, 272);
            Fire.SizeMode = PictureBoxSizeMode.CenterImage;
            Fire.TabIndex = 0;
            Fire.TabStop = false;
            Fire.Click += pictureBox1_Click;
            // 
            // Water
            // 
            Water.BackColor = Color.Transparent;
            Water.Cursor = Cursors.Hand;
            Water.Image = (Image)resources.GetObject("Water.Image");
            Water.Location = new Point(354, 213);
            Water.Name = "Water";
            Water.Size = new Size(225, 272);
            Water.SizeMode = PictureBoxSizeMode.CenterImage;
            Water.TabIndex = 1;
            Water.TabStop = false;
            // 
            // EArth
            // 
            EArth.BackColor = Color.Transparent;
            EArth.Cursor = Cursors.Hand;
            EArth.Image = (Image)resources.GetObject("EArth.Image");
            EArth.Location = new Point(616, 213);
            EArth.Name = "EArth";
            EArth.Size = new Size(225, 272);
            EArth.SizeMode = PictureBoxSizeMode.CenterImage;
            EArth.TabIndex = 2;
            EArth.TabStop = false;
            EArth.Click += pictureBox3_Click;
            // 
            // Wind
            // 
            Wind.BackColor = Color.Transparent;
            Wind.Cursor = Cursors.Hand;
            Wind.Image = (Image)resources.GetObject("Wind.Image");
            Wind.Location = new Point(884, 213);
            Wind.Name = "Wind";
            Wind.Size = new Size(200, 272);
            Wind.SizeMode = PictureBoxSizeMode.CenterImage;
            Wind.TabIndex = 3;
            Wind.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(369, 55);
            label1.Name = "label1";
            label1.Size = new Size(413, 32);
            label1.TabIndex = 4;
            label1.Text = "CHOOSE YOUR CHARACTER";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(505, 114);
            label2.Name = "label2";
            label2.Size = new Size(151, 32);
            label2.TabIndex = 5;
            label2.Text = "PLAYER 1";
            label2.Click += label2_Click;
            // 
            // back
            // 
            back.BackColor = SystemColors.ActiveCaption;
            back.FlatStyle = FlatStyle.Popup;
            back.Font = new Font("Arial Rounded MT Bold", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            back.Location = new Point(45, 37);
            back.Name = "back";
            back.Size = new Size(92, 56);
            back.TabIndex = 6;
            back.Text = "BACK";
            back.UseVisualStyleBackColor = false;
            back.Click += back_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(146, 499);
            label3.Name = "label3";
            label3.Size = new Size(107, 32);
            label3.TabIndex = 7;
            label3.Text = "Lumen";
            // 
            // ChooseCharacters
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1182, 753);
            Controls.Add(label3);
            Controls.Add(back);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Wind);
            Controls.Add(EArth);
            Controls.Add(Water);
            Controls.Add(Fire);
            Name = "ChooseCharacters";
            Text = "ChooseCharacters";
            ((System.ComponentModel.ISupportInitialize)Fire).EndInit();
            ((System.ComponentModel.ISupportInitialize)Water).EndInit();
            ((System.ComponentModel.ISupportInitialize)EArth).EndInit();
            ((System.ComponentModel.ISupportInitialize)Wind).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Label label1;
        private Label label2;
        private Button back;
        private PictureBox Fire;
        private PictureBox Water;
        private PictureBox EArth;
        private PictureBox Wind;
        private Label label3;
    }
}