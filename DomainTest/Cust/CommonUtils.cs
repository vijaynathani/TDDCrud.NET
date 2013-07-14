using System;
using System.Text;

namespace DomainTest.Cust
{
    internal class CommonUtils
    {
        public static String GetRandomStringNumeric(int length)
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= length; i++)
                sb.Append("" + (i%10));
            return sb.ToString();
        }

        public static String GetRandomString(int length)
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= length; i++)
                sb.Append('a');
            return sb.ToString();
        }
    }
}