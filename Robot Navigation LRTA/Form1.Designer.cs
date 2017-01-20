namespace Robot_Navigation_LRTA
{
    partial class Form1
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
            this.pbCamvas = new System.Windows.Forms.PictureBox();
            this.btnTriangle = new System.Windows.Forms.Button();
            this.btnSquare = new System.Windows.Forms.Button();
            this.tbSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_perf = new System.Windows.Forms.Label();
            this.bt_q4_2 = new System.Windows.Forms.Button();
            this.bt_q3 = new System.Windows.Forms.Button();
            this.bt_q2 = new System.Windows.Forms.Button();
            this.bt_q1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamvas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCamvas
            // 
            this.pbCamvas.BackColor = System.Drawing.SystemColors.Window;
            this.pbCamvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCamvas.Location = new System.Drawing.Point(0, 0);
            this.pbCamvas.Name = "pbCamvas";
            this.pbCamvas.Size = new System.Drawing.Size(974, 670);
            this.pbCamvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCamvas.TabIndex = 0;
            this.pbCamvas.TabStop = false;
            this.pbCamvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCamvas_Paint);
            this.pbCamvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCamvas_MouseDown);
            this.pbCamvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCamvas_MouseUp);
            // 
            // btnTriangle
            // 
            this.btnTriangle.Location = new System.Drawing.Point(6, 51);
            this.btnTriangle.Name = "btnTriangle";
            this.btnTriangle.Size = new System.Drawing.Size(71, 23);
            this.btnTriangle.TabIndex = 1;
            this.btnTriangle.Text = "Triangle";
            this.btnTriangle.UseVisualStyleBackColor = true;
            this.btnTriangle.Click += new System.EventHandler(this.btnTriangle_Click);
            // 
            // btnSquare
            // 
            this.btnSquare.Location = new System.Drawing.Point(83, 51);
            this.btnSquare.Name = "btnSquare";
            this.btnSquare.Size = new System.Drawing.Size(75, 23);
            this.btnSquare.TabIndex = 2;
            this.btnSquare.Text = "Square";
            this.btnSquare.UseVisualStyleBackColor = true;
            this.btnSquare.Click += new System.EventHandler(this.btnSquare_Click);
            // 
            // tbSize
            // 
            this.tbSize.Location = new System.Drawing.Point(59, 80);
            this.tbSize.Name = "tbSize";
            this.tbSize.Size = new System.Drawing.Size(99, 22);
            this.tbSize.TabIndex = 3;
            this.tbSize.Text = "10";
            this.tbSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSize.TextChanged += new System.EventHandler(this.tbSize_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(7, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Size :";
            // 
            // timer
            // 
            this.timer.Interval = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lbl_perf);
            this.groupBox1.Controls.Add(this.bt_q4_2);
            this.groupBox1.Controls.Add(this.bt_q3);
            this.groupBox1.Controls.Add(this.bt_q2);
            this.groupBox1.Controls.Add(this.bt_q1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnTriangle);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSquare);
            this.groupBox1.Controls.Add(this.tbSize);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(169, 226);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(83, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_perf
            // 
            this.lbl_perf.AutoSize = true;
            this.lbl_perf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_perf.Location = new System.Drawing.Point(3, 192);
            this.lbl_perf.Name = "lbl_perf";
            this.lbl_perf.Size = new System.Drawing.Size(120, 20);
            this.lbl_perf.TabIndex = 12;
            this.lbl_perf.Text = "Performance : ";
            // 
            // bt_q4_2
            // 
            this.bt_q4_2.Location = new System.Drawing.Point(83, 166);
            this.bt_q4_2.Name = "bt_q4_2";
            this.bt_q4_2.Size = new System.Drawing.Size(75, 23);
            this.bt_q4_2.TabIndex = 9;
            this.bt_q4_2.Text = "Q-4.2";
            this.bt_q4_2.UseVisualStyleBackColor = true;
            this.bt_q4_2.Click += new System.EventHandler(this.bt_q4_2_Click);
            // 
            // bt_q3
            // 
            this.bt_q3.Location = new System.Drawing.Point(12, 166);
            this.bt_q3.Name = "bt_q3";
            this.bt_q3.Size = new System.Drawing.Size(67, 23);
            this.bt_q3.TabIndex = 8;
            this.bt_q3.Text = "Q-3";
            this.bt_q3.UseVisualStyleBackColor = true;
            this.bt_q3.Click += new System.EventHandler(this.bt_q3_Click);
            // 
            // bt_q2
            // 
            this.bt_q2.Location = new System.Drawing.Point(83, 137);
            this.bt_q2.Name = "bt_q2";
            this.bt_q2.Size = new System.Drawing.Size(75, 23);
            this.bt_q2.TabIndex = 7;
            this.bt_q2.Text = "Q-2";
            this.bt_q2.UseVisualStyleBackColor = true;
            this.bt_q2.Click += new System.EventHandler(this.bt_q2_Click);
            // 
            // bt_q1
            // 
            this.bt_q1.Location = new System.Drawing.Point(10, 137);
            this.bt_q1.Name = "bt_q1";
            this.bt_q1.Size = new System.Drawing.Size(67, 23);
            this.bt_q1.TabIndex = 6;
            this.bt_q1.Text = "Q-1";
            this.bt_q1.UseVisualStyleBackColor = true;
            this.bt_q1.Click += new System.EventHandler(this.bt_q1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "AMIA Q4.11";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 670);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pbCamvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbCamvas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCamvas;
        private System.Windows.Forms.Button btnTriangle;
        private System.Windows.Forms.Button btnSquare;
        private System.Windows.Forms.TextBox tbSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_q4_2;
        private System.Windows.Forms.Button bt_q3;
        private System.Windows.Forms.Button bt_q2;
        private System.Windows.Forms.Button bt_q1;
        private System.Windows.Forms.Label lbl_perf;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

