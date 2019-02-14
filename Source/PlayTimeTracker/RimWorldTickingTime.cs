using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace PlayTimeTracker
{
    public struct RimWorldTickingTime
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public int Ticks { get; private set; }
        public int TotalTime => Hours * TicksPerHour + Minutes * TicksPerMinute + (Seconds * TicksPerSecond) + Ticks;

        public const int TicksPerSecond = 60;
        public const int TicksPerMinute = TicksPerSecond * 60;
        public const int TicksPerHour = TicksPerMinute * 60;

        public RimWorldTickingTime(int rawTicks)
        {
            Hours = rawTicks / TicksPerHour;
            rawTicks %= TicksPerHour;
            Minutes = rawTicks / TicksPerMinute;
            rawTicks %= TicksPerMinute;
            Seconds = rawTicks / TicksPerSecond;
            Ticks = rawTicks %= TicksPerSecond;
        }

        public RimWorldTickingTime(int hours, int minutes, int seconds, int ticks)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Ticks = ticks;
        }

        public RimWorldTickingTime(RimWorldTickingTime other): this(other.Hours, other.Minutes, other.Seconds, other.Ticks)
        {

        }

        public override string ToString()
        {
            // Optimized for speed.
            // Just bear with the low readability.
            StringBuilder builder = new StringBuilder();
            if (Hours == 0)
            {
                builder.Append("00");
            }
            else
            {
                if (Hours < 10)
                {
                    builder.Append("0");
                }
                builder.Append(Hours.ToStringCached());
            }
            builder.Append(":");
            if (Minutes == 0)
            {
                builder.Append("00");
            }
            else
            {
                if (Minutes < 10)
                {
                    builder.Append("0");
                }
                builder.Append(Minutes.ToStringCached());
            }
            builder.Append(":");
            if (Seconds == 0)
            {
                builder.Append("00");
            }
            else
            {
                if (Seconds < 10)
                {
                    builder.Append("0");
                }
                builder.Append(Seconds.ToStringCached());
            }
            builder.Append(":");
            if (Ticks == 0)
            {
                builder.Append("00");
            }
            else
            {
                if (Ticks < 10)
                {
                    builder.Append("0");
                }
                builder.Append(Ticks.ToStringCached());
            }
            /*
            if ()
            {
                builder.Append("0");
            }
            if (Hours < )
            {

            }
            builder.Append(Convert.ToString(Hours).PadLeft(2, '0'));
            builder.Append(":");
            builder.Append(Convert.ToString(Minutes).PadLeft(2, '0'));
            builder.Append(":");
            builder.Append(Convert.ToString(Seconds).PadLeft(2, '0'));
            builder.Append(":");
            builder.Append(Convert.ToString(Ticks).PadLeft(2, '0'));
            */
            return builder.ToString();
        }

        public static RimWorldTickingTime operator +(RimWorldTickingTime instance, int ticksElapsed)
        {
            int rawTotalTime = instance.TotalTime + ticksElapsed;
            instance.Hours = rawTotalTime / TicksPerHour;
            rawTotalTime %= TicksPerHour;
            instance.Minutes = rawTotalTime / TicksPerMinute;
            rawTotalTime %= TicksPerMinute;
            instance.Seconds = rawTotalTime / TicksPerSecond;
            instance.Ticks = rawTotalTime %= TicksPerSecond;

            return instance;
        }

        public static RimWorldTickingTime operator +(RimWorldTickingTime left, RimWorldTickingTime right)
        {
            return left + right.TotalTime;
        }

        public static RimWorldTickingTime operator ++(RimWorldTickingTime instance)
        {
            return instance + 1;
        }
    }
}
