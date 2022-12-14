using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using UnityEngine;

namespace Bastyon
{
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
            ls.Label("Set Raid Chance");
            for (int i = keys.Count - 1; i >= 0; i--)
            {
                var incidentDef = DefDatabase<IncidentDef>.GetNamedSilentFail(keys[i]);
                if (incidentDef != null)
                {
                    var incidentChance = raidIncidentChances[keys[i]];
                    ls.SliderLabeled(String.Format("Incident Chance - {0}", incidentDef.label), ref incidentChance, incidentChance.ToStringDecimalIfSmall(), 0f, 100f, String.Format("Sets incident chance for {0}", incidentDef.label));
                    raidIncidentChances[keys[i]] = incidentChance;

                }
            }
            ls.End();
        }

    }
}
