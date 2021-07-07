﻿using System;
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
            DefDatabase<PawnKindDef>.AllDefsListForReading.RemoveAll(pawnKindDef => BastyonMod.modSettings.disabledBastyonAnimals.Contains(pawnKindDef.defName));
            return true;
        }
    }
}
