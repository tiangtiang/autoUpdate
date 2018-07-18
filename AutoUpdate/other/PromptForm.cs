using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{   
    /// <summary>
    /// 提示框状态：成功、失败、警告
    /// </summary>
    /// <author>宋佳恒</author>
    /// <date>2017-04-13</date>
    public enum PromptState
    {
        Success,//成功
        Fail,   //失败
        Warn    //警告
    }
    /// <summary>
    /// 提示框，失败、成功、警告
    /// add by zhengling 2017年4月13日
    /// </summary>
    public partial class PromptForm : UI.FrameForm
    {
        //private static PromptForm pf;
        ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptForm));
        public PromptForm(PromptState state, String message)
        {
            InitializeComponent();
            try
            {   
                /**
                 * 之前使用了3个pictureBox来展示三种图标，
                 * 实际上只需要一个pictureBox就可以展示，
                 * 另外加上了对话框状态的枚举状态，通过switch case来判断
                 * Modyfied by:sjh
                 * Modyfied time:2017-04-13
                 * */
                switch(state)
                {
                    case PromptState.Success: /*this.pictureBox_success.Visible = true;
                                              this.pictureBox_fail.Visible = false;
                                              this.pictureBox_warm.Visible = false;*/
                        this.pictureBox.Image = AutoUpdate.Resource1.pictureBox_success_Image;
                                              break;
                    case PromptState.Fail:    /*this.pictureBox_success.Visible = false;
                                              this.pictureBox_fail.Visible = true;
                                              this.pictureBox_warm.Visible = false;*/
                                              this.pictureBox.Image = AutoUpdate.Resource1.pictureBox_fail_Image;
                                              break;
                    case PromptState.Warn:    /*this.pictureBox_success.Visible = false;
                                              this.pictureBox_fail.Visible = false;
                                              this.pictureBox_warm.Visible = true;*/

                                              this.pictureBox.Image = AutoUpdate.Resource1.pictureBox_warn_Image;
                                              break;
                    default:break;
                }         
                //this.label1.Text = message;
                this.textMessage.Text = message;
                this.pictureBox.Size = pictureBox.Image.Size;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public static void ShowPromptForm(Form parent, PromptState state, String message)
        {
            PromptForm pf = new PromptForm(state, message);
            //pf.TopLevel = false;
            //parent.Controls.Add(pf);
            //pf.Parent = parent;
            //pf.Location = new Point((parent.Width - pf.Width) / 2, (parent.Height - pf.Height) / 2);
            pf.StartPosition = FormStartPosition.CenterScreen;
            // pf.BringToFront();
            //pf.CenterToParent();
            pf.ShowDialog();
        }     
            //pf.Dispose();

        private void iButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 加载时调用，修改提示框的样式
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //准备显示的文字
            //string msg = textMessage.Text;
            string msg = textMessage.Text.Replace('\n', ' ');
            //显示文字区域的最大宽度
            int threshold = Convert.ToInt32(this.contentPanel.Width * 0.7);
            //显示为文字的像素大小
            SizeF textSize = CreateGraphics().MeasureString(msg, textMessage.Font);
            
            if (threshold < textSize.Width)
            {
                textMessage.AutoSize = false;
                textMessage.Width = threshold;
                //计算行数
                int lineNums = Convert.ToInt32(textSize.Width / threshold) + 1;
                //获得文本区域的高度
                textMessage.Height = lineNums * (Convert.ToInt32(textSize.Height) + 1);
            }
            //文字或图标与按钮的上下间距
            int divBoder = 15;
            //文字与上边距或按钮与下边距的距离
            int border = (contentPanel.Height - (textMessage.Height > pictureBox.Height ? textMessage.Height : pictureBox.Height) -
                divBoder - iButton1.Height)/2;
            if (border < 30)
                border = 30;
            //文字与图标的间距
            int margin = 10;
            
            //文本的高度大于图标的高度
            if (textMessage.Height > pictureBox.Height)
            {
                //先设置文本的位置
                textMessage.Location = new Point((this.contentPanel.Width - this.textMessage.Width) / 2 + this.pictureBox.Width / 2 + margin / 2,
                    border);
                //在设置图标的位置
                this.pictureBox.Location = new Point(this.textMessage.Location.X - this.pictureBox.Width - margin,
                    this.textMessage.Location.Y + ((textMessage.Height - pictureBox.Height) / 2));
                //设置按钮的位置
                iButton1.Location = new Point((this.contentPanel.Width - iButton1.Width) / 2,
                                    this.textMessage.Location.Y + this.textMessage.Height + divBoder);

            }
            else
            {
                //先设置图标的位置
                this.pictureBox.Location = new Point((this.contentPanel.Width - this.textMessage.Width - this.pictureBox.Width - margin) / 2,
                    border);
                //在设置文本的位置
                this.textMessage.Location = new Point(this.pictureBox.Location.X + this.pictureBox.Width + margin,
                    this.pictureBox.Location.Y + (pictureBox.Height - textMessage.Height) / 2);
                //设置按钮的位置
                iButton1.Location = new Point((this.contentPanel.Width - iButton1.Width) / 2,
                                this.pictureBox.Location.Y + this.pictureBox.Height + divBoder);
            }
                
            //如果超过长度，就延长窗体
            if ((iButton1.Location.Y + iButton1.Height + border) > contentPanel.Height)
            {
                this.Height += (iButton1.Location.Y + iButton1.Height + border) - contentPanel.Height;
            }
        }
    }
}
