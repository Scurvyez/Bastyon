using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace Bastyon
{
    public class CompProperties_PawnFlecks : CompProperties
    {
        public FleckDef fleck = null;
        public int burstCount = 1;
        public Vector3 offset = new Vector3(0, 0, 0);
        public Color colorA = Color.white;
        public Color colorB = Color.white;
        public FloatRange scale;

        public bool staggeredTimings = false;
        public int staggeredTimingInt = 0;

        public IntRange randomizedTimingRange;

        public CompProperties_PawnFlecks() => compClass = typeof(Comp_PawnFlecks);
    }
}
