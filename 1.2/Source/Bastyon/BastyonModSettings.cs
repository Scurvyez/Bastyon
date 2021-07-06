using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;


namespace Bastyon
{
    public class BastyonModSettings : ModSettings
    {
        public List<string> disabledBastyonAnimals = new List<string>();

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref disabledBastyonAnimals, "disabledBastyonAnimals", LookMode.Value, Array.Empty<object>());
        }
    }
}
