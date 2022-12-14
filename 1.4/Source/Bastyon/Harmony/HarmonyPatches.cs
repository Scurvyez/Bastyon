using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Verse;
using RimWorld;
using System.Reflection;
using System.Reflection.Emit;

namespace Bastyon
{
    [HarmonyPatch(typeof(WildAnimalSpawner))]
    [HarmonyPatch("SpawnRandomWildAnimalAt")]
    public static class BastyonAnimals_WildAnimalSpawner_SpawnRandomWildAnimalAt_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilg)
        {
            var codes = new List<CodeInstruction>(instructions);
            Label label = ilg.DefineLabel();
            int i = 0;
            foreach (CodeInstruction instruction in codes)
            {
                if (instruction.opcode == OpCodes.Stloc_0)
                {
                    codes[i + 1].labels.Add(label);
                    yield return new CodeInstruction(OpCodes.Stloc_0);
                    yield return new CodeInstruction(OpCodes.Ldloc_0);//Load "PawnKindDef" local variable. 
                    yield return new CodeInstruction(OpCodes.Call, typeof(BastyonAnimals_WildAnimalSpawner_SpawnRandomWildAnimalAt_Patch).GetMethod("DetectBastyonCreatureAndOptions"));
                    yield return new CodeInstruction(OpCodes.Brfalse, label);
                    yield return new CodeInstruction(OpCodes.Ret);
                }
                else
                {
                    yield return instruction;
                }

                i++;
            }

        }
        public static bool DetectBastyonCreatureAndOptions(PawnKindDef theCreature)
        {

            if (BastyonMod.modSettings.bastyonAnimalToggle != null && BastyonMod.modSettings.bastyonAnimalToggle.Keys.Contains(theCreature.defName))
            {
                if (BastyonMod.modSettings.bastyonAnimalToggle[theCreature.defName])
                {
                    return true;
                }
                else return false;
            }
            else return false;

        }

    }

    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.Bastyon");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(PawnGraphicSet), "ResolveAllGraphics")]
    class PawnGraphicSet_ResolveAllGraphics_Postfix
    {
        static void Postfix(PawnGraphicSet __instance)
        {
            if (__instance?.pawn?.RaceProps?.Animal != true) return;
            if (__instance?.pawn?.gender != Gender.Female) return;
            var modExt = __instance?.pawn?.kindDef?.GetModExtension<AlternateGraphicsModExt>();
            if (modExt == null) return;

            if (modExt.alternateGraphicsFemale.NullOrEmpty<AlternateGraphic>())
            {
                RestoreDefaultGraphic(__instance);
                return;
            }
            Rand.PushState(__instance.pawn.thingIDNumber ^ 46101);
            if (Rand.Value <= __instance.pawn.kindDef.alternateGraphicChance)
            {
                __instance.nakedGraphic = modExt.alternateGraphicsFemale.RandomElementByWeight((AlternateGraphic x) => x.Weight).GetGraphic(__instance.nakedGraphic);
            }
            else RestoreDefaultGraphic(__instance);
            Rand.PopState();
        }

        private static void RestoreDefaultGraphic(PawnGraphicSet __instance)
        {
            var curKindLifeStage = __instance?.pawn?.ageTracker?.CurKindLifeStage;
            if (curKindLifeStage == null) return;
            if (__instance.pawn.gender != Gender.Female || curKindLifeStage.femaleGraphicData == null) __instance.nakedGraphic = curKindLifeStage.bodyGraphicData.Graphic;
            else __instance.nakedGraphic = curKindLifeStage.femaleGraphicData.Graphic;
        }
    }
}
