namespace SMI1002_TP
{
    partial class FormConnect
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
            button2 = new Button();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(302, 329);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 3;
            button2.Text = "Retourner";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(420, 329);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 2;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 31);
            label1.Name = "label1";
            label1.Size = new Size(50, 24);
            label1.TabIndex = 4;
            label1.Text = "Host";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 86);
            label2.Name = "label2";
            label2.Size = new Size(46, 24);
            label2.TabIndex = 5;
            label2.Text = "Port";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 145);
            label3.Name = "label3";
            label3.Size = new Size(160, 24);
            label3.TabIndex = 6;
            label3.Text = "SID/Service name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(43, 203);
            label4.Name = "label4";
            label4.Size = new Size(72, 24);
            label4.TabIndex = 7;
            label4.Text = "User ID";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(43, 262);
            label5.Name = "label5";
            label5.Size = new Size(91, 24);
            label5.TabIndex = 8;
            label5.Text = "Password";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(218, 28);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(314, 30);
            textBox1.TabIndex = 9;
            textBox1.Text = "172.16.25.43";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(218, 83);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(314, 30);
            textBox2.TabIndex = 10;
            textBox2.Text = "1521";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(218, 142);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(314, 30);
            textBox3.TabIndex = 11;
            textBox3.Text = "coursbd.uqtr.ca";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(218, 200);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(314, 30);
            textBox4.TabIndex = 12;
            textBox4.Text = "SMI1002_070";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(218, 259);
            textBox5.Name = "textBox5";
            textBox5.PasswordChar = '*';
            textBox5.Size = new Size(314, 30);
            textBox5.TabIndex = 13;
            textBox5.Text = "93cpdf92";
            // 
            // FormConnect
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(591, 399);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "FormConnect";
            Text = "Se connecter";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
    }
}