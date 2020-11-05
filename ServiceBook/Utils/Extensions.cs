using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBook.Utils
{
    public static class Extensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string s)
        {
            return string.IsNullOrEmpty(s) || string.IsNullOrEmpty(s);
        }
    }
}
