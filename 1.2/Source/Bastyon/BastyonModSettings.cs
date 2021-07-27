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
        private static Vector2 scrollPosition = Vector2.zero;
        public Dictionary<string, bool> bastyonAnimalToggle = new Dictionary<string, bool>();

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref bastyonAnimalToggle, "bastyonAnimalToggle", LookMode.Value, LookMode.Value, ref animalKeys, ref animalValues);
        }

        private List<string> animalKeys;
        private List<bool> animalValues;

        public void DoWdindowContents(Rect inRect)
        {
            List<string> keyNames = bastyonAnimalToggle.Keys.ToList().OrderByDescending(x => x).ToList();
            Listing_Standard ls = new Listing_Standard();

            Rect rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            Rect rect2 = new Rect(0f, 0f, inRect.width - 30f, ((keyNames.Count / 2) * 50));

            Widgets.BeginScrollView(rect, ref scrollPosition, rect2, true);
            ls.ColumnWidth = rect2.width / 2.2f;
            ls.Begin(rect2);
            for (int i = keyNames.Count - 1; i >= 0; i--)
            {
                if (i == keyNames.Count / 2)
                {
                    ls.NewColumn();
                }
                bool state = bastyonAnimalToggle[keyNames[i]];
                ls.CheckboxLabeled(PawnKindDef.Named(keyNames[i]).LabelCap, ref state);
                bastyonAnimalToggle[keyNames[i]] = state;
            }
            ls.End();
            Widgets.EndScrollView();
            base.Write();

        }
    }

    public class BastyonRaidSettings : ModSettings
    {
        public bool disableBahlrinRaid = false;
        public Dictionary<string, float> raidIncidentChances = new Dictionary<string, float>();
        public List<string> incidentKeys;
        public List<float> incidentChancesValues;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref disableBahlrinRaid, "disableBahlrinRaid", false, true);
            Scribe_Collections.Look(ref raidIncidentChances, "raidIncidentChances", LookMode.Value, LookMode.Value, ref incidentKeys, ref incidentChancesValues);
        }

        public void DoWindowContents(Rect inRect)
        {
            var keys = raidIncidentChances.Keys.ToList().OrderByDescending(x => x).ToList();
            Listing_Standard ls = new Listing_Standard();

            ls.Begin(inRect);
            ls.Gap(10f);

            ls.CheckboxLabeled("Disable Balhrin Raids", ref disableBahlrinRaid, "Prevents Balhrin raids from spawning");
            ls.Gap(10f);
            ls.GapLine();
            ls.Gap(10f);
            for (int i = keys.Count -1; i >= 0; i--)
            {
                var incidentDef = DefDatabase<IncidentDef>.GetNamedSilentFail(keys[i]);
                if(incidentDef != null)
                {
                    var incidentChance = raidIncidentChances[keys[i]];
                    ls.SliderLabeled(String.Format("Incident Chance - {0}", incidentDef.label), ref incidentChance, incidentChance.ToStringDecimalIfSmall(), 0f, 5f, String.Format("Sets incident chance for {0}", incidentDef.label));
                    raidIncidentChances[keys[i]] = incidentChance;
                    
                }
            }
            ls.End();
        }

    }

}