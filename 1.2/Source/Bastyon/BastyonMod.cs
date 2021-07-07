using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;

namespace Bastyon
{
    public class BastyonMod : Mod
    {
        public BastyonMod(ModContentPack modContent) : base(modContent)
        {
            modSettings = GetSettings<BastyonModSettings>();
            HarmonyPatches.CallHarmonyPatches();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard settingsWindowTop = new Listing_Standard();
            settingsWindowTop.Begin(inRect);
            settingsWindowTop.Label("Enabled bastyon test animals", -1, null);
            settingsWindowTop.End();
            if (bastyonAnimalValues == null)
            {
                bastyonAnimalValues = new bool[allBastyonAnimals.Count];
                for (int i = 0; i < allBastyonAnimals.Count; i++)
                {
                    bastyonAnimalValues[i] = !modSettings.disabledBastyonAnimals.Contains(allBastyonAnimals[i].defName);
                }
            }
            Listing_Standard settingsWindowBottom = new Listing_Standard();
            Rect bottomRect = new Rect(inRect.position + new Vector2(0f, 20f), inRect.size - new Vector2(0f, 20f));
            Rect viewRect = new Rect(0f, 0f, bottomRect.width - 20f, modSettings.disabledBastyonAnimals.Count * 8f);
            settingsWindowBottom.BeginScrollView(bottomRect, ref scrollPosition, ref viewRect);
            for (int i = 0; i < allBastyonAnimals.Count; i++)
            {
                Log.Message(allBastyonAnimals[i].defName);
                Rect checkboxRect = settingsWindowBottom.GetRect(Text.LineHeight);
                if (Mouse.IsOver(checkboxRect))
                {
                    Widgets.DrawHighlight(checkboxRect);
                }
                Widgets.CheckboxLabeled(checkboxRect, allBastyonAnimals[i].label.CapitalizeFirst(), ref bastyonAnimalValues[i], false, null, null, false);
            }
            settingsWindowBottom.EndScrollView(ref viewRect);
            base.DoSettingsWindowContents(inRect);
        }

        public override void WriteSettings()
        {
            modSettings.disabledBastyonAnimals = new List<string>();
            for (int i = 0; i < allBastyonAnimals.Count; i++)
            {
                if (!bastyonAnimalValues[i])
                {
                    modSettings.disabledBastyonAnimals.Add(allBastyonAnimals[i].defName);
                }
            }
            base.WriteSettings();
        }

        public override string SettingsCategory()
        {
            return "Bastyon";
        }

        public static BastyonModSettings modSettings;

        public static List<PawnKindDef> allBastyonAnimals = new List<PawnKindDef>();
            /*(from currentDef in DefDatabase<PawnKindDef>.AllDefs
             where currentDef.modContentPack.PackageId == "Scurvyez.Bastyon".ToLower()
             orderby currentDef.defName
             select currentDef).ToList<PawnKindDef>();*/

        public static bool[] bastyonAnimalValues;

        public static Vector2 scrollPosition = Vector2.zero;
    }
}
