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
    public class BastyonModSettings : ModSettings
    {
        public List<string> disabledBastyonAnimals = new List<string>();
        public List<PawnKindDef> allBastyonAnimals = new List<PawnKindDef>();
        public bool[] bastyonAnimalValues = new bool[DefUtil.AllAnimalDefs("Bast_").Count];

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref disabledBastyonAnimals, "disabledBastyonAnimals", LookMode.Value, Array.Empty<object>());
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Rect bottomRect = new Rect(inRect.position + new Vector2(0f, 20f), inRect.size - new Vector2(0f, 20f));
            Rect viewRect = new Rect(0f, 0f, bottomRect.width - 20f, disabledBastyonAnimals.Count * 8f);
            
            Listing_Standard settingsWindow = new Listing_Standard();
            settingsWindow.Begin(inRect);
            settingsWindow.Label("Enabled bastyon animals", -1, null);
            
            if (allBastyonAnimals == null)
            {
                allBastyonAnimals = DefUtil.AllAnimalDefs("Bast_");
            }
            if (bastyonAnimalValues == null)
            {
                bastyonAnimalValues = new bool[allBastyonAnimals.Count];
                for (int i = 0; i < allBastyonAnimals.Count; i++)
                {
                    bastyonAnimalValues[i] = !disabledBastyonAnimals.Contains(allBastyonAnimals[i].defName);
                }
            }
            settingsWindow.BeginScrollView(bottomRect, ref scrollPosition, ref viewRect);
            for (int i = 0; i < allBastyonAnimals.Count; i++)
            {
                Rect checkboxRect = settingsWindow.GetRect(Text.LineHeight);
                settingsWindow.Gap();
                if (Mouse.IsOver(checkboxRect))
                {
                    Widgets.DrawHighlight(checkboxRect);
                }
                Widgets.CheckboxLabeled(checkboxRect, allBastyonAnimals[i].label.CapitalizeFirst(), ref bastyonAnimalValues[i], false, null, null, false);
            }
            settingsWindow.EndScrollView(ref viewRect);
            settingsWindow.End();
        }
        private static Vector2 scrollPosition = Vector2.zero;
    }
}
