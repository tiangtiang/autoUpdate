using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace UI
{
    /// <summary>
    /// 重写富文本框，使之能够显示背景
    /// </summary>
    /// <author>tiang</author>
    /// <date>2017/5/26</date>
    class ContentBox:RichTextBox
    {
        public ContentBox()
        {
            this.ReadOnly = true;
            this.ScrollBars = RichTextBoxScrollBars.None;
            this.Size = new Size(425, 210);
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ForeColor = Color.White;
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }
        /// <summary>
        /// 设置背景色为透明
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
    }
}
