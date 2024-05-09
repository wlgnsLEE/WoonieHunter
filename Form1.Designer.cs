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
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.tmr_bullet = new System.Windows.Forms.Timer(this.components);
            this.tmr_spawn_enemy = new System.Windows.Forms.Timer(this.components);
            this.background1_ = new System.Windows.Forms.PictureBox();
            this.background1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.background1_)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.background1)).BeginInit();
            this.SuspendLayout();
            // 
            // tmr
            // 
            this.tmr.Interval = 1;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // tmr_bullet
            // 
            this.tmr_bullet.Interval = 1;
            this.tmr_bullet.Tick += new System.EventHandler(this.tmr_bullet_Tick);
            // 
            // tmr_spawn_enemy
            // 
            this.tmr_spawn_enemy.Interval = 1000;
            this.tmr_spawn_enemy.Tick += new System.EventHandler(this.tmr_spawn_enemy_Tick);
            // 
            // background1_
            // 
            this.background1_.BackgroundImage = global::WoonieHunter.Properties.Resources.bg_stars;
            this.background1_.Location = new System.Drawing.Point(12, 69);
            this.background1_.Name = "background1_";
            this.background1_.Size = new System.Drawing.Size(100, 50);
            this.background1_.TabIndex = 0;
            this.background1_.TabStop = false;
            // 
            // background1
            // 
            this.background1.BackgroundImage = global::WoonieHunter.Properties.Resources.bg_stars;
            this.background1.Location = new System.Drawing.Point(13, 13);
            this.background1.Name = "background1";
            this.background1.Size = new System.Drawing.Size(100, 50);
            this.background1.TabIndex = 0;
            this.background1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.background1);
            this.Controls.Add(this.background1_);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.background1_)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.background1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Timer tmr_bullet;
        private System.Windows.Forms.Timer tmr_spawn_enemy;
        private System.Windows.Forms.PictureBox background1;
        private System.Windows.Forms.PictureBox background1_;
    }
}

