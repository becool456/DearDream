using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DearDreamModels.Helper
{
    public static class Serialization
    {
        static public Dictionary<int,int> ConvertStrToDic(string strValue,string strKey)
        {
            var dicResult = new Dictionary<int, int>();
            if (string.IsNullOrEmpty(strValue) || string.IsNullOrEmpty(strKey))
                return dicResult;
            string[] key = strKey.Trim().Split('|');
            string[] value = strValue.Trim().Split('|');
            if(key.Count() == value.Count())
            {
                for(int i = 0 ; i < key.Count()-1 ; i++)
                {
                    int nKey = Int32.Parse(key[i]);
                    int nValue = Int32.Parse(value[i]);
                    dicResult.Add(nKey, nValue);
                }
            }
            return dicResult;
        }

        static public bool ConvertEnumToStr(IEnumerable<KeyValuePair<int, int> > dic,out string key,out string value)
        {
            key = string.Empty;
            value = string.Empty;
            if (dic == null)
                return false;
            foreach(var item in dic)
            {
                key += item.Key.ToString() + "|";
                value += item.Value.ToString() + "|";
            }
            return true;
        }

        static public IEnumerable<int> ConvertStrToEnum(string strKey)
        {
            IList<int> result = new List<int>();
            if (string.IsNullOrEmpty(strKey))
                return result;
            string[] key = strKey.Trim().Split('|');
            for (int i = 0; i < key.Count()-1; i++)
            {
                int nKey = Int32.Parse(key[i]);
                result.Add(nKey);
            }
            return result;
        }
    }
}
