﻿namespace WoonieHunter
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
            this.SuspendLayout();
            // 
            // lbRankList
            // 
            this.lbRankList.AutoSize = true;
            this.lbRankList.Location = new System.Drawing.Point(12, 9);
            this.lbRankList.Name = "lbRankList";
            this.lbRankList.Size = new System.Drawing.Size(38, 12);
            this.lbRankList.TabIndex = 0;
            this.lbRankList.Text = "label1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 272);
            this.Controls.Add(this.lbRankList);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbRankList;
    }
}