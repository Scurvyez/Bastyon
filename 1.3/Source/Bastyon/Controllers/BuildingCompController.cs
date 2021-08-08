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
    public static class BuildingCompController
    {

    }

    public class PlaceWorker_RepellerDesignator : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            // On selection of buiding in architect menue - show ring around the item
            range = def.specialDisplayRadius;
            GenDraw.DrawRadiusRing(center, this.range);



            //find nearest second building and draw a line between them.
            List<Thing> forCell = Find.CurrentMap.listerBuldingOfDefInProximity.GetForCell(center, this.range, def);
            if(forCell != null)
            {
                if (forCell.Count > 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        GenDraw.DrawLineBetween(GenThing.TrueCenter(center, Rot4.North, def.size, def.Altitude), forCell[i].TrueCenter(), SimpleColor.Green, 0.2f);
                    }
                }
                else if (forCell.Count == 1)
                {
                    GenDraw.DrawLineBetween(GenThing.TrueCenter(center, Rot4.North, def.size, def.Altitude), forCell[0].TrueCenter(), SimpleColor.Green, 0.2f);
                }
            } 
        }

        public float range;
    }

    public class LinkBuildingNodeController
    {
        
        

    }
}
