using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace UI
{
    public class NewProcessBar:Panel
    {
        private Label left, right;

        private void initLabel()
        {
            left = new Label();
            left.Text = "";
            this.Controls.Add(left);
            left.Image = AutoUpdate.Resource1.left;
            left.Size = left.Image.Size;
            left.BackColor = Color.FromArgb(36,60,88);
            left.Location = new Point(0, 0);

            right = new Label();
            right.Text = "";
            this.Controls.Add(right);
            right.Image = AutoUpdate.Resource1.right;
            right.Size = right.Image.Size;
            right.BackColor = Color.FromArgb(36, 60, 88);
            right.Location = new Point(this.Width - right.Width, 0);
        }
        public NewProcessBar()
        {
            base.SetStyle(ControlStyles.UserPaint, true);
            this.Height = 8;
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand;
            initLabel();
        }

        public void setWidth(int width)
        {
            this.Width = width;
            right.Location = new Point(this.Width - right.Width, 0);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    int arc = this.Height/2 ;
        //    Graphics graph = e.Graphics;
        //    graph.SmoothingMode = SmoothingMode.HighQuality;    //抗锯齿
        //    graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //    graph.CompositingQuality = CompositingQuality.HighQuality;

        //    Pen pen = new Pen(Color.Transparent, 0);        //创建画笔
        //    GraphicsPath path = new GraphicsPath();         //创建绘制路径

        //    path.AddArc(0, 0, Height, Height, 90, 180);
        //    path.AddLine(Height, 0, this.Width - Height, 0);
        //    path.AddArc(Width - Height, 0, Height, Height, 270, 180);
        //    path.AddLine(Width-Height, Height, Height, Height);
        //    this.Region = new System.Drawing.Region(path);      //将路径赋值为控件的边框
        //    graph.DrawPath(pen, path);          //绘制路径
        //}
    }
}
