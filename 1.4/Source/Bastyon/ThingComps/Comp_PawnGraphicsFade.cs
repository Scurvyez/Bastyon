using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace Bastyon
{
    public class Comp_PawnGraphicsFade : ThingComp
    {
        public CompProperties_PawnGraphicsFade Props => (CompProperties_PawnGraphicsFade)props;

        public int TickCounter = 0;
        public float Alpha = 1.0f;
        public Color FadeColor = new Color(0, 0, 0, 0);
        public FloatRange Range = new FloatRange(0.1f, 1.0f);

        private Material M_Materials;
        private Color M_Color;

        public override void CompTick()
        {
            base.CompTick();
            Comp_PawnGraphicsExtra Comp = parent.GetComp<Comp_PawnGraphicsExtra>();
            for (int i = 0; i < Comp.Props.graphicsExtra.Count; i++)
            {
                M_Materials = Comp.Props.graphicsExtra[i].Graphic.MatSingleFor(parent);
                Log.Message(M_Materials.ToString().Colorize(Color.green) + " materials present on " + parent.def.defName);
                M_Color = M_Materials.color;
            }

            TickCounter++;
            if (TickCounter < Props.tickInterval)
            {
                Alpha--;
                while (Alpha > 0.0f)
                {
                    for (int i = 0; i < Comp.Props.graphicsExtra.Count; i++)
                    {
                        M_Materials.color = new Color(M_Color.r, M_Color.g, M_Color.b, Alpha);
                    }
                }
                TickCounter = 0;
            }
        }
    }
}
