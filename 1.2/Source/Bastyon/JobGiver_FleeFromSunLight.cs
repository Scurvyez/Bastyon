using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace Bastyon
{
    public class JobGiver_FleeFromSunLight : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (GridsUtility.Fogged(pawn.Position, pawn.Map))
            {
                return JobMaker.MakeJob(JobDefOf.LayDown);
            }
            if (!pawn.Map.IsNightNow() && RCellFinder.TryFindBestExitSpot(pawn, out IntVec3 spot))
            {
                Job job = JobMaker.MakeJob(JobDefOf.Goto, spot);
                job.exitMapOnArrival = true;
                job.locomotionUrgency = LocomotionUrgency.Jog;
                job.expiryInterval = new IntRange(60, 300).RandomInRange;
                job.canBash = true;
                return job;
            }
            return null;
        }
    }
}