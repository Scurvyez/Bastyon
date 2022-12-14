using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using UnityEngine;

namespace Bastyon
{
    public class BastyonModSettings : ModSettings
    {
        private static Vector2 scrollPosition = Vector2.zero;
        public Dictionary<string, bool> bastyonAnimalToggle = new Dictionary<string, bool>();
        private List<string> animalKeys;
        private List<bool> animalValues;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref bastyonAnimalToggle, "bastyonAnimalToggle", LookMode.Value, LookMode.Value, ref animalKeys, ref animalValues);
        }

        public void DoWdindowContents(Rect inRect)
        {
            List<string> keyNames = bastyonAnimalToggle.Keys.ToList().OrderByDescending(x => x).ToList();
            Listing_Standard ls = new Listing_Standard();

            Rect rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            Rect rect2 = new Rect(0f, 0f, inRect.width - 30f, ((keyNames.Count / 2) * 50));

            Widgets.BeginScrollView(rect, ref scrollPosition, rect2, true);
            ls.Label("Disable Wild Animal Spawns");
            ls.ColumnWidth = rect2.width / 2.2f;
            ls.Begin(rect2);
            for (int i = keyNames.Count - 1; i >= 0; i--)
            {
                if (i == keyNames.Count / 2)
                {
                    ls.NewColumn();
                }
                bool state = bastyonAnimalToggle[keyNames[i]];
                ls.CheckboxLabeled(string.Format("Disable {0}", PawnKindDef.Named(keyNames[i]).LabelCap), ref state, String.Format("Disable {0}", PawnKindDef.Named(keyNames[i]).LabelCap));
                bastyonAnimalToggle[keyNames[i]] = state;
            }
            ls.End();
            Widgets.EndScrollView();
            base.Write();

        }
    }
}
