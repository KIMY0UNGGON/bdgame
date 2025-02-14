namespace boardgame
{
    partial class GameMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.butt_solo = new System.Windows.Forms.Button();
            this.butt_multi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(627, 621);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // butt_solo
            // 
            this.butt_solo.Location = new System.Drawing.Point(86, 70);
            this.butt_solo.Name = "butt_solo";
            this.butt_solo.Size = new System.Drawing.Size(75, 23);
            this.butt_solo.TabIndex = 1;
            this.butt_solo.Text = "솔로모드";
            this.butt_solo.UseVisualStyleBackColor = true;
            this.butt_solo.Click += new System.EventHandler(this.butt_solo_Click_1);
            // 
            // butt_multi
            // 
            this.butt_multi.Location = new System.Drawing.Point(86, 117);
            this.butt_multi.Name = "butt_multi";
            this.butt_multi.Size = new System.Drawing.Size(75, 23);
            this.butt_multi.TabIndex = 2;
            this.butt_multi.Text = "멀티플레이";
            this.butt_multi.UseVisualStyleBackColor = true;
            this.butt_multi.Click += new System.EventHandler(this.butt_multi_Click);
            // 
            // GameMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(984, 961);
            this.Controls.Add(this.butt_multi);
            this.Controls.Add(this.butt_solo);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "GameMain";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button butt_solo;
        private System.Windows.Forms.Button butt_multi;
    }
}

