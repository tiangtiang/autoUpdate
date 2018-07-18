using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace UI
{   
    /// <summary>
    /// 修饰窗口,所有窗口的基类
    /// </summary>
    /// <author>宋佳恒</author>
    /// <date>2017-04-05</date>
    public partial class DecorateForm : Form
    {
        public string FormID { get; set; }
        /// <summary>
        /// BaseForm的构造函数
        /// </summary>
        /// <author>宋佳恒</author>
        /// <date>2017-03-30</date>
        /// 
        new public void Show()
        {
            base.Show();
        }
        public DecorateForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 参数类型转换函数，将参数转换成目标类型的值。
        /// 由于.net自带的参数转换函数不支持某些类型的转换，所以重写了该方法。
        /// </summary>
        /// <param name="value">待转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>目标类型的值</returns>
        /// <author>宋佳恒</author>
        /// <date>2017-03-29</date>
        /// <exception cref="System.InvalidCastException">类型转换异常</exception>               
        protected static object ConvertValueType(object value,Type targetType)
        {   
            //转换后的值
            object transValue=null;
            try
            {   
                if(null==value)
                {
                    throw new InvalidCastException("待转换的值为空");
                }
                if(null==targetType)
                {
                    throw new InvalidCastException("目标类型为空");
                }  
                string bathPath=AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                switch(targetType.ToString())
                {
                    case "System.Drawing.Color": transValue = System.Drawing.ColorTranslator.FromHtml((string)value); break;                                                                                     
                    case "System.Drawing.Bitmap": transValue = Image.FromFile(bathPath +"\\"+ (string)value); break;
                    case "System.Drawing.Image": transValue = Image.FromFile(bathPath + "\\" + (string)value); break;
                    case "System.Windows.Forms.ImageLayout": transValue = (ImageLayout)Enum.Parse(typeof(ImageLayout), (string)value); break;
                    case "System.Drawing.Font": FontConverter fc=new FontConverter();
                                                transValue = fc.ConvertFromInvariantString((string)value);break;
                    default: transValue = Convert.ChangeType(value, targetType) ; break;
                }
            }
            catch(InvalidCastException ex)
            {
                throw new InvalidCastException("值"+value+"转换为"+targetType+"失败");
            }
            return transValue;
        }
      

    }

}
