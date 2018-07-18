using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace UI
{
    /// <summary>
    /// 进度条页面
    /// </summary>
    public partial class Progress : Form
    {
        /// <summary>
        /// 后台更新下载程序
        /// </summary>
        private UpdateProgram update;
        /// <summary>
        /// 设置更新下载程序
        /// </summary>
        /// <param name="up"></param>
        public void setUpdateProgram(UpdateProgram up = null)
        {
            if (up == null){
                update = new UpdateProgram();
                update.init();
            }
            else
                update = up;
            initValue(update.UpdateList.Count);
        }
        /// <summary>
        /// 进度条
        /// </summary>
        private NewProcessBar bar;
        /// <summary>
        /// 显示内容窗口
        /// </summary>
        private ContentBox contentbox;
        /// <summary>
        /// 下载失败的文件列表
        /// </summary>
        public List<String> FailedList = new List<string>();
        /// <summary>
        /// 初始化变量
        /// </summary>
        public Progress()
        {
            InitializeComponent();
            this.Hide();
            bar = new NewProcessBar();
            bar.BringToFront();
            this.Controls.Add(bar);
            bar.Location = new Point(line.Location.X, line.Location.Y-bar.Height/2);
            bar.setWidth(0);

            contentbox = new ContentBox();
            this.Controls.Add(contentbox);
            contentbox.Location = new Point(81, 106);
            contentbox.Parent = backImg;

            bar.Parent = backImg;
            logoPic.Parent = backImg;
            title.Parent = backImg;
            btnMini.Parent = backImg;
            btnClose.Parent = backImg;

            backImg.MouseDown += new MouseEventHandler(mouseDown);
            backImg.MouseMove += new MouseEventHandler(mouseMove);
            backImg.MouseUp += new MouseEventHandler(mouseUp);
        }
        /// <summary>
        /// 是否需要更新文件
        /// </summary>
        /// <returns></returns>
        public int needUpdate()
        {
            return update.needUpdate();
        }
        /// <summary>
        /// 进度条每一步前进的距离
        /// </summary>
        private int stepLength;
        private void initValue(int downloadFileNum){
            if (downloadFileNum == 0)
            {
                stepLength = 0;
            }
            else
            {
                stepLength = this.line.Width / downloadFileNum;
            }
        }
        /// <summary>
        /// 更新进度条位置
        /// </summary>
        /// <param name="ipos">进度条位置</param>
        private delegate void DelegateFunction(int ipos);
        /// <summary>
        /// 关闭窗口
        /// </summary>
        private delegate void FormClose();
        /// <summary>
        /// 更新提示内容信息
        /// </summary>
        /// <param name="info">提示信息</param>
        private delegate void ChangeContent(String info);
        /// <summary>
        /// 下载是否成功的事件
        /// </summary>
        /// <param name="isSuccess">是否完全成功</param>
        public delegate void UpdateSuccess(bool isSuccess);
        public event UpdateSuccess SuccessOrNot;
        //执行函数  
        private void StartWork()
        {
            
            int count = 1;
            foreach (string name in update.UpdateList)
            {
                changeThisContent(String.Format("{0}/{1}: {2} 正在下载中...\n", count, update.UpdateList.Count, name));

                if (update.downloadFile(name))
                {      //下载文件
                    changeThisContent(String.Format("{0}/{1}: {2} 下载完成\n", count, update.UpdateList.Count, name));
                }
                else
                {
                    changeThisContent(String.Format("{0}/{1}: {2} 下载失败\n", count, update.UpdateList.Count, name));
                    FailedList.Add(name);
                } 
                SetPos(count);
                count++;
            }
            SetPos(count);
            String messge = "";
            if (FailedList.Count == 0)
            {
                if (SuccessOrNot != null)
                    SuccessOrNot(true);
                messge = "更新完成";
            }
            else
            {
                if (SuccessOrNot != null)
                    SuccessOrNot(false);
                messge = "部分文件下载失败！将重新打开未更新前的系统";
            }
            UI.PromptForm promt = new PromptForm(PromptState.Success, messge);
            DialogResult result = promt.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                update.closeConnect();
                closeThis();
            }
        }
        //设置进度条的Value  
        private void SetPos(int ipos)
        {
            if (this.bar.InvokeRequired)
            {
                DelegateFunction df = new DelegateFunction(SetPos);
                this.Invoke(df, new object[] { ipos});
            }
            else
            {
                bar.setWidth(stepLength * ipos);
            }

        }
        /// <summary>
        /// 关闭该窗口
        /// </summary>
        private void closeThis()
        {
            if (this.InvokeRequired)
            {
                FormClose fc = new FormClose(closeThis);
                this.Invoke(fc);
            }
            else
            {
                //退出整个程序，执行更新后开启新程序
                Application.Exit();
            }
        }
        /// <summary>
        /// 改变content面板中的内容
        /// </summary>
        /// <param name="info">内容</param>
        private void changeThisContent(string info)
        {
            if (contentbox.InvokeRequired)
            {
                ChangeContent cc = new ChangeContent(changeThisContent);
                this.Invoke(cc, new object[] { info });
            }
            else
            {
                try
                {
                    contentbox.Text += info;
                    //移动光标
                    contentbox.Select(contentbox.Text.Length, 0);
                    contentbox.ScrollToCaret();
                }
                catch (Exception) { }
                
            }
        }
        /// <summary>
        /// 下载线程
        /// </summary>
        private Thread progressThread;
        /// <summary>
        /// 下载文件
        /// </summary>
        public void downloadFiles()
        {
            this.Show();
            progressThread = new Thread(StartWork);
            progressThread.IsBackground = true;
            progressThread.Start();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #region 窗体移动
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
                this.Location = new Point(posX - currentPosX, posY - currentPosY);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
