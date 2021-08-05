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
        public static List<Building> GetAllBuildingsOfName(Map map, string buildingName)
        {
            return map.listerBuildings.AllBuildingsColonistOfDef(DefDatabase<ThingDef>.GetNamed(buildingName)).ToList();
        }

        public static IntVec2 Centroid(this List<IntVec2> path)
        {
            IntVec2 result = path.Aggregate(IntVec2.Zero, (current, point) => current + point);
            result /= path.Count;

            return result;
        }
    }
}
