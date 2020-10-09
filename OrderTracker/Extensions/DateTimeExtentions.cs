using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System.Runtime.CompilerServices;

namespace OrderTracker.Extensions
{
    public static class DateTimeExtentions
    {
        public static string GetWeek(this DateTime date)
        {
            var week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, System.DayOfWeek.Monday);
            return $"W{week:00}";
        }
    }
}
