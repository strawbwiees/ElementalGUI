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
            button1.BackColor = Color.Red;
            button1.FlatStyle = FlatStyle.Popup;
            button1.ForeColor = SystemColors.Window;
            button1.Location = new Point(102, 322);
            button1.Name = "button1";
            button1.Size = new Size(117, 31);
            button1.TabIndex = 0;
            button1.Text = "CANCEL";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Marykate", 16.1999989F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(144, 29);
            label1.Name = "label1";
            label1.Size = new Size(196, 23);
            label1.TabIndex = 1;
            label1.Text = "CONFIRM SELECTION";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Marykate", 16.1999989F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(125, 122);
            label2.Name = "label2";
            label2.Size = new Size(98, 23);
            label2.TabIndex = 2;
            label2.Text = "PLAYER 1:";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Marykate", 16.1999989F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(125, 180);
            label3.Name = "label3";
            label3.Size = new Size(99, 23);
            label3.TabIndex = 3;
            label3.Text = "PLAYER 2:";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Marykate", 16.1999989F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(255, 122);
            label4.Name = "label4";
            label4.Size = new Size(113, 23);
            label4.TabIndex = 4;
            label4.Text = "CHARACTER";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Marykate", 16.1999989F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(255, 180);
            label5.Name = "label5";
            label5.Size = new Size(113, 23);
            label5.TabIndex = 5;
            label5.Text = "CHARACTER";
            label5.Click += label5_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.FlatStyle = FlatStyle.Popup;
            button2.ForeColor = Color.Black;
            button2.Location = new Point(276, 322);
            button2.Name = "button2";
            button2.Size = new Size(117, 31);
            button2.TabIndex = 6;
            button2.Text = "CONFIRM";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Marykate", 16.1999989F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(185, 262);
            label6.Name = "label6";
            label6.Size = new Size(147, 23);
            label6.TabIndex = 7;
            label6.Text = "Are you ready?";
            label6.Click += label6_Click;
            // 
            // ConfirmSelection
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(503, 406);
            Controls.Add(label6);
            Controls.Add(button2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Font = new Font("Marykate", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Margin = new Padding(4, 3, 4, 3);
            Name = "ConfirmSelection";
            Text = "ConfirmSelection";
            Load += ConfirmSelection_Load;
            ResumeLayout(false);
            PerformLayout();
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