using RimWorld;
using System;
using System.Linq;
using Verse;
using Verse.AI;

namespace Bastyon
{
    public static class Utils
    {
        public static Job MeleeAttackJob(this Pawn pawn, Thing enemyTarget, int expiryInterval)
        {
            Job job = JobMaker.MakeJob(JobDefOf.AttackMelee, enemyTarget);
            job.expiryInterval = expiryInterval;
            job.checkOverrideOnExpire = true;
            job.expireRequiresEnemiesNearby = true;
            job.canBashDoors = true;
            job.canBashFences = true;
            Log.Message(pawn + " is attacking " + enemyTarget + " - " + job);
            return job;
        }

        public static Building FindTurretTarget(this Pawn pawn)
        {
            return (Building)AttackTargetFinder.BestAttackTarget(pawn,
                TargetScanFlags.NeedLOSToPawns | TargetScanFlags.NeedLOSToNonPawns |
                TargetScanFlags.NeedReachable | TargetScanFlags.NeedThreat |
                TargetScanFlags.NeedAutoTargetable, (Thing t) => t is Building, 0f, 70f,
                default(IntVec3), float.MaxValue, false, true);
        }

        public static readonly SimpleCurve DistanceFactor = new SimpleCurve
        {
            new CurvePoint(5f, 2f),
            new CurvePoint(10f, 1f),
            new CurvePoint(20f, 0.5f),
            new CurvePoint(30f, 0.1f),
        };
        public static Pawn FindPawnTarget(this Pawn pawn, float distance, Predicate<Thing> customValidator = null)
        {
            if (customValidator == null)
            {
                customValidator = new Predicate<Thing>(x => true);
            }
            bool Predicate(Thing p) => p != null && p != pawn && p.def != pawn.def && p is Pawn prey && pawn.CanReserve(p) && customValidator(p);
            pawn.Map.mapPawns.AllPawnsSpawned.Where(x => Predicate(x)).TryRandomElementByWeight(x => DistanceFactor.Evaluate(x.Position.DistanceTo(pawn.Position)), out Pawn victim);
            return victim;
        }
        public static bool IsNightNow(this Map map)
        {
            return GenLocalDate.HourInteger(map) >= 18 || GenLocalDate.HourInteger(map) <= 5;
        }

    }
}