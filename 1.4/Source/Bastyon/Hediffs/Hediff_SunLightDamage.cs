using RimWorld;
using Verse;

namespace Bastyon
{
    public class Hediff_SunLightDamage : HediffWithComps
    {
        public override bool ShouldRemove => false;
        public override void Tick()
        {
            base.Tick();
            var options = def.GetModExtension<SunLightDamageModExt>();
            if (pawn.Map != null && pawn.IsHashIntervalTick(options.tickInterval))
            {
                if (!pawn.Position.Roofed(pawn.Map) && !Utils.IsNightNow(pawn.Map))
                {
                    severityInt += options.hediffSeverity;
                    var chance = Rand.Chance(options.fireSpawnChance);
                    Log.Message(chance + " - " + options.fireSpawnChance);
                    if (chance)
                    {
                        FireUtility.TryAttachFire(pawn, options.fireSize.RandomInRange);
                    }
                }
                else if (severityInt > 0 && (pawn.Position.Roofed(pawn.Map) || Utils.IsNightNow(pawn.Map)))
                {
                    severityInt -= options.hediffSeverity;
                }
            }
            if (pawn.Map != null && pawn.IsHashIntervalTick(600))
            {
                if (CurStage.lifeThreatening && !pawn.Position.Roofed(pawn.Map) && !Utils.IsNightNow(pawn.Map))
                {
                    pawn.TakeDamage(new DamageInfo(DamageDefOf.Flame, 1000f));
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
        }
    }
}
