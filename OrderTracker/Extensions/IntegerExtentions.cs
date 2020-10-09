using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTracker.Extensions
{
    public static class IntegerExtentions
    {
        public static bool IsBetween(this int value, int bottom, int top , bool inclusive = false)
        {
            return inclusive
                ? value >= bottom && value <= top
                : value > bottom && value < top;
        }
    }
}
