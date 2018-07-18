namespace UI
{
    partial class Progress
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
            this.line = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.logoPic = new System.Windows.Forms.PictureBox();
            this.btnMini = new System.Windows.Forms.Button();
            this.backImg = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backImg)).BeginInit();
            this.SuspendLayout();
            // 
            // line
            // 
            this.line.BackColor = System.Drawing.Color.White;
            this.line.Location = new System.Drawing.Point(52, 347);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(480, 2);
            this.line.TabIndex = 3;
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(186, 28);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(373, 57);
            this.title.TabIndex = 4;
            this.title.Text = "正在安装，全新体验即将开始";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logoPic
            // 
            this.logoPic.BackColor = System.Drawing.Color.Transparent;
            this.logoPic.BackgroundImage = global::AutoUpdate.Resource1.logo;
            this.logoPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logoPic.Location = new System.Drawing.Point(54, 28);
            this.logoPic.Name = "logoPic";
            this.logoPic.Size = new System.Drawing.Size(127, 57);
            this.logoPic.TabIndex = 2;
            this.logoPic.TabStop = false;
            // 
            // btnMini
            // 
            this.btnMini.BackColor = System.Drawing.Color.Transparent;
            this.btnMini.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(60)))), ((int)(((byte)(88)))));
            this.btnMini.FlatAppearance.BorderSize = 0;
            this.btnMini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMini.Image = global::AutoUpdate.Resource1.mini;
            this.btnMini.Location = new System.Drawing.Point(535, 0);
            this.btnMini.Name = "btnMini";
            this.btnMini.Size = new System.Drawing.Size(23, 23);
            this.btnMini.TabIndex = 6;
            this.btnMini.UseVisualStyleBackColor = false;
            this.btnMini.Click += new System.EventHandler(this.btnMini_Click);
            // 
            // backImg
            // 
            this.backImg.BackgroundImage = global::AutoUpdate.Resource1.软件安装;
            this.backImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backImg.Location = new System.Drawing.Point(0, 0);
            this.backImg.Name = "backImg";
            this.backImg.Size = new System.Drawing.Size(600, 400);
            this.backImg.TabIndex = 7;
            this.backImg.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(60)))), ((int)(((byte)(88)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::AutoUpdate.Resource1.dialogClose;
            this.btnClose.Location = new System.Drawing.Point(565, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMini);
            this.Controls.Add(this.title);
            this.Controls.Add(this.line);
            this.Controls.Add(this.logoPic);
            this.Controls.Add(this.backImg);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Progress";
            this.Text = "Progress";
            ((System.ComponentModel.ISupportInitialize)(this.logoPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox logoPic;
        private System.Windows.Forms.Label line;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button btnMini;
        private System.Windows.Forms.PictureBox backImg;
        private System.Windows.Forms.Button btnClose;
    }
}