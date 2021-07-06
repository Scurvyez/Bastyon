using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Bastyon
{
    public static class BastDefRemover
    {
        public static bool Prefix_GenerateImpliedDefs_PreResolve()
        {
            BastyonMod.modSettings.allBastyonAnimals = DefUtil.AllAnimalDefs("Bast_");
            BastyonMod.modSettings.allBastyonIncidents = DefUtil.allIncidentDefs("Bast_");
            DefDatabase<PawnKindDef>.AllDefsListForReading.RemoveAll(pawnKindDef => BastyonMod.modSettings.disabledBastyonAnimals.Contains(pawnKindDef.defName));
            DefDatabase<PawnKindDef>.AllDefsListForReading.RemoveAll(incidentDef => BastyonMod.modSettings.disabledBastyonIncidents.Contains(incidentDef.defName));
            return true;
        }
    }
}
