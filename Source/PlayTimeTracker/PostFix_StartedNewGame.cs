using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace PlayTimeTracker
{
    [HarmonyPatch(typeof(GameComponentUtility))]
    [HarmonyPatch("StartedNewGame", MethodType.Normal)]
    public class PostFix_StartedNewGame
    {
        [HarmonyPostfix]
        public static void PostFix()
        {
            PlayTimeTrackerMain.BeginOrResetCounting();
        }
    }
}
