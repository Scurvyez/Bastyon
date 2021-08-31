using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace Bastyon
{
    public class JobGiver_FleeFromRepeller : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (Utils_RepellerBuilding.InRepellerArea(Utils_RepellerBuilding.GetAllBuildingPositions(), pawn.Position.ToVector3()))
            {
                IntVec3 cell = new IntVec3();
                int squareRadius = 4;
                do
                {
                    CellFinder.TryFindRandomCellNear(pawn.Position, Find.CurrentMap, squareRadius, null, out cell);
                    if (Utils_RepellerBuilding.InRepellerArea(Utils_RepellerBuilding.GetAllBuildingPositions(), cell.ToVector3()))
                    {
                        squareRadius = squareRadius + 4;
                    }
                }
                while (Utils_RepellerBuilding.InRepellerArea(Utils_RepellerBuilding.GetAllBuildingPositions(), cell.ToVector3()));

                Job job = JobMaker.MakeJob(JobDefOf.Goto, cell);
                job.locomotionUrgency = LocomotionUrgency.Jog;
                job.expiryInterval = new IntRange(60, 300).RandomInRange;
                job.canBashDoors = true;
                job.canBashFences = true;
                return job;
            }
            return null;
        }       
    }
}
