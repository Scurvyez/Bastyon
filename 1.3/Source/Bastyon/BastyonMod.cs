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
            //HarmonyPatches.CallHarmonyPatches();
        }

        public override string SettingsCategory()
        {
            return "Bastyon";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            allBastyonAnimals = (from currentDef in DefDatabase<PawnKindDef>.AllDefs
                                 where currentDef.defName.Contains("Bast_")
                                 orderby currentDef.defName
                                 select currentDef).ToList<PawnKindDef>();

            if (modSettings.bastyonAnimalToggle == null) modSettings.bastyonAnimalToggle = new Dictionary<string, bool>();
            for (int i = 0; i < allBastyonAnimals.Count; i++)
            {
                if (!modSettings.bastyonAnimalToggle.ContainsKey(allBastyonAnimals[i].defName))
                {
                    modSettings.bastyonAnimalToggle[allBastyonAnimals[i].defName] = false;
                }
            }

            modSettings.DoWdindowContents(inRect);
        }

        public static BastyonModSettings modSettings;
        public List<PawnKindDef> allBastyonAnimals = new List<PawnKindDef>();
    }

    public class BastyonEvents : Mod
    {
        public BastyonEvents(ModContentPack content) : base(content)
        {
            modSettings = GetSettings<BastyonRaidSettings>();
        }

        public override string SettingsCategory()
        {
            return "Bastyon Raid Settings";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            allBastyonIncidents = (from currentDef in DefDatabase<IncidentDef>.AllDefs
                                   where currentDef.defName.Contains("Bast_")
                                   orderby currentDef.defName
                                   select currentDef).ToList<IncidentDef>();

            if (modSettings.raidIncidentChances == null) modSettings.raidIncidentChances = new Dictionary<string, float>();
            for (int i = 0; i < allBastyonIncidents.Count; i++)
            {
                if (!modSettings.raidIncidentChances.ContainsKey(allBastyonIncidents[i].defName))
                {
                    modSettings.raidIncidentChances[allBastyonIncidents[i].defName] = allBastyonIncidents[i].baseChance;
                }
            }

            modSettings.DoWindowContents(inRect);
        }

        public static BastyonRaidSettings modSettings;
        public List<IncidentDef> allBastyonIncidents = new List<IncidentDef>();
    }


}
