using HugsLib.Utils;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace PlayTimeTracker
{
    public class Alert_PlayTimeTracker : Alert
    {
        private readonly string descriptionString = "SPT stands for Session Play Time.\n\n" +
            "A session begins when you load a save, and ends when you Quit to OS or Quit to Main Menu.\n\n" +
            "This timer tells you how much time you have spent in this RimWorld session.\n\n" +
            "You may want to avoid playing long sessions of RimWorld.";
        /// <summary>
        /// Defines the time interval that the time-based color gradient is scaled on. Currently set to 5 hours.
        /// </summary>
        // private readonly int maxTimeForGradience = 5 * RimWorldTickingTime.TicksPerHour;

        private const float PulseFreq = 0.5f;
        private const float PulseAmpCritical = 0.6f;
        private readonly TimeSpan maxTimeForGradient = new TimeSpan(5, 0, 0);

        public Alert_PlayTimeTracker()
        {
            defaultPriority = AlertPriority.Critical;
        }

        public override string GetLabel()
        {
            return "SPT T+ " + PlayTimeTrackerMain.SPTTObjectInstance.ToString();
        }

        public override string GetExplanation()
        {
            return descriptionString;
            /*
            StringBuilder builder = new StringBuilder("Time is flowing.");
            // builder.AppendLine(PlayTimeTrackerMain.TimeKeeperObjectInstance.ToString());
            builder.AppendLine();
            builder.Append(descriptionString);
            return builder.ToString();
            */
        }

        public override AlertReport GetReport()
        {
            return true;
        }

        protected override Color BGColor
        {
            get
            {
                //int elapsedTicks = PlayTimeTrackerMain.TimeKeeperObjectInstance.TotalTime;
                //float progression = ((float)elapsedTicks) / maxTimeForGradience;
                return GradientColor;
                /*
                if (progression >= 0.7f)
                {
                    return PulserColor * GradientColor;
                }
                else
                {
                    return GradientColor;
                }
                */
            }
        }

        private Color GradientColor
        {
            get
            {
                TimeSpan elapsedTime = PlayTimeTrackerMain.SPTTObjectInstance.ElapsedTime;
                float progression = Mathf.Clamp((float)(elapsedTime.TotalMilliseconds / maxTimeForGradient.TotalMilliseconds), 0, 1);
                float localProgression;
                // Different progression results in different gradience
                if (progression < 0.4f)
                {
                    // Red -> Yellow
                    // defaultPriority = AlertPriority.Medium;
                    localProgression = progression / 0.4f;
                    return new Color(175 / 256f * localProgression, 175 / 256f, 0);
                }
                else if (progression < 0.7f)
                {
                    // Yellow -> Red
                    // defaultPriority = AlertPriority.High;
                    localProgression = (progression - 0.4f) / 0.3f;
                    return new Color(175 / 256f, 175 / 256f * (1 - localProgression), 0);
                }
                else
                {
                    // Red -> Purple
                    // New version: Red -> Black
                    // defaultPriority = AlertPriority.Critical;
                    localProgression = (progression - 0.7f) / 0.3f;
                    return new Color((175 / 256f) * (1 - localProgression), 0, 0);
                    // return new Color((175 - (175 - 120) * localProgression) / 256f, 0, 120 / 256f * localProgression);
                }
                /*
                progression *= Mathf.PI / 2;
                // return new Color(125 / 256f, 0, 125 / 256f);
                return new Color(125 / 256f * Mathf.Sin(progression), 150 / 256f * Mathf.Cos(progression), 125 / 256f * Mathf.Sin(progression));
                */
            }
        }

        private Color PulserColor
        {
            get
            {
                float num = Pulser.PulseBrightness(PulseFreq, Pulser.PulseBrightness(PulseFreq, PulseAmpCritical));
                return new Color(num, num, num);
            }
        }
    }
}
