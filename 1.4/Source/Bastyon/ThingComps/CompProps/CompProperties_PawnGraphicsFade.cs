using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;
using UnityEngine;

namespace Bastyon
{
    public class CompProperties_PawnGraphicsFade : CompProperties
    {
        public int? tickInterval = 360;

        public CompProperties_PawnGraphicsFade()
        {
            compClass = typeof(Comp_PawnGraphicsFade);
        }
    }
}
 