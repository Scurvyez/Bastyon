using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using HarmonyLib;
using RimWorld;


namespace Bastyon
{
    public class CompProperties_CompRepelMonster : CompProperties
    {
        public CompProperties_CompRepelMonster()
        {
            this.compClass = typeof(CompRepelMonster);

        }
    }

    public class CompRepelMonster : ThingComp
    {
        public List<Building> buildings = new List<Building>();
        public List<IntVec2> positionValues = new List<IntVec2>();

        
        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            // Get Building in a list
            buildings = BuildingCompController.GetAllBuildingsOfName(base.parent.Map, this.parent.def.defName);
            if(buildings.Count > 1)
            {
                foreach(Building building in buildings)
                {
                    positionValues.Add(building.InteractionCell.ToIntVec2);
                }
            }
            
            
        }
    }
}
