using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Bastyon
{
    public class RaidOptionsModExt : DefModExtension
    {
        public PawnGroupMaker pawnGroup;
        public PawnGroupMaker minimumPawnCountPerKind;
        public int minimumPawnCount = -1;
        public int fixedRaidPoints = -1;
        public float raidPointsMultiplier = -1f;
        public FactionDef raidFaction;
        public RaidStrategyDef raidStrategy;
        public PawnsArrivalModeDef raidArrival;
        public bool? disableLordRaid;
        public IntRange? repeatAfterTicks;
        public int maxRepeatCount;
        public IntRange? allowedHourRange;

        public LetterDef letterDef;
        public string letterTitle;
        public string letterText;
        public FloatRange raidPoints;
        public float? minimumPlayerWealth;
        public TechLevel? minimumPlayerTechLevel;
        public List<ResearchProjectDef> requiredResearchProjectsUnlocked;
    }
}