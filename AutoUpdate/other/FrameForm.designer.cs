namespace UI
{
    partial class FrameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrameForm));
            this.contentPanel = new System.Windows.Forms.Panel();
            this.title = new System.Windows.Forms.Label();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.icon = new System.Windows.Forms.PictureBox();
            this.close = new System.Windows.Forms.Button();
            this.titlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.SuspendLayout();
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 40);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(200, 110);
            this.contentPanel.TabIndex = 0;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(12, 4);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(0, 25);
            this.title.TabIndex = 1;
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titlePanel
            // 
            this.titlePanel.Controls.Add(this.icon);
            this.titlePanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(200, 40);
            this.titlePanel.TabIndex = 4;
            // 
            // icon
            // 
            this.icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.icon.Location = new System.Drawing.Point(3, 3);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(100, 32);
            this.icon.TabIndex = 3;
            this.icon.TabStop = false;
            this.icon.Visible = false;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.FlatAppearance.BorderSize = 0;
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close.Image = global::AutoUpdate.Resource1.dialogClose;
            this.close.Location = new System.Drawing.Point(172, 2);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(20, 20);
            this.close.TabIndex = 2;
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // FrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(82)))), ((int)(((byte)(133)))));
            this.ClientSize = new System.Drawing.Size(200, 150);
            this.Controls.Add(this.close);
            this.Controls.Add(this.title);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.titlePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(200, 150);
            this.Name = "FrameForm";
            this.ShowInTaskbar = true;
            this.Text = "爱课熊";
            this.Load += new System.EventHandler(this.FrameForm_Load);
            this.titlePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.PictureBox icon;
        public System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel titlePanel;

    }
}