using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Utility
{   
    public enum RoundStyle
    {
        /// <summary>
        /// �ĸ��Ƕ�����Բ�ǡ�
        /// </summary>
        None = 0,
        /// <summary>
        /// �ĸ��Ƕ�ΪԲ�ǡ�
        /// </summary>
        All = 1,
        /// <summary>
        /// ���������ΪԲ�ǡ�
        /// </summary>
        Left = 2,
        /// <summary>
        /// �ұ�������ΪԲ�ǡ�
        /// </summary>
        Right = 3,
        /// <summary>
        /// �ϱ�������ΪԲ�ǡ�
        /// </summary>
        Top = 4,
        /// <summary>
        /// �±�������ΪԲ�ǡ�
        /// </summary>
        Bottom = 5,
        /// <summary>
        /// ���½�ΪԲ�ǡ�
        /// </summary>
        BottomLeft = 6,
        /// <summary>
        /// ���½�ΪԲ�ǡ�
        /// </summary>
        BottomRight = 7,
    }

}