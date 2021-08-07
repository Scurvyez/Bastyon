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
       
    }
}
