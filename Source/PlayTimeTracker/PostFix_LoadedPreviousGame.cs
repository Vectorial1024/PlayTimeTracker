﻿using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace PlayTimeTracker
{
    [HarmonyPatch(typeof(GameComponentUtility))]
    [HarmonyPatch("LoadedGame", MethodType.Normal)]
    public class PostFix_LoadedPreviousGame
    {
        [HarmonyPostfix]
        public static void PostFix()
        {
            PlayTimeTrackerMain.BeginOrResetCounting();
        }
    }
}
