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
            Vector3 drawPos = parent.DrawPos;
            Rot4 rotation = parent.Rotation;
            ParentPawn = parent as Pawn;
            pawnKind = ParentPawn.kindDef;
            Vector2 drawSize = pawnKind.lifeStages[ParentPawn.ageTracker.CurLifeStageIndex].bodyGraphicData.Graphic.drawSize;
            Pawn_JobTracker parentJob = new Pawn_JobTracker(ParentPawn);

            if (ParentPawn != null)
            {
                if (Props.graphicsExtra != null)
                {
                    for (int i = 0; i < Props.graphicsExtra.Count; i++)
                    {
                        Props.graphicsExtra[i].Graphic.drawSize = drawSize;

                        Props.graphicsExtra[i].Graphic.Draw(
                            (
                            drawPos + Props.graphicsExtra[i].drawOffset),
                            rotation,
                            parent
                            );
                    }
                }

                if (Props.graphicsAttacking != null && ParentPawn.IsAttacking())
                {
                    for (int i = 0; i < Props.graphicsAttacking.Count; i++)
                    {
                        Props.graphicsAttacking[i].Graphic.drawSize = drawSize;

                        Props.graphicsAttacking[i].Graphic.Draw(
                            (
                            drawPos + Props.graphicsAttacking[i].drawOffset),
                            rotation, 
                            parent
                            );
                    }
                }

                if (Props.graphicsResting != null && parentJob.posture.Laying())
                {
                    for (int i = 0; i < Props.graphicsResting.Count; i++)
                    {
                        Props.graphicsResting[i].Graphic.drawSize = drawSize;

                        Props.graphicsResting[i].Graphic.Draw(
                            (
                            drawPos + Props.graphicsResting[i].drawOffset),
                            rotation,
                            parent
                            );
                    }
                }
            }
        }
    }
}
