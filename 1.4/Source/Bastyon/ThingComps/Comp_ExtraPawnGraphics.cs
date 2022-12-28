﻿using System;
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
        
        private bool OnlyShowDuringGameCondition()
        {
            if (Props.graphicsGameCondition != null && parent.MapHeld.gameConditionManager.ConditionIsActive(Props.gameCondition) && Props.gameCondition != null)
            {
                return true;
            }
            return false;
        }

        private bool IsMale()
        {
            if (parent is Pawn parentPawn)
            {
                if (parentPawn.gender == Gender.Male)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsFemale()
        {
            if (parent is Pawn parentPawn)
            {
                if (parentPawn.gender == Gender.Female)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsAsexual()
        {
            if (parent is Pawn parentPawn)
            {
                if (parentPawn.gender == Gender.None)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks to see if an animal is attacking, sleeping, eating, or moving over certain terrain types.
        /// If any of these conditions are met additional graphics are applied. :)
        /// 
        /// DO THE SLEEPING CHECKS AND HIDE THE GRAPHICS
        /// </summary>
        public override void PostDraw()
        {
            base.PostDraw();
            //Color color1 = new Color(0.145f, 0.588f, 0.745f, 1f); // for debugging text

            if (parent is Pawn parentPawn)
            {
                PawnKindDef pawnKind = parentPawn.kindDef;
                //Pawn_JobTracker parentJob = parentPawn.jobs;

                Rot4 rotation = parent.Rotation;
                Vector2 drawSize = pawnKind.lifeStages[parentPawn.ageTracker.CurLifeStageIndex].bodyGraphicData.Graphic.drawSize;
                Vector3 drawPos = parent.DrawPos;


                if (Props.graphicsExtra != null && (IsMale() || IsAsexual()))
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

                else if (Props.graphicsExtraFemale != null && IsFemale())
                {
                    for (int i = 0; i < Props.graphicsExtraFemale.Count; i++)
                    {
                        Props.graphicsExtraFemale[i].Graphic.drawSize = drawSize;

                        Props.graphicsExtraFemale[i].Graphic.Draw(
                            (
                            drawPos + Props.graphicsExtraFemale[i].drawOffset),
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

                if (OnlyShowDuringGameCondition() == true)
                {
                    if (!IsFemale())
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
                    else
                    {
                        for (int i = 0; i < Props.graphicsExtraFemale.Count; i++)
                        {
                            Props.graphicsExtraFemale[i].Graphic.drawSize = drawSize;

                            Props.graphicsExtraFemale[i].Graphic.Draw(
                                (
                                drawPos + Props.graphicsExtraFemale[i].drawOffset),
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
