using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using JWBControl;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Utility;
namespace UI
{   

    /// <summary>
    /// 无边框窗体，可以拖动和缩放
    /// </summary>
    /// <author>tiang</author>
    /// <time>2017/04/13</time>
    public partial class FrameForm : DecorateForm
    {
       
       private SkinForm skin;

        #region 变量属性
        //不显示FormBorderStyle属性
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = FormBorderStyle.None; }
        }
        #endregion

        #region 重载事件
        //Show或Hide被调用时
        protected override void OnVisibleChanged(EventArgs e) {
            if (Visible) {
                //启用窗口淡入淡出
                if (!DesignMode) {
                    //淡入特效
                    //Win32.AnimateWindow(this.Handle, 150, Win32.AW_BLEND | Win32.AW_ACTIVATE);
                }
                //判断不是在设计器中
                if (!DesignMode && skin == null) {
                    skin = new SkinForm(this);
                    skin.Show(this);
                }
                base.OnVisibleChanged(e);
            } else {
                base.OnVisibleChanged(e);
                //Win32.AnimateWindow(this.Handle, 150, Win32.AW_BLEND | Win32.AW_HIDE);
            }
        }

        //窗体关闭时
        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            //先关闭阴影窗体
            if (skin != null) {
                skin.Close();
            }
            //在Form_FormClosing中添加代码实现窗体的淡出
            //Win32.AnimateWindow(this.Handle, 150, Win32.AW_BLEND | Win32.AW_HIDE);
        }

        //控件首次创建时被调用
        protected override void OnCreateControl() {
            base.OnCreateControl();
            SetReion();
        }

        //圆角
        private void SetReion() {
            using (GraphicsPath path =
                    GraphicsPathHelper.CreatePath(
                    new Rectangle(Point.Empty, base.Size), 6, RoundStyle.All, true)) {
                Region region = new Region(path);
                path.Widen(Pens.White);
                region.Union(path);
                this.Region = region;
            }
        }

        //改变窗体大小时
        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            SetReion();
        }

        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;


        #region 允许点击任务栏最小化
        protected override CreateParams CreateParams {
            get {
                const int WS_MINIMIZEBOX = 0x00020000;  
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;   // 允许最小化操作
                return cp;
            }
        }
        #endregion

       


        private int border = 5; //边距
        /// <summary>
        /// 标题字体设置
        /// </summary>
        public Font titleFont
        {
            set
            {
                this.title.Font = value;
            }
            get{
                return this.title.Font;
            }
        }
        /// <summary>
        /// 标题字体颜色设置
        /// </summary>
        public Color titleColor
        {
            set
            {
                this.title.ForeColor = value;
            }
            get
            {
                return this.title.ForeColor;
            }
        }

        /// <summary>
        /// 图标图片设置
        /// </summary>
        public Image iconImg
        {
            set
            {
                icon.BackgroundImage = value;
            }
            get
            {
                return icon.BackgroundImage;
            }
        }
        /// <summary>
        /// 标题名设置
        /// </summary>
        public string titleText
        {
            set
            {
                title.Text = value;
                this.Text = value;
            }
            get
            {
                return title.Text;
            }
        }

        private int titleHeight;        //标题栏高度
        public FrameForm()
        {
            InitializeComponent();
            SetStyles();
            //SetBitmap(UI.Properties.Resources.back);
            this.BackColor = Color.MidnightBlue;
            this.titleColor = Color.White;
            contentPanel.Location = new Point(0, 40);
            //计算标题栏高度
            //titleHeight = this.Height - contentPanel.Height - border;
            titleHeight = this.titlePanel.Height;
            this.contentPanel.Height = this.Height - this.titlePanel.Height;
            //this.TopMost = true;
            //this.TopLevel = false;
            titlePanel.MouseDown += new MouseEventHandler(mouseDown);
            titlePanel.MouseUp += new MouseEventHandler(mouseUp);
            titlePanel.MouseMove += new MouseEventHandler(mouseMove);
        }

        #region 窗体移动事件
        private bool buttonDown = false;
        private int posX, posY;         //鼠标坐标
        private int currentPosX, currentPosY;         //鼠标当前坐标与控件位置的差
        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (buttonDown)
            {
                /* *
                 * 获取鼠标坐标在菜单中的相对坐标
                 * */
                //posX = this.PointToClient(Control.MousePosition).X;
                //posY = this.PointToClient(Control.MousePosition).Y;
                posX = Control.MousePosition.X;
                posY = Control.MousePosition.Y;
                this.Location = new Point(posX-currentPosX, posY-currentPosY);
                //this.Location = new Point(posX, posY);
            }
        }
        private void mouseDown(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            if (e.Button == MouseButtons.Left)      //左键点击， 选中控件，准备拖动
            {
                buttonDown = true;
                /**
                * 计算鼠标坐标与图表坐标的差值
                * */
                currentPosX = Control.MousePosition.X - this.Location.X;
                currentPosY = Control.MousePosition.Y - this.Location.Y;
            }
        }
        private void mouseUp(object sender, MouseEventArgs e)
        {
            buttonDown = false;
            //this.Location = new Point(posX, posY);
        }
        #endregion

        #region 减少闪烁
        private void SetStyles()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            base.AutoScaleMode = AutoScaleMode.None;
        }
        #endregion

        #region 窗口拖动与缩放
        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201:                //鼠标左键按下的消息 
                    m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero; //默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
            //base.WndProc(ref m);

        }
        #endregion
        /// <summary>
        /// 按钮关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, EventArgs e)
        {
            //MainForm main = (MainForm)this.FindForm();
            //main.RemoveFormFromDict(this.GetType());
            this.Close();
        }
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameForm_Load(object sender, EventArgs e)
        {
            setCloseButtonPos();
            if (iconImg != null)        //如果没有设置图片，修改标题文字的位置
            {
                icon.Show();
                icon.Location = new Point(border, border);
                icon.Size = new System.Drawing.Size(titleHeight - 2 * border, titleHeight - 2 * border);
                title.Location = new Point(border + icon.Width, border);
            }
            else
            {           //如果设置了图片，设置标题文字的位置
                icon.Hide();
                title.Location = new Point(border, border);
            }
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void setCloseButtonPos()
        {
            close.Location = new Point(this.Width - close.Width - border, border);
        }
    }
}
        #endregion