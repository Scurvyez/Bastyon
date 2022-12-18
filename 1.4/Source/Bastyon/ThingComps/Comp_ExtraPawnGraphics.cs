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
        public GameConditionDef Aurora = GameConditionDefOf.Aurora;
        
        /// <summary>
        /// Checks to see if an animal is attacking, sleeping, eating, or moving over certain terrain types.
        /// If any of these conditions are met additional graphics are applied. :)
        /// </summary>
        public override void PostDraw()
        {
            base.PostDraw();
            Pawn parentPawn = parent as Pawn;

            Color color1 = new Color(0.145f, 0.588f, 0.745f, 1f); // for debugging text

            if (parentPawn != null)
            {
                PawnKindDef pawnKind = parentPawn.kindDef;
                Pawn_JobTracker parentJob = parentPawn.jobs;

                Rot4 rotation = parent.Rotation;
                Vector2 drawSize = pawnKind.lifeStages[parentPawn.ageTracker.CurLifeStageIndex].bodyGraphicData.Graphic.drawSize;
                Vector3 drawPos = parent.DrawPos;

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

                if (Props.graphicsAttacking != null && parentPawn.IsAttacking())
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

                if (Props.graphicsGameCondition != null && parent.MapHeld.gameConditionManager.ConditionIsActive(Props.gameCondition))
                {
                    if (Props.graphicsGameConditionFemale != null && parentPawn.gender == Gender.Female)
                    {
                        for (int i = 0; i < Props.graphicsGameConditionFemale.Count; i++)
                        {
                            Props.graphicsGameConditionFemale[i].Graphic.drawSize = drawSize;

                            Props.graphicsGameConditionFemale[i].Graphic.Draw(
                                (
                                drawPos + Props.graphicsGameConditionFemale[i].drawOffset),
                                rotation,
                                parent
                                );
                        }
                    }

                    else
                    {
                        for (int i = 0; i < Props.graphicsGameCondition.Count; i++)
                        {
                            Props.graphicsGameCondition[i].Graphic.drawSize = drawSize;

                            Props.graphicsGameCondition[i].Graphic.Draw(
                                (
                                drawPos + Props.graphicsGameCondition[i].drawOffset),
                                rotation,
                                parent
                                );
                        }
                    }
                }
            }
        }
    }
}
