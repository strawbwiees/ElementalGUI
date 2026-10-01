namespace ElementalGUI
{
    partial class ConfirmSelection
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
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button2 = new Button();
            label6 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 99, 92);
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Marykate", 16.1999989F);
            button1.ForeColor = Color.White;
            button1.Location = new Point(77, 320);
            button1.Name = "button1";
            button1.Size = new Size(140, 45);
            button1.TabIndex = 0;
            button1.Text = "CANCEL";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Marykate", 23.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(255, 244, 100);
            label1.Location = new Point(51, 29);
            label1.Name = "label1";
            label1.Size = new Size(400, 45);
            label1.TabIndex = 1;
            label1.Text = "CONFIRM SELECTION";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Font = new Font("Marykate", 19.7999973F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(126, 187, 255);
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(92, 110);
            label2.Name = "label2";
            label2.Size = new Size(101, 35);
            label2.TabIndex = 2;
            label2.Text = "PLAYER 1:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.Font = new Font("Marykate", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(255, 99, 92);
            label3.Location = new Point(92, 170);
            label3.Name = "label3";
            label3.Size = new Size(101, 35);
            label3.TabIndex = 3;
            label3.Text = "PLAYER 2:";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.Font = new Font("Marykate", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(254, 110);
            label4.Name = "label4";
            label4.Size = new Size(171, 35);
            label4.TabIndex = 4;
            label4.Text = "CHARACTER";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.Font = new Font("Marykate", 19.7999973F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(254, 170);
            label5.Name = "label5";
            label5.Size = new Size(171, 35);
            label5.TabIndex = 5;
            label5.Text = "CHARACTER";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            label5.Click += label5_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(126, 217, 87);
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Marykate", 16.1999989F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(254, 320);
            button2.Name = "button2";
            button2.Size = new Size(140, 45);
            button2.TabIndex = 6;
            button2.Text = "CONFIRM";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label6
            // 
            label6.Font = new Font("Marykate", 22.1999989F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(102, 250);
            label6.Name = "label6";
            label6.Size = new Size(275, 35);
            label6.TabIndex = 7;
            label6.Text = "Are you ready?";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.Click += label6_Click;
            // 
            // ConfirmSelection
            // 
            BackColor = Color.FromArgb(44, 32, 66);
            ClientSize = new Size(503, 406);
            Controls.Add(label6);
            Controls.Add(button2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "ConfirmSelection";
            Text = "Confirm Selection";
            Load += ConfirmSelection_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button button2;
        private Label label6;
    }
}
