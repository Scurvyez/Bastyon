using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Bastyon
{
    public static class Utils_RepellerBuilding
    {
        private static List<Building> getAllBuildingRepellers(ThingDef def)
        {
            List<Building> buildingsList = new List<Building>();
            buildingsList = Find.CurrentMap.listerBuildings.AllBuildingsColonistOfDef(def).ToList();
            return buildingsList;
        }
        private static ThingDef getRepellerDef()
        {
            return ThingDef.Named("Bast_Repeller");
        }

        
        public static ThingDef RepellerDef
        {
            get { return getRepellerDef(); }
        }
        
        public static List<Building> GetAllBuildingRepellers
        {
            get { return getAllBuildingRepellers(RepellerDef).ToList();  }
        }

        public static List<Vector3> GetAllBuildingPositions()
        {
            List<Vector3> positions = new List<Vector3>();
            foreach (Building building in GetAllBuildingRepellers)
            {
                positions.Add(building.Position.ToVector3());
            }
            return positions;
        }

        public static bool InRepellerArea(List<Vector3> polyPoints, Vector3 p)
        {
            Vector3[] polyArr = polyPoints.ToArray();
            var j = polyArr.Length -1;
            var inside = false;
            Vector2 pos = new Vector2(p.x, p.z);
            for (int i = 0; i < polyArr.Length; j = i++)
            {
                Vector2 pi = new Vector2(polyArr[i].x, polyArr[i].z);
                Vector2 pj = new Vector2(polyArr[j].x, polyArr[j].z);
                if (((pi.y <= pos.y && pos.y < pj.y) || (pj.y <= pos.y && pos.y < pi.y)) &&
                    (pos.x < (pj.x - pi.x) * (pos.y - pi.y) / (pj.y - pi.y) + pi.x))
                    inside = !inside;
            }
            return inside;
        }
    }
}
