using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using HarmonyLib;

namespace Bastyon
{
    public static class HarmonyPatches
    {
        public static void CallHarmonyPatches()
        {
            HarmonyPatches_DefGenerator();
        }

        public static void HarmonyPatches_DefGenerator()
        {
            var harmonyInstance = new Harmony("RimWorld.Scurvyez.Bastyon.HarmonyPatches_DefGenerator");

            var originalMethod = AccessTools.Method(typeof(DefGenerator), "GenerateImpliedDefs_PreResolve");
            var prefixMethod = AccessTools.Method(typeof(BastDefRemover), "Prefix_GenerateImpliedDefs_PreResolve");
            harmonyInstance.Patch(originalMethod, prefix: new HarmonyMethod(prefixMethod));
        }
    }
}
