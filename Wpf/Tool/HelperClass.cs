using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Tool
{
    interface IHelperClass
    {   
        bool IsNumeric(string str);//判断字符串是否为数字
    }
    class  HelperClass : IHelperClass
    {
        /// <summary>
        /// 验证字符串是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)    //验证这个参数是否为空
            {
                return false; //是，就返回False
            }
            ASCIIEncoding ascii = new ASCIIEncoding();//new ASCIIEncoding 的实例
            byte[] bytestr = ascii.GetBytes(str);         //把string类型的参数保存到数组里
            foreach (byte c in bytestr)                   //遍历这个数组里的内容
            {
                if (c < 48 || c > 57)                          //判断是否为数字
                {
                    return false;                              //不是，就返回False
                }
            }
            return true;                                        //是，就返回True
        }

    }
}
