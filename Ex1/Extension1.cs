using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    public static class Extention
    {
        public static string GetPadFootingSizeString(this string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(value);
            sb.Append("*");
            sb.Append(value);
            return sb.ToString();
        }

        public static ArrayList GetRebarGroupRadiuses(this string value)
        {
            ArrayList ret = new ArrayList();
            string[] radiuses = value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var radius in radiuses)
            {
                ret.Add(Convert.ToDouble(radius));
            }
            return ret;
        }
    }
}
