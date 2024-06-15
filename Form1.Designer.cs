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
            this.tmr_item_spawn = new System.Windows.Forms.Timer(this.components);
            this.background2_ = new System.Windows.Forms.PictureBox();
            this.background2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.background2_)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.background2)).BeginInit();
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
            // tmr_item_spawn
            // 
            this.tmr_item_spawn.Interval = 3000;
            this.tmr_item_spawn.Tick += new System.EventHandler(this.tmr_item_spawn_Tick);
            // 
            // background2_
            // 
            this.background2_.Image = global::WoonieHunter.Properties.Resources.tuto;
            this.background2_.Location = new System.Drawing.Point(197, 13);
            this.background2_.Name = "background2_";
            this.background2_.Size = new System.Drawing.Size(72, 272);
            this.background2_.TabIndex = 1;
            this.background2_.TabStop = false;
            this.background2_.Visible = false;
            // 
            // background2
            // 
            this.background2.Image = global::WoonieHunter.Properties.Resources.bg_planet;
            this.background2.Location = new System.Drawing.Point(119, 12);
            this.background2.Name = "background2";
            this.background2.Size = new System.Drawing.Size(72, 272);
            this.background2.TabIndex = 1;
            this.background2.TabStop = false;
            this.background2.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.background2_);
            this.Controls.Add(this.background2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.background2_)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.background2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Timer tmr_bullet;
        private System.Windows.Forms.Timer tmr_spawn_enemy;
        private System.Windows.Forms.PictureBox background2;
        private System.Windows.Forms.PictureBox background2_;
        private System.Windows.Forms.Timer tmr_item_spawn;
    }
}

