using RimWorld;
using Verse;

namespace Bastyon
{
    [DefOf]
    public static class BastDefOf
    {
        // HediffDefs
        public static HediffDef Bast_SunLightDamage;

        // MiscDefs
        //public static ThingDef Bast_BlankThingWithBlankTexture;

        static BastDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BastDefOf));
        }
    }
}