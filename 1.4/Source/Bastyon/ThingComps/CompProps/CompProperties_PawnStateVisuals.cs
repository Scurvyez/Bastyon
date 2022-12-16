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
    public class CompProperties_PawnStateVisuals : CompProperties
    {
        public List<EffecterDef> effectsExtra = null;
        public List<EffecterDef> effectsAttacking = null;
        public List<EffecterDef> effectsResting = null;
        public List<EffecterDef> effectsEating = null;
        public List<EffecterDef> effectsWhileOnTerrain = null;

        public CompProperties_PawnStateVisuals()
        {
            compClass = typeof(Comp_PawnStateVisuals);
        }
    }
}
