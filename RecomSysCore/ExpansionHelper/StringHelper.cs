using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecomSysCore.ExpansionHelper
{
    public static class StringHelper
    {
        public static DateTime getByIndex(this string str, int index)
        {
            if (index < 0 || index < str.TimeSize())
                throw new Exception("字符串下标越界！");
            char [] buf = new char[40];
            int  n = 0 ; 
            for(int i = 0 ; i < index+1 ; i++)
            {
                if( str[i] == ';' )
                {
                    buf[n] = '\0';
                }
                else
                {
                    buf[n++] = str[i];
                }
            }
            string tmp = new string(buf);
            return Convert.ToDateTime(tmp);
        }

        public static int TimeSize(this string str)
        {
            string singleTime = DateTime.Now.ToString() + ";";
            return str.Length / singleTime.Length;
        }

        public static string AddTime(this string str,DateTime dt)
        {
            string addStr = dt.ToString() + ";";
            return str + addStr;
        }
    }
}
