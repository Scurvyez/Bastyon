using UnityEngine;
using Verse;
using RimWorld;

namespace Bastyon
{
    public class SubEffecter_ConstantCircleEmote : SubEffecter
    {
        public SubEffecter_ConstantCircleEmote(SubEffecterDef def, Effecter parent) : base(def, parent)
        {

        }

        public override void SubEffectTick(TargetInfo A, TargetInfo B)
        {
            ticksUntilMote--;
            if (ticksUntilMote <= 0)
            {
                MakeMote(A);
                ticksUntilMote = def.ticksBetweenMotes;
            }
        }

        protected void MakeMote(TargetInfo A)
        {
            float num = 360f / def.maxMoteCount;
            for (int i = 0; i < def.maxMoteCount; i++)
            {
                Vector3 a = Quaternion.AngleAxis(num * i, Vector3.up) * Vector3.forward;
                SpawnMote(A.Thing, new Vector3?(A.Cell.ToVector3Shifted() + a * def.positionRadius));
            }
        }

        public virtual Mote SpawnMote(Thing target, Vector3? forcedPos = null)
        {
            Vector3? vector = ScaledPos(target, forcedPos);
            if (vector == null)
            {
                return null;
            }
            Mote mote = MoteMaker.MakeStaticMote(vector.Value, target.Map, def.moteDef, 1f, false);
            if (mote == null)
            {
                return null;
            }
            mote.exactRotation = def.angle.RandomInRange;
            mote.Scale = def.scale.RandomInRange;
            mote.rotationRate = def.rotationRate.RandomInRange;
            if (mote.def.mote.needsMaintenance)
            {
                mote.Maintain();
            }
            return mote;
        }

        public Vector3? ScaledPos(Thing target, Vector3? forcedPos = null)
        {
            Vector3 vector2 = (forcedPos ?? target.Position.ToVector3());
            return new Vector3?(vector2);
        }

        private int ticksUntilMote;
    }
}
