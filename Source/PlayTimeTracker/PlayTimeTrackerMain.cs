using HugsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace PlayTimeTracker
{
    public class PlayTimeTrackerMain : ModBase
    {
        public static string MODID
        {
            get
            {
                return "com.vectorial1024.rimworld.ptt";
            }
        }

        /// <summary>
        /// Already includes a space character.
        /// </summary>
        public static string MODPREFIX
        {
            get
            {
                return "[V1024-PTT] ";
            }
        }

        public override string ModIdentifier => MODID;

        public static RimWorldSPTT SPTTObjectInstance { get; private set; }

        public static void BeginOrResetCounting()
        {
            SPTTObjectInstance = new RimWorldSPTT(DateTime.Now);
        }
    }
}
