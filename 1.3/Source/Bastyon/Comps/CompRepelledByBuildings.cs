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
            base.CompTick();
        }
    }
}
