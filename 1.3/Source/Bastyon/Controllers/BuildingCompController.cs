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
    public class PlaceWorker_RepellerDesignator : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            CompProperties_RepelMonster compProperties = def.GetCompProperties<CompProperties_RepelMonster>();

            if (compProperties.BuildingConnectionRadius != 0f) range = compProperties.BuildingConnectionRadius;

            List<IntVec3> cells = new List<IntVec3>();
            int buildCountIndex = 0;
            for (int d = 0; d < 4; d++)
            {
                if (buildCountIndex != 1)
                {
                    for (int i = 0; i <= (Int32)range; i++)
                    {
                        var curCell = center + new IntVec3(0, 0, i).RotatedBy(new Rot4(d));
                        if (!curCell.InBounds(Find.CurrentMap)) break;
                        var node = curCell.GetFirstBuilding(Find.CurrentMap);
                        if(node != null)
                        {
                            if (node.def.defName == def.defName)
                            {
                                GenDraw.DrawLineBetween(GenThing.TrueCenter(center, Rot4.North, def.size, def.Altitude), node.TrueCenter(), SimpleColor.Green, 0.2f);
                                buildCountIndex++;
                            }
                        }
                    }
                    if (buildCountIndex == 1)
                    {
                        break;
                    }
                }
            }
        }
        public float range;
    }

    public class PlaceWorker_RepellerConnectionRadius : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            CompProperties_RepelMonster compProperties = def.GetCompProperties<CompProperties_RepelMonster>();

            if (compProperties.BuildingConnectionRadius != 0f) range = compProperties.BuildingConnectionRadius;

            GenDraw.DrawRadiusRing(center, this.range);
        }
        public float range;
    }
}
