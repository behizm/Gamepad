using System;

namespace Gamepad.Utility.Models
{
    public class DateSlice
    {
        public DateSlice(DateTime? start, DateTime? end)
        {
            Start = start ?? DateTime.MinValue;
            End = end ?? DateTime.MaxValue;
        }

        public DateSlice() : this(null, null)
        {
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
    }
}
