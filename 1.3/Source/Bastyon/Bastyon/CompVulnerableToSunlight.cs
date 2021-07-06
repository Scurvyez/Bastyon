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
    public class Hediff_SunLightDamage : HediffWithComps
    {
        public override bool ShouldRemove => false;
        public override void Tick()
        {
            base.Tick();
            var options = this.def.GetModExtension<SunLightDamage>();
            if (pawn.Map != null && this.pawn.IsHashIntervalTick(options.tickInterval))
            {
                if (!this.pawn.Position.Roofed(this.pawn.Map) && !Utils.IsNightNow(pawn.Map))
                {
                    this.severityInt += options.hediffSeverity;
                    var chance = Rand.Chance(options.fireSpawnChance);
                    Log.Message(chance + " - " + options.fireSpawnChance);
                    if (chance)
                    {
                        FireUtility.TryAttachFire(this.pawn, options.fireSize.RandomInRange);
                    }
                }
                else if (this.severityInt > 0 && (this.pawn.Position.Roofed(this.pawn.Map) || Utils.IsNightNow(pawn.Map)))
                {
                    this.severityInt -= options.hediffSeverity;
                }
            }
            if (pawn.Map != null && this.pawn.IsHashIntervalTick(600))
            {
                if (this.CurStage.lifeThreatening && !this.pawn.Position.Roofed(this.pawn.Map) && !Utils.IsNightNow(pawn.Map))
                {
                    this.pawn.TakeDamage(new DamageInfo(DamageDefOf.Flame, 1000f));
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
        }
    }
    public class SunLightDamage : DefModExtension
    {
        public float hediffSeverity;

        public int tickInterval;

        public float fireSpawnChance;

        public FloatRange fireSize;
    }

    public class CompProperties_VulnerableToSunlight : CompProperties
    {
        public CompProperties_VulnerableToSunlight()
        {
            this.compClass = typeof(CompVulnerableToSunlight);
        }
    }
    public class CompVulnerableToSunlight : ThingComp
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