using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;
using UnityEngine;

namespace Bastyon
{
    public class CompProperties_ExtraPawnGraphics : CompProperties
    {
        public List<GraphicData> graphicsExtra = null;
        public List<GraphicData> graphicsAttacking = null;
        public List<GraphicData> graphicsResting = null;
        public List<GraphicData> graphicsEating = null;
        public List<GraphicData> graphicsOnTerrain = null;

        public CompProperties_ExtraPawnGraphics()
        {
            compClass = typeof(Comp_ExtraPawnGraphics);
        }
    }
}
