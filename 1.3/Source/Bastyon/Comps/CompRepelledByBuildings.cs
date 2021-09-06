using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using UnityEngine;

namespace Bastyon
{
    public class CompProperties_RepelledByBuildings : CompProperties
    {
        public CompProperties_RepelledByBuildings()
        {
            this.compClass = typeof(CompRepelledByBuildings);
        }
    }


    public class CompRepelledByBuildings : ThingComp
    {
        public List<Vector3> repellerBuildingsOnMap = new List<Vector3>();
        public ThingDef repellerBuildingDef = Utils_RepellerBuilding.RepellerDef;
        public Pawn pawn => this.parent as Pawn;

        public override void CompTick()
        {
            IntVec3 position = pawn.Position;
            int range = 40;
            bool inRepellerRange = Utils_RepellerBuilding.InRepellerArea(Utils_RepellerBuilding.GetAllBuildingPositions(), pawn.Position.ToVector3());
            List<Dictionary<string, object>> DirectionalPositionInfo = new List<Dictionary<string, object>>();

            if (pawn.CurJob != null)
            {
                if (inRepellerRange)
                {
                    for (int d = 0; d < 4; d++)
                    {
                        Dictionary<string, object> cellInfo = new Dictionary<string, object>();
                        List<IntVec3> cells = new List<IntVec3>();
                        for (int i = 0; i <= range; i++)
                        {
                            var curCell = position + new IntVec3(0, 0, i).RotatedBy(new Rot4(d));
                            if (!curCell.InBounds(Find.CurrentMap)) break;
                            cells.Add(curCell);
                            if (!Utils_RepellerBuilding.InRepellerArea(Utils_RepellerBuilding.GetAllBuildingPositions(), curCell.ToVector3()))
                            {
                                cellInfo.Add("DirectionIndex", d);
                                cellInfo.Add("CellCount", cells.Count);
                                cellInfo.Add("CellPosition", curCell + new IntVec3(0, 0, 10).RotatedBy(new Rot4(d)));
                                DirectionalPositionInfo.Add(cellInfo);
                                break;
                            }
                        }
                    }

                        var sortedList = DirectionalPositionInfo.OrderBy(dict => dict["CellCount"]).ToList();


                        Job job = JobMaker.MakeJob(JobDefOf.Goto, (IntVec3)sortedList[0]["CellPosition"]);
                        job.locomotionUrgency = LocomotionUrgency.Amble;
                        job.expiryInterval = -1;
                        if (pawn.CurJobDef != job.def)
                        {

                            pawn.jobs.StartJob(job);
                        }
                }
                base.CompTick();
            }
        }
    }
}
