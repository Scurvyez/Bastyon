using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using SettingsHelper;
using UnityEngine;

namespace Bastyon
{
    public class BastyonMod : Mod
    {
        public static BastyonModSettings modSettings;
        public BastyonMod(ModContentPack modContent) : base(modContent)
        {
            modSettings = GetSettings<BastyonModSettings>();
            HarmonyPatches.CallHarmonyPatches();
        }
        /* Settings window */
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            modSettings.DoSettingsWindowContents(inRect);
        }

        public override void WriteSettings()
        {
            modSettings.disabledBastyonAnimals = new List<string>();
            for (int i = 0; i < modSettings.allBastyonAnimals.Count; i++)
            {
                if (!modSettings.bastyonAnimalValues[i])
                {
                    modSettings.disabledBastyonAnimals.Add(modSettings.allBastyonAnimals[i].defName);
                }
            }
            base.WriteSettings();
        }

        public override string SettingsCategory()
        {
            return "Bastyon";
        }
    }
}
