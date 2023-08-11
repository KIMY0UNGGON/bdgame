namespace boardgame
{
    partial class Select_build
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
            this.build_v = new System.Windows.Forms.CheckBox();
            this.build_b = new System.Windows.Forms.CheckBox();
            this.build_h = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.build_g = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // build_v
            // 
            this.build_v.AutoSize = true;
            this.build_v.Location = new System.Drawing.Point(68, 50);
            this.build_v.Name = "build_v";
            this.build_v.Size = new System.Drawing.Size(48, 16);
            this.build_v.TabIndex = 0;
            this.build_v.Text = "별장";
            this.build_v.UseVisualStyleBackColor = true;
            this.build_v.CheckedChanged += new System.EventHandler(this.build_v_CheckedChanged);
            // 
            // build_b
            // 
            this.build_b.AutoSize = true;
            this.build_b.Location = new System.Drawing.Point(156, 50);
            this.build_b.Name = "build_b";
            this.build_b.Size = new System.Drawing.Size(48, 16);
            this.build_b.TabIndex = 1;
            this.build_b.Text = "빌딩";
            this.build_b.UseVisualStyleBackColor = true;
            this.build_b.CheckedChanged += new System.EventHandler(this.build_b_CheckedChanged);
            // 
            // build_h
            // 
            this.build_h.AutoSize = true;
            this.build_h.Location = new System.Drawing.Point(225, 50);
            this.build_h.Name = "build_h";
            this.build_h.Size = new System.Drawing.Size(48, 16);
            this.build_h.TabIndex = 1;
            this.build_h.TabStop = false;
            this.build_h.Text = "호텔";
            this.build_h.UseVisualStyleBackColor = true;
            this.build_h.CheckedChanged += new System.EventHandler(this.build_h_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.build_g);
            this.groupBox1.Controls.Add(this.build_v);
            this.groupBox1.Controls.Add(this.build_h);
            this.groupBox1.Controls.Add(this.build_b);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 109);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "건물";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // build_g
            // 
            this.build_g.AutoSize = true;
            this.build_g.Location = new System.Drawing.Point(8, 50);
            this.build_g.Name = "build_g";
            this.build_g.Size = new System.Drawing.Size(36, 16);
            this.build_g.TabIndex = 2;
            this.build_g.Text = "땅";
            this.build_g.UseVisualStyleBackColor = true;
            this.build_g.CheckedChanged += new System.EventHandler(this.build_g_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "확인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Select_build
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 152);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Select_build";
            this.Text = "건물선택";
            this.Load += new System.EventHandler(this.Select_build_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox build_v;
        private System.Windows.Forms.CheckBox build_b;
        private System.Windows.Forms.CheckBox build_h;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox build_g;
        private System.Windows.Forms.Label label1;
    }
}