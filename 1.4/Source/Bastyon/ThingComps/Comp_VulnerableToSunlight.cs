using Verse;

namespace Bastyon
{
    public class Comp_VulnerableToSunlight : ThingComp
    {
        public Pawn pawn => parent as Pawn;
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