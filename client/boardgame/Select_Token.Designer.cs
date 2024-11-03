
namespace boardgame
{
    partial class Select_Token
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
            this.SuspendLayout();
            // 
            // Select_Token
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 346);
            this.Name = "Select_Token";
            this.Text = "말 색깔 선택";
            this.Load += new System.EventHandler(this.Select_Token_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Select_Token_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Select_Token_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}