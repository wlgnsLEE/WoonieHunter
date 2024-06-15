namespace WoonieHunter
{
    partial class Form2
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
            this.lbRankList = new System.Windows.Forms.Label();
            this.lbRank = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbRankList
            // 
            this.lbRankList.AutoSize = true;
            this.lbRankList.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbRankList.Location = new System.Drawing.Point(50, 77);
            this.lbRankList.Name = "lbRankList";
            this.lbRankList.Size = new System.Drawing.Size(0, 16);
            this.lbRankList.TabIndex = 0;
            // 
            // lbRank
            // 
            this.lbRank.AutoSize = true;
            this.lbRank.BackColor = System.Drawing.Color.Transparent;
            this.lbRank.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbRank.Location = new System.Drawing.Point(64, 29);
            this.lbRank.Name = "lbRank";
            this.lbRank.Size = new System.Drawing.Size(68, 27);
            this.lbRank.TabIndex = 0;
            this.lbRank.Text = "랭킹";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::WoonieHunter.Properties.Resources.bg_stars1;
            this.ClientSize = new System.Drawing.Size(194, 272);
            this.Controls.Add(this.lbRank);
            this.Controls.Add(this.lbRankList);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbRankList;
        private System.Windows.Forms.Label lbRank;
    }
}