﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace PlayTimeTracker
{
    /// <summary>
    /// RimWorld Session Play Time Tracker, abbreviated as RimWorld SPTT
    /// </summary>
    public struct RimWorldSPTT
    {
        public DateTime CommencementTime { get; private set; }
        public TimeSpan ElapsedTime => DateTime.Now - CommencementTime;

        /// <summary>
        /// Instantiates a SPPT with the given DateTime as the Commencement Time.
        /// <para/>
        /// The parameter is required since explicit parameterless constructors of struct is forbidden.
        /// </summary>
        /// <param name="commencement"></param>
        public RimWorldSPTT(DateTime commencement)
        {
            CommencementTime = commencement;
        }

        /// <summary>
        /// Generates a string (format is "hh:mm:ss:f") to represent the time elapsed since the Commencement Time.
        /// <para/>
        /// Note that since we are in Framework v3.5, formatting strings does not exist, so we have to construct the string ourselves.
        /// </summary>
        /// <returns>The string representing the time elapsed.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            // Hours: Displayed in full.
            // Supposedly people won't play more than 24 hours in one go.
            int hours = (int) ElapsedTime.TotalHours;
            builder.Append(hours.ToStringCached());
            builder.Append(":");
            // Minutes
            int minutes = ElapsedTime.Minutes;
            if (minutes == 0)
            {
                builder.Append("00");
            }
            else
            {
                if (minutes < 10)
                {
                    builder.Append("0");
                }
                builder.Append(minutes.ToStringCached());
            }
            builder.Append(":");
            // Seconds
            int seconds = ElapsedTime.Seconds;
            if (seconds == 0)
            {
                builder.Append("00");
            }
            else
            {
                if (seconds < 10)
                {
                    builder.Append("0");
                }
                builder.Append(seconds.ToStringCached());
            }
            builder.Append(":");
            // Milliseconds
            // Policy is to display 2 d.p. of milliseconds
            int millisecondsTenths = ElapsedTime.Milliseconds / 10;
            if (millisecondsTenths < 10)
            {
                builder.Append("0");
            }
            builder.Append(millisecondsTenths.ToStringCached());
            // Everything is done.
            return builder.ToString();
        }
    }
}
