using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWCTracker.Models
{
    public static class Utilities
    {


        public static string ToRandValue(this decimal? value)
        {
            return value.HasValue ? string.Format("R {0:###,##0.00}", value) : string.Empty;
        }
        public static string ToDecimalValue(this decimal value)
        {
            return string.Format("{0:###,##0.00}", value);
        }

        public static string ToDecimalValue(this decimal? value)
        {
            return value.HasValue ? string.Format("{0:###,##0.00}", value) : string.Empty;
        }

        public static string ToRandValue(this decimal value)
        {
            return string.Format("R {0:###,##0.00}", value);
        }

        public static string ToCustomShortDate(this DateTime? value)
        {
            return value.HasValue ? string.Format("{0:dd MMM yyyy}", value) : string.Empty;
        }
        public static string ToCustomShortDate(this DateTime value)
        {
            return string.Format("{0:dd MMM yyyy}", value);
        }

        public static string ToCustomLongDateTime(this DateTime? value)
        {
            return value.HasValue ? string.Format("{0:dd/MM/yyyy HH:mm}", value) : string.Empty;
        }
        public static string ToCustomLongDateTime(this DateTime value)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm}", value);
        }

        public static string ToCustomLongDate(this DateTime? value)
        {
            return value.HasValue ? string.Format("{0:dd MMMM yyyy}", value) : string.Empty;
        }
        public static string ToCustomLongDate(this DateTime value)
        {
            return string.Format("{0:dd MMMM yyyy}", value);
        }
        public static string ToDayMonth(this DateTime value)
        {
            var currentDate = new DateTime(value.Year, value.Month, 1);
            return string.Format("{0:dd MMMM}", currentDate);
        }


        public static string ToDayMonth(this DateTime? value)
        {
            if (value.HasValue)
            {
                DateTime currentDate = (DateTime)value;
                 currentDate = new DateTime(currentDate.Year, currentDate.Month, 1);
                return string.Format("{0:dd MMMM}", currentDate);
            }

            return string.Empty;
        }
        public static string ToYearMonth(this DateTime value)
        {
            var currentDate = new DateTime(value.Year, value.Month, 1);
            return string.Format("{0:yyyy-MMM}", currentDate);
        }
        public static string ToMonth(this DateTime value)
        {
            var currentDate = new DateTime(value.Year, value.Month, 1);
            return string.Format("{0:MMMM}", currentDate);
        }
        public static string ToMonthYear(this DateTime value)
        {
            var currentDate = new DateTime(value.Year, value.Month, 1);
            return string.Format("{0:MM/yyyy}", currentDate);
        }
        public static string ToMonthYear(this DateTime? value)
        {
            return value.HasValue ? string.Format("{0:MM/yyyy}", value) : string.Empty;
        }
        /// <summary>
        /// Check if Array is not empty
        /// </summary>
        ///<param name="dataArr">IEnumerable<int></int></param>
        ///<returns>bool</returns>
        public static bool ArrayIsNotEmpty(
            this IEnumerable<int> dataArr)
        {
            return dataArr.Count() == 1 && dataArr.First() != 0 ? true : false;
        }

        /// <summary>
        /// Convert a boolean value into a string description
        /// </summary>
        ///<param name="dataArr">IEnumerable int</param>
        ///<returns>bool</returns>
        public static string ToBoolString(
            this bool value)
        {
            return value ? "Yes" : "No";
        }

        /// <summary>
        /// Convert a boolean value into a string description
        /// </summary>
        ///<param name="dataArr">IEnumerable int</param>
        ///<returns>bool</returns>
        public static string ToBoolString(
            this bool? value)
        {

            return value.HasValue ? ((bool)value).ToBoolString() : string.Empty;
        }
        /// <summary>
        /// concatenates two dates into a string range
        /// </summary>
        ///<param name="startDate">DateTime</param>
        /// ///<param name="endDate">DateTime</param>
        ///<returns>string</returns>
        public static string ToShortDateRange(
            this DateTime startDate,
            DateTime endDate)
        {
            return startDate.ToCustomShortDate() + " - " + endDate.ToCustomShortDate();
        }

        /// <summary>
        /// concatenates two dates into a string range
        /// </summary>
        ///<param name="startDate">DateTime</param>
        /// ///<param name="endDate">DateTime</param>
        ///<returns>string</returns>
        public static string ToYearRange(
            this DateTime startDate,
            DateTime endDate)
        {
            return startDate.Year.ToString() + " - " + endDate.Year.ToString();
        }

        public static SaveResult ToSaveResult(
    this Dictionary<bool, string> data)
        {
            var saveResult = new SaveResult();

            foreach (var item in data)
            {
                if (item.Key)
                {
                    saveResult.IsSuccess = item.Key;
                }
                else
                {
                    saveResult.Message = item.Value;
                }

            }

            return saveResult;
        }

        public static SaveResult ToSaveResult(
         this Dictionary<int, string> data)
        {
            var saveResult = new SaveResult();

            foreach (var item in data)
            {
                if (item.Key > 0)
                {
                    saveResult.ID = item.Key;
                    saveResult.IsSuccess = true;
                }
                else
                {
                    saveResult.Message = item.Value;
                }

            }

            return saveResult;
        }

        public static List<Tuple<string, string>> ToReportParam(this
            string finYear,
            string param)
        {


            var reportParams = new List<Tuple<string, string>>();

            reportParams.Add(Tuple.Create(param.ToString(), finYear));

            return reportParams;

        }

        public static String Number2String(this int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
