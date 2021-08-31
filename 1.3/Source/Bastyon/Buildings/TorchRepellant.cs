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
    public class BuildingRepeller : Building
    {
		public bool connectedNode;
        public override void DrawExtraSelectionOverlays()
        {
			if (this.def.specialDisplayRadius > 0.1f)
			{
				GenDraw.DrawRadiusRing(this.Position, this.def.specialDisplayRadius);
			}
			if (!this.def.drawPlaceWorkersWhileSelected && this.def.PlaceWorkers != null)
			{
				for (int i = 0; i < this.def.PlaceWorkers.Count; i++)
				{
					if(this.def.PlaceWorkers[i].GetType().Name == "PlaceWorker_RepellerDesignator")
					this.def.PlaceWorkers[i].DrawGhost(this.def, this.Position, this.Rotation, Color.white, this);
				}
			}
		}
    }
}
