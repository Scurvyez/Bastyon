using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace Bastyon
{
    public static class DefUtil
    {
        public static List<PawnKindDef> AllAnimalDefs(string filterDef)
        {
            return (from currentDef in DefDatabase<PawnKindDef>.AllDefs
                    where currentDef.defName.Contains(filterDef)
                    orderby currentDef.defName
                    select currentDef).ToList<PawnKindDef>();
        }

        public static List<IncidentDef> allIncidentDefs(string filterDef)
        {
            return (from currentDef in DefDatabase<IncidentDef>.AllDefs
                    where currentDef.defName.Contains(filterDef)
                    orderby currentDef.defName
                    select currentDef).ToList<IncidentDef>();
        }
    }
}
