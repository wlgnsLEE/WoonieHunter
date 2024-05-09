namespace WoonieHunter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.character = new System.Windows.Forms.PictureBox();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.bullet = new System.Windows.Forms.PictureBox();
            this.tmr_bullet = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.character)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bullet)).BeginInit();
            this.SuspendLayout();
            // 
            // character
            // 
            this.character.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("character.BackgroundImage")));
            this.character.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.character.Location = new System.Drawing.Point(370, 349);
            this.character.Name = "character";
            this.character.Size = new System.Drawing.Size(50, 100);
            this.character.TabIndex = 0;
            this.character.TabStop = false;
            // 
            // tmr
            // 
            this.tmr.Interval = 1;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // bullet
            // 
            this.bullet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bullet.BackgroundImage")));
            this.bullet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bullet.Location = new System.Drawing.Point(388, 312);
            this.bullet.Name = "bullet";
            this.bullet.Size = new System.Drawing.Size(15, 31);
            this.bullet.TabIndex = 1;
            this.bullet.TabStop = false;
            this.bullet.Visible = false;
            // 
            // tmr_bullet
            // 
            this.tmr_bullet.Interval = 1;
            this.tmr_bullet.Tick += new System.EventHandler(this.tmr_bullet_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.bullet);
            this.Controls.Add(this.character);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.character)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bullet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox character;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.PictureBox bullet;
        private System.Windows.Forms.Timer tmr_bullet;
    }
}

