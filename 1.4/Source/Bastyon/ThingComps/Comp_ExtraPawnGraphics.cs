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
    public class Comp_ExtraPawnGraphics : ThingComp
    {
        public CompProperties_ExtraPawnGraphics Props => (CompProperties_ExtraPawnGraphics)props;
        public Pawn ParentPawn = new Pawn();
        public PawnKindDef pawnKind = new PawnKindDef();

        /// <summary>
        /// Checks to see if an animal is attacking, sleeping, eating, or moving over certain terrain types.
        /// If any of these conditions are met additional graphics are applied and the DefaultGraphic
        /// may be replaced. :)
        /// </summary>
        public override void PostDraw()
        {
            base.PostDraw();
            ParentPawn = (Pawn)parent;
            pawnKind = ParentPawn.kindDef;
            Pawn_JobTracker parentJob = new Pawn_JobTracker(ParentPawn);

            if (ParentPawn != null)
            {
                if (Props.graphicsAttacking != null && ParentPawn.IsAttacking())
                {
                    for (int i = 0; i < Props.graphicsAttacking.Count; i++)
                    {
                        Props.graphicsAttacking[i].Graphic.DrawWorker(
                            (
                            parent.DrawPos + Props.graphicsAttacking[i].drawOffset), 
                            parent.Rotation, 
                            parent.def, 
                            parent, 
                            0f
                            );
                    }
                }

                if (Props.graphicsResting != null && parentJob.posture.Laying())
                {
                    for (int i = 0; i < Props.graphicsResting.Count; i++)
                    {
                        Props.graphicsResting[i].Graphic.DrawWorker(
                            (
                            parent.DrawPos + Props.graphicsResting[i].drawOffset),
                            parent.Rotation,
                            parent.def,
                            parent,
                            0f
                            );
                    }
                }
            }
            else
            {
                parent.DefaultGraphic.Draw(parent.DrawPos, parent.Rotation, parent);
            }
        }
    }
}
