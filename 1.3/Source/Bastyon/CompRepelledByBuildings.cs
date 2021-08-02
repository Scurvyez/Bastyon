using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Bastyon
{
    public class RepelBalhrin : DefModExtension
    {
        public bool repelledBy;

        public float hediffSeverity;

        public int tickInterval;

        public float fireSpawnChance;

        public FloatRange fireSize;
    }
    public class CompProperties_RepelledByBuildings : CompProperties
    {
        public CompProperties_RepelledByBuildings()
        {
            this.compClass = typeof(CompRepelledByBuildings);
        }
    }

    public class CompRepelledByBuildings : ThingComp
    {
        public Pawn pawn => this.parent as Pawn;
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            if (!pawn.health.hediffSet.HasHediff(BastDefOf.Bast_SunLightDamage))
            {
                var hediff = HediffMaker.MakeHediff(BastDefOf.Bast_SunLightDamage, pawn);
                pawn.health.AddHediff(hediff);
                hediff.Severity = 0;
            }
        }
    }


}
