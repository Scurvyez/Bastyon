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
    public class RepelBalhrin : DefModExtension
    {
        public bool repelledBy;

        public float hediffSeverity;

        public int tickInterval;

        public float fireSpawnChance;

        public FloatRange fireSize;
    }
    public class CompProperties_RepelledByBuildings : CompProperties
    {
        public CompProperties_RepelledByBuildings()
        {
            this.compClass = typeof(CompRepelledByBuildings);
        }
    }

    public class CompRepelledByBuildings : ThingComp
    {
        
    }


}
