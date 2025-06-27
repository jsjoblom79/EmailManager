using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailManager
{
    public static class ExtentionMethods
    {

        public static DateTime ToTime(this string data)
        {
            DateTime.TryParse(data, out DateTime result);
            return result;
        }
        public static DateTime ToDate(this string data)
        {
            DateTime.TryParse(data, out DateTime result);
            return result;
        }

        public static string Clean(this string data)
        {
            var result = data.Trim();
            if (data.Contains("'"))
            {
                result = data.Replace("'", "");
            }

            return result;
        }

        public static string IsValidEmail(this string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (Regex.IsMatch(email.Clean(), emailPattern)) { return email.Clean(); }

            return null;
        }
        public static IEnumerable<List<T>> Partition<T>(this List<T> values, int chunkSize)
        {
            for (int i = 0; i < values.Count; i += chunkSize)
            {
                yield return values.GetRange(i, Math.Min(chunkSize, values.Count - i));
            }
        }
    }
}
