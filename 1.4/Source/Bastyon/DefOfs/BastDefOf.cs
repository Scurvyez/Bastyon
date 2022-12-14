using RimWorld;
using Verse;

namespace Bastyon
{
    [DefOf]
    public static class BastDefOf
    {
        public static HediffDef Bast_SunLightDamage;

        static BastDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BastDefOf));
        }
    }
}