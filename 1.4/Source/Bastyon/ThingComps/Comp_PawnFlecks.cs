using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace Bastyon
{
    public class Comp_PawnFlecks : ThingComp
    {
        public CompProperties_PawnFlecks Props => (CompProperties_PawnFlecks)props;
        private Color EmissionColor => Color.Lerp(Props.colorA, Props.colorB, Rand.Value);

        public override void CompTick()
        {
            base.CompTick();

            if (Props.staggeredTimings)
            {
                if (parent.IsHashIntervalTick(Props.staggeredTimingInt * Props.randomizedTimingRange.RandomInRange))
                {
                    Emit();
                }
            }
            else
            {
                Emit();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Emit()
        {
            Vector3 vel = new Vector3(0.00f, 0.00f, 1.00f); // velocity

            for (int i = 0; i < Props.burstCount; ++i)
            {
                FleckCreationData fCD = FleckMaker.GetDataStatic(parent.DrawPos + Props.offset, parent.Map, Props.fleck, Props.scale.RandomInRange);
                fCD.rotationRate = Rand.RangeInclusive(-240, 240);
                fCD.instanceColor = new Color?(EmissionColor);
                fCD.velocitySpeed = Rand.Range(0.1f, 0.8f);
                fCD.velocity = vel * 3.0f;
                parent.Map.flecks.CreateFleck(fCD);
            }
        }
    }
}
