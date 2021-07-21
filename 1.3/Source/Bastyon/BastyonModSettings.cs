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
        public List<string> disabledBastyonAnimals;
        //public List<string> disabledBastyonIncidents;
        public List<PawnKindDef> allBastyonAnimals = new List<PawnKindDef>();
        //public List<IncidentDef> allBastyonIncidents = new List<IncidentDef>();
        //public static bool[] bastyoneIncidentValues;
        public bool[] bastyonAnimalValues;
        public override void ExposeData()
        {
            Scribe_Collections.Look(ref disabledBastyonAnimals, "disabledBastyonAnimals", LookMode.Value, Array.Empty<object>());
           //Scribe_Collections.Look(ref disabledBastyonIncidents, "disabledBastyonIncidents");
           
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            if(bastyonAnimalValues == null)
            {
                bastyonAnimalValues = new bool[allBastyonAnimals.Count];
                for (int i = 0; i < allBastyonAnimals.Count; i++)
                {
                    bastyonAnimalValues[i] = !disabledBastyonAnimals.Contains(allBastyonAnimals[i].defName);
                }
            }
            Listing_Standard settingsWindowBottom = new Listing_Standard();
            Rect bottomRect = new Rect(inRect.position + new Vector2(0f, 20f), inRect.size - new Vector2(0f, 20f));
            Rect viewRect = new Rect(0f, 0f, bottomRect.width - 20f, disabledBastyonAnimals.Count * 8f);
            /*settingsWindowBottom.BeginScrollView(bottomRect, ref scrollPosition, ref viewRect);
            
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
            settingsWindowBottom.GapLine();
            settingsWindowBottom.EndScrollView(ref viewRect);*/
            Widgets.BeginScrollView(bottomRect, ref scrollPosition, viewRect, true);
            bottomRect.height = 100000f;
            bottomRect.width -= 20f;
            settingsWindowBottom.Begin(bottomRect.AtZero());
            for (int i = 0; i < allBastyonAnimals.Count; i++)
            {
                Rect checkboxRect = settingsWindowBottom.GetRect(Text.LineHeight);
                if (Mouse.IsOver(checkboxRect))
                {
                    Widgets.DrawHighlight(checkboxRect);
                }
                Widgets.CheckboxLabeled(checkboxRect, allBastyonAnimals[i].label.CapitalizeFirst(), ref bastyonAnimalValues[i], false, null, null, false);
            }
            Widgets.EndScrollView();
            settingsWindowBottom.End();
        }
        private static Vector2 scrollPosition = Vector2.zero;
    }
}
