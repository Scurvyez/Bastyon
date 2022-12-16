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
    public class Comp_PawnStateVisuals : ThingComp
    {
        public CompProperties_PawnStateVisuals Props => (CompProperties_PawnStateVisuals)props;
        public Pawn ParentPawn = new Pawn();
        public PawnKindDef pawnKind = new PawnKindDef();

        /// <summary>
        /// Checks to see if an animal is attacking, sleeping, eating, or moving over certain terrain types.
        /// If any of these conditions are met then visual effects are applied to the pawn.
        /// </summary>
        public override void CompTick()
        {
            base.CompTick();
            ParentPawn = (Pawn)parent;
            pawnKind = ParentPawn.kindDef;

            if (ParentPawn != null)
            {
                if (Props.effectsAttacking != null && ParentPawn.IsAttacking())
                {
                    for (int i = 0; i < Props.effectsAttacking.Count; i++)
                    {
                        //Log.Message(ParentPawn.Name + "<color=#4494E3FF> is attacking.</color>");
                        Props.effectsAttacking[i].Spawn(ParentPawn, parent.Map, i);
                    }
                }

                if (Props.effectsResting != null && !ParentPawn.Awake())
                {
                    for (int i = 0; i < Props.effectsResting.Count; i++)
                    {
                        Props.effectsResting[i].Spawn(ParentPawn, parent.Map, i);
                    }
                }
            }
        }
    }
}
