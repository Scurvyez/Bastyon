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
    public class CompProperties_RepelMonster : CompProperties
    {
        public CompProperties_RepelMonster()
        {
            this.compClass = typeof(CompRepelMonster);
        }
        public float BuildingConnectionRadius;
    }
    public class CompRepelMonster : ThingComp
    {
		public CompProperties_RepelMonster Props => (CompProperties_RepelMonster)this.props;
        public float BuildingConnectionRadius => Props.BuildingConnectionRadius;
    }
}
