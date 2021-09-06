using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace Bastyon
{
    public class JobGiver_MonsterBerserk : ThinkNode_JobGiver
    {
        private static readonly IntRange ExpiryInterval_Melee = new IntRange(360, 480);

        private readonly float distanceToReact = 500f;
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (GridsUtility.Fogged(pawn.Position, pawn.Map))
            {
                return JobMaker.MakeJob(JobDefOf.LayDown);
            }
            if (pawn.TryGetAttackVerb(null, false) == null)
            {
                return null;
            }

            Pawn victim = pawn.FindPawnTarget(distanceToReact, (Thing p) => p is Pawn v && !v.Downed && !Utils_RepellerBuilding.InRepellerArea(Utils_RepellerBuilding.GetAllBuildingPositions(), v.Position.ToVector3()));
            if (victim == null)
            {
                if (pawn.GetRoom() != null && !pawn.GetRoom().PsychologicallyOutdoors)
                {
                    Predicate<Thing> validator = delegate (Thing t)
                    {
                        return t.def.defName.ToLower().Contains("door");
                    };
                    var door = GenClosest.ClosestThingReachable(pawn.Position,
                    pawn.Map, ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial), PathEndMode.Touch, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), 5f, validator);
                    if (door != null)
                    {
                        return pawn.MeleeAttackJob(door, ExpiryInterval_Melee.RandomInRange);
                    }
                }
            }
            else
            {
                using (PawnPath path = pawn.Map.pathFinder.FindPath(pawn.Position, victim.Position, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.PassAllDestroyableThings)))
                {
                    IntVec3 cellBefore;
                    Thing thing = path.FirstBlockingBuilding(out cellBefore, pawn);
                    if (thing != null)
                    {
                        Job job = DigUtility.PassBlockerJob(pawn, thing, cellBefore, canMineMineables: true, canMineNonMineables: true);
                        if (job != null)
                        {
                            return job;
                        }
                    }
                }
                return pawn.MeleeAttackJob(victim, ExpiryInterval_Melee.RandomInRange);
            }

            Building building = pawn.FindTurretTarget();
            if (building != null)
            {
                return pawn.MeleeAttackJob(building, ExpiryInterval_Melee.RandomInRange);
            }
            return null;
        }
    }
}