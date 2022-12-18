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
        public GameConditionDef Aurora = GameConditionDefOf.Aurora;

        /// <summary>
        /// Returns true if the parent pawn has any female body graphics listed.
        /// </summary>
        protected bool UseProperSexGraphics()
        {
            if (pawnKind.lifeStages[ParentPawn.ageTracker.CurLifeStageIndex].femaleGraphicData != null)
            {
                return true;
            }

            return false;
        }
        
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
            Vector3 drawOffset = pawnKind.lifeStages[ParentPawn.ageTracker.CurLifeStageIndex].bodyGraphicData.Graphic.data.drawOffset;
            Pawn_JobTracker parentJob = new Pawn_JobTracker(ParentPawn);

            Color color1 = new Color(0.145f, 0.588f, 0.745f, 1f); // for debugging text

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

                if (Props.graphicsGameCondition != null && parent.MapHeld.gameConditionManager.ConditionIsActive(Props.gameCondition))
                {
                    if (Props.graphicsGameConditionFemale != null && ParentPawn.gender == Gender.Female)
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
