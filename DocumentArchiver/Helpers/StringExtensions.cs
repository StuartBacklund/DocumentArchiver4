﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DocumentArchiver.Helpers
{
    public static class StringExtensionMethods
    {
        public static bool IsNull(this object nullObject)
        {
            return nullObject == null;
        }

        public static bool IsNotNull(this object nullObject)
        {
            return nullObject != null;
        }

        public static string FormatCurrentCulture(this System.String format, params object[] values)
        {
            return string.Format(CultureInfo.CurrentCulture, format, values);
        }

        public static string CleanSpacesFromString(this string text)
        {
            return text.Replace(" ", "");
        }

        public static string FormatInvariantCulture(this System.String format, params object[] values)
        {
            return string.Format(CultureInfo.InvariantCulture, format, values);
        }

        private static StringBuilder DisplayMessageBuilder(this IEnumerable<string> results)
        {
            var sb = new StringBuilder();

            foreach (var result in results)
            {
                sb.AppendLine(result.ToString());
            }
            return sb;
        }

        public static string TryParseString(this string str, out string result)
        {
            result = string.Empty;
            if (str.IsNull())
            {
                return result;
            }
            return str.ToString();
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value.IsNotNull())
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsWhiteSpace(value[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void Clear(this StringBuilder builder)
        {
            if (builder.IsNotNull())
            {
                builder.Length = 0;
            }
        }

        public static string Capitalize(string input)
        {
            if (input.Length == 0) return string.Empty;
            if (input.Length == 1) return input.ToUpper();

            return input.Substring(0, 1).ToUpper() + input.Substring(1);
        }

        public static bool IsCapitalized(string input)
        {
            if (input.Length == 0) return false;
            return string.Compare(input.Substring(0, 1), input.Substring(0, 1).ToUpper(), false) == 0;
        }

        public static bool IsLowerCase(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (string.Compare(input.Substring(i, 1), input.Substring(i, 1).ToLower(), false) != 0)
                    return false;
            }
            return true;
        }

        public static bool IsUpperCase(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (string.Compare(input.Substring(i, 1), input.Substring(i, 1).ToUpper(), false) != 0)
                    return false;
            }
            return true;
        }

        public static int CountTotal(string input, string chars, bool ignoreCases)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (!(i + chars.Length > input.Length) &&
                    string.Compare(input.Substring(i, chars.Length), chars, ignoreCases) == 0)
                {
                    count++;
                }
            }
            return count;
        }

        public static string GetMonthNameAndYear()
        {
            return string.Format("{0}_{1}", DateTime.Now.ToString("MMMM"), DateTime.Today.Year);
        }
    }
}