namespace UI
{
    partial class PromptForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptForm));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.iButton1 = new IButton.IButton();
            this.textMessage = new System.Windows.Forms.Label();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.textMessage);
            this.contentPanel.Controls.Add(this.iButton1);
            this.contentPanel.Controls.Add(this.pictureBox);
            this.contentPanel.Size = new System.Drawing.Size(404, 191);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(39, 47);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(45, 50);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // iButton1
            // 
            this.iButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iButton1.BackColor = System.Drawing.Color.White;
            this.iButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iButton1.BtnStyle = IButton.ButtonStyle.Confirm;
            this.iButton1.EnableImage = ((System.Drawing.Image)(resources.GetObject("iButton1.EnableImage")));
            this.iButton1.FlatAppearance.BorderSize = 0;
            this.iButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.iButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.iButton1.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(194)))), ((int)(((byte)(227)))));
            this.iButton1.GapBetweenImgAndLeft = 20;
            this.iButton1.GapBetweenImgAndText = 30;
            this.iButton1.Image = ((System.Drawing.Image)(resources.GetObject("iButton1.Image")));
            this.iButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iButton1.Location = new System.Drawing.Point(123, 124);
            this.iButton1.Name = "iButton1";
            this.iButton1.OnMouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(249)))), ((int)(((byte)(248)))));
            this.iButton1.OnMouseEnterColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.iButton1.Radius = 3F;
            this.iButton1.Size = new System.Drawing.Size(130, 35);
            this.iButton1.TabIndex = 7;
            this.iButton1.Text = "确   定";
            this.iButton1.UseVisualStyleBackColor = false;
            this.iButton1.Click += new System.EventHandler(this.iButton1_Click);
            // 
            // textMessage
            // 
            this.textMessage.AutoSize = true;
            this.textMessage.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.textMessage.Location = new System.Drawing.Point(154, 47);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(0, 25);
            this.textMessage.TabIndex = 8;
            // 
            // PromptForm
            // 
            this.ClientSize = new System.Drawing.Size(404, 231);
            this.Name = "PromptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "提示";
            this.titleText = "提示";
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private IButton.IButton iButton1;
        private System.Windows.Forms.Label textMessage;
    }
}
