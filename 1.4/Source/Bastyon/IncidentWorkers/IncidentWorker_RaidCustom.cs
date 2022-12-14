using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Bastyon
{
	public class IncidentWorker_RaidCustom : IncidentWorker_RaidEnemy
	{
		//2. Second Trigger 
		protected override bool FactionCanBeGroupSource(Faction f, Map map, bool desperate = false)
		{
			if (base.FactionCanBeGroupSource(f, map, desperate) && f.HostileTo(Faction.OfPlayer))
			{
				if (!desperate)
				{
					return (float)GenDate.DaysPassed >= f.def.earliestRaidDays;
				}
				return true;
			}
			return false;
		}

		public bool CanGenerateFrom(PawnGroupMakerParms parms, PawnGroupMaker groupMaker)
		{
			if (!PawnGroupMakerUtility.ChoosePawnGenOptionsByPoints(parms.points, groupMaker.options, parms).Any())
			{
				return false;
			}
			return true;
		}

		protected void GeneratePawns(PawnGroupMakerParms parms, PawnGroupMaker groupMaker, List<Pawn> outPawns, bool errorOnZeroResults = true)
		{
			if (CanGenerateFrom(parms, groupMaker))
			{
				float pointsTotal = parms.points;
				bool allowFood = parms.raidStrategy == null || parms.raidStrategy.pawnsCanBringFood || (parms.faction != null && !parms.faction.HostileTo(Faction.OfPlayer));
				Predicate<Pawn> validatorPostGear = (parms.raidStrategy != null) ? ((Predicate<Pawn>)((Pawn p) => parms.raidStrategy.Worker.CanUsePawn(pointsTotal, p, outPawns))) : null;
				bool flag = false;
				foreach (PawnGenOptionWithXenotype item in PawnGroupMakerUtility.ChoosePawnGenOptionsByPoints(parms.points, groupMaker.options, parms))
				{
					Pawn pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(
						item.Option.kind,
						parms.faction, 
						PawnGenerationContext.NonPlayer, 
						parms.tile, 
						forceGenerateNewPawn: false, 
						allowDead: false, 
						allowDowned: false, 
						canGeneratePawnRelations: false,
						mustBeCapableOfViolence: true, 
						1f, 
						forceAddFreeWarmLayerIfNeeded: false, 
						allowGay: true, 
						allowPregnant: false, 
						allowFood, 
						allowAddictions: true, 
						parms.inhabitants, 
						certainlyBeenInCryptosleep: false, 
						forceRedressWorldPawnIfFormerColonist: false, 
						worldPawnFactionDoesntMatter: false, 
						0f, 
						0f, 
						null, 
						1f, 
						null, 
						validatorPostGear));
					
					if (parms.forceOneDowned && !flag)
					{
						pawn.health.forceDowned = true;
						pawn.mindState.canFleeIndividual = false;
						flag = true;
					}
					outPawns.Add(pawn);
				}
			}
		}

		public List<Pawn> SpawnThreats(IncidentParms parms, RaidOptionsModExt options)
		{
			List<Pawn> list = new List<Pawn>();
			int num = 0;
			int attempts = 0;
			while (num < options.minimumPawnCount && attempts < options.minimumPawnCount + 100)
			{
				if (options.pawnGroup.options.TryRandomElementByWeight(x => x.selectionWeight, out var result))
				{
					PawnGenerationRequest request = new PawnGenerationRequest(
						result.kind,
						parms.faction, 
						PawnGenerationContext.NonPlayer, 
						-1,
						forceGenerateNewPawn: false, 
						allowDead: false, 
						allowDowned: false, 
						canGeneratePawnRelations: false, 
						mustBeCapableOfViolence: true, 
						1f,
						forceAddFreeWarmLayerIfNeeded: false, 
						allowGay: true, 
						allowPregnant: false,
						allowFood: true,
						allowAddictions: true,
						inhabitant: false,
						certainlyBeenInCryptosleep: false,
						forceRedressWorldPawnIfFormerColonist: false,
						worldPawnFactionDoesntMatter: false,
						0f,
						0f,
						null,
						1f,
						null);

					request.BiocodeApparelChance = 1f;
					Pawn pawn = PawnGenerator.GeneratePawn(request);
					if (pawn != null)
					{
						list.Add(pawn);
						num++;
					}
				}
				attempts++;
			}
			if (list.Any())
			{
				return list;
			}
			return null;
		}

		public List<Pawn> SpawnThreats2(IncidentParms parms, RaidOptionsModExt options)
		{
			List<Pawn> list = new List<Pawn>();
			foreach (var pawnCount in options.minimumPawnCountPerKind.options)
			{
				int num = 0;
				int attempts = 0;
				int minimumPawnCount = (int)pawnCount.selectionWeight;
				while (num < minimumPawnCount && attempts < minimumPawnCount + 100)
				{
					PawnGenerationRequest request = new PawnGenerationRequest(
						pawnCount.kind, 
						parms.faction, 
						PawnGenerationContext.NonPlayer,
						-1,
						forceGenerateNewPawn: false, 
						allowDead: false, 
						allowDowned: false, 
						canGeneratePawnRelations: false, 
						mustBeCapableOfViolence: true, 
						1f,
						forceAddFreeWarmLayerIfNeeded: false, 
						allowGay: true, 
						allowPregnant: false,
						allowFood: true,
						allowAddictions: true,
						inhabitant: false,
						certainlyBeenInCryptosleep: false,
						forceRedressWorldPawnIfFormerColonist: false,
						worldPawnFactionDoesntMatter: false,
						0f,
						0f,
						null,
						1f,
						null);

					request.BiocodeApparelChance = 1f;
					Pawn pawn = PawnGenerator.GeneratePawn(request);
					if (pawn != null)
					{
						list.Add(pawn);
						num++;
					}
					attempts++;
				}
			}

			if (list.Any())
			{
				return list;
			}
			return null;
		}


		//1. An Incident is triggered - Parms contain the incident values.
		// Incident Parms details are not yet filled
		protected override bool CanFireNowSub(IncidentParms parms)
		{
			//Log.Message(" - CanFireNowSub - var value = base.CanFireNowSub(parms); - 1");
			var value = base.CanFireNowSub(parms);
			//Log.Message(" - CanFireNowSub - var options = this.def.GetModExtension<RaidOptions>(); - 2");
			
			// Gets all raid options from Customer Def

			var options = this.def.GetModExtension<RaidOptionsModExt>();
			//Log.Message(" - CanFireNowSub - if (options.allowedHourRange.HasValue && parms.target is Map map) - 3");

			// This checks the map time to see if it's night time for a Balhrin Raid (can only raid at night)

			if (options.allowedHourRange.HasValue && parms.target is Map map)
			{
				//Log.Message(" - CanFireNowSub - var hourRange = options.allowedHourRange.Value; - 4");
				var hourRange = options.allowedHourRange.Value;
				//Log.Message(" - CanFireNowSub - var curHour = GenLocalDate.HourOfDay(map.Tile); - 5");
				var curHour = GenLocalDate.HourOfDay(map.Tile);

				var minHour = hourRange.min;
				var maxHour = hourRange.max;

				//Log.Message(" - CanFireNowSub - if (hourRange.min <= hourRange.max && curHour >= hourRange.min && curHour <= hourRange.max) - 6");
				if (hourRange.min <= hourRange.max && curHour >= hourRange.min && curHour <= hourRange.max)
				{
					//Log.Message(" - CanFireNowSub - return value; - 7");
					return value;
				}
				else if (curHour >= hourRange.min || curHour <= hourRange.max)
				{
					//Log.Message(" - CanFireNowSub - return value; - 9");
					return value;
				}
				else
				{
					//Log.Message("curHour: " + curHour);
					//Log.Message("hourRange.min: " + hourRange.min);
					//Log.Message("hourRange.max: " + hourRange.max);
					return false;
				}
			}
			return value;
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			var options = this.def.GetModExtension<RaidOptionsModExt>();

			if (options.raidFaction != null && Find.FactionManager.FirstFactionOfDef(options.raidFaction) is null)
			{
				/*
				var faction = FactionGenerator.NewGeneratedFaction(options.raidFaction);
				faction.TrySetRelationKind(Faction.OfPlayer, FactionRelationKind.Hostile, false, null, null);
				Find.FactionManager.Add(faction);
				map.attackTargetsCache.Notify_FactionHostilityChanged(faction, Faction.OfPlayer);
				*/
				FactionGeneratorParms factionGeneratorParms = new FactionGeneratorParms
				{
					factionDef = options.raidFaction
                };
				var faction = FactionGenerator.NewGeneratedFaction(factionGeneratorParms);
				faction.SetRelationDirect(Faction.OfPlayer, FactionRelationKind.Hostile, false, null, null);
				Find.FactionManager.Add(faction);
				map.attackTargetsCache.Notify_FactionHostilityChanged(faction, Faction.OfPlayer);
			}

			ResolveRaidPoints(parms);
			// Tech Level condition
			if (options.minimumPlayerTechLevel.HasValue && Faction.OfPlayer.def.techLevel < options.minimumPlayerTechLevel.Value)
			{
				return false;
			}
			//Wealth Condition
			if (options.minimumPlayerWealth.HasValue && options.minimumPlayerWealth.Value > map.wealthWatcher.WealthTotal)
			{
				return false;
			}
			if (options.requiredResearchProjectsUnlocked != null && options.requiredResearchProjectsUnlocked.Any(x => !x.IsFinished))
			{
				return false;
			}
			if (options.raidFaction is null)
			{
				if (!TryResolveRaidFaction(parms))
				{
					return false;
				}
			}
			else
			{
				parms.faction = Find.FactionManager.FirstFactionOfDef(options.raidFaction);
			}
			if (!parms.forced && options.repeatAfterTicks.HasValue)
			{
				var curTick = Find.TickManager.TicksGame;
				var range = options.maxRepeatCount > 0 ? options.maxRepeatCount : 999;
				for (var i = 0; i < range; i++)
				{
					curTick += options.repeatAfterTicks.Value.RandomInRange;
					IncidentParms newParms = new IncidentParms
					{
						target = map,
						forced = true,
						points = parms.points
					};
					Find.Storyteller.incidentQueue.Add(this.def, curTick, newParms);
				}
			}
			else if (parms.forced && !CanFireNow(parms))
			{
				return false;
			}

			IncidentQueue incidentQueue = Find.Storyteller.incidentQueue;

			PawnGroupKindDef combat = PawnGroupKindDefOf.Combat;
			if (options.raidStrategy is null)
			{
				ResolveRaidStrategy(parms, combat);
			}
			else
			{
				parms.raidStrategy = options.raidStrategy;
			}
			//Log.Message("parms.raidStrategy: " + parms.raidStrategy);
			if (options.raidArrival is null)
			{
				ResolveRaidArriveMode(parms);
			}
			else
			{
				parms.raidArrivalMode = options.raidArrival;
			}

			parms.raidStrategy.Worker.TryGenerateThreats(parms);
			if (!parms.raidArrivalMode.Worker.TryResolveRaidSpawnCenter(parms))
			{
				return false;
			}
			float points = parms.points;
			parms.points = AdjustedRaidPoints(parms.points, parms.raidArrivalMode, parms.raidStrategy, parms.faction, combat);

			if (options.fixedRaidPoints != -1)
			{
				parms.points = options.fixedRaidPoints;
			}
			if (options.raidPointsMultiplier != -1)
			{
				parms.points *= options.raidPointsMultiplier;
			}

			var list = new List<Pawn>();
			if (options.minimumPawnCount != -1)
			{
				list = SpawnThreats(parms, options);
			}
			else if (options.minimumPawnCountPerKind != null)
			{
				list = SpawnThreats2(parms, options);
			}
			if (options.pawnGroup != null)
			{
				GeneratePawns(IncidentParmsUtility.GetDefaultPawnGroupMakerParms(combat, parms), options.pawnGroup, list);
			}

			if (list.Count == 0)
			{
				Log.Error("[Bastyon] Got no pawns spawning raid from parms " + parms);
				return false;
			}
			parms.raidArrivalMode.Worker.Arrive(list, parms);
			GenerateRaidLoot(parms, points, list);
			TaggedString letterLabel = GetLetterLabel(parms, options);
			TaggedString letterText = GetLetterText(parms, list, options);
			PawnRelationUtility.Notify_PawnsSeenByPlayer_Letter(list, ref letterLabel, ref letterText, GetRelatedPawnsInfoLetterText(parms), informEvenIfSeenBefore: true);
			List<TargetInfo> list2 = new List<TargetInfo>();
			if (parms.pawnGroups != null)
			{
				List<List<Pawn>> list3 = IncidentParmsUtility.SplitIntoGroups(list, parms.pawnGroups);
				List<Pawn> list4 = list3.MaxBy((List<Pawn> x) => x.Count);
				if (list4.Any())
				{
					list2.Add(list4[0]);
				}
				for (int i = 0; i < list3.Count; i++)
				{
					if (list3[i] != list4 && list3[i].Any())
					{
						list2.Add(list3[i][0]);
					}
				}
			}
			else if (list.Any())
			{
				foreach (Pawn item in list)
				{
					list2.Add(item);
				}
			}
			SendStandardLetter(letterLabel, letterText, GetLetterDef(options), parms, list2);
			if (!options.disableLordRaid.HasValue || !options.disableLordRaid.Value)
			{
				parms.raidStrategy.Worker.MakeLords(parms, list);
			}
			/*LessonAutoActivator.TeachOpportunity(ConceptDefOf.EquippingWeapons, OpportunityType.Critical);
			if (!PlayerKnowledgeDatabase.IsComplete(ConceptDefOf.ShieldBelts))
			{
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].apparel.WornApparel.Any((Apparel ap) => ap is ShieldBelt))
					{
						LessonAutoActivator.TeachOpportunity(ConceptDefOf.ShieldBelts, OpportunityType.Critical);
						break;
					}
				}
			}*/
			Find.TickManager.slower.SignalForceNormalSpeedShort();
			Find.StoryWatcher.statsRecord.numRaidsEnemy++;
			//Log.Message("2 parms.raidStrategy: " + parms.raidStrategy);
			return true;
		}

		protected override bool TryResolveRaidFaction(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			if (parms.faction != null)
			{
				return true;
			}
			float num = parms.points;
			if (num <= 0f)
			{
				num = 999999f;
			}
			if (PawnGroupMakerUtility.TryGetRandomFactionForCombatPawnGroup(num, out parms.faction, (Faction f) => FactionCanBeGroupSource(f, map), allowNonHostileToPlayer: true, allowHidden: true, allowDefeated: true))
			{
				return true;
			}
			if (PawnGroupMakerUtility.TryGetRandomFactionForCombatPawnGroup(num, out parms.faction, (Faction f) => FactionCanBeGroupSource(f, map, desperate: true), allowNonHostileToPlayer: true, allowHidden: true, allowDefeated: true))
			{
				return true;
			}
			return false;
		}

		protected override void ResolveRaidPoints(IncidentParms parms)
		{
			if (parms.points <= 0f)
			{
				Log.Error("[Bastyon] RaidEnemy is resolving raid points. They should always be set before initiating the incident.");
				parms.points = StorytellerUtility.DefaultThreatPointsNow(parms.target);
			}
		}

		public override void ResolveRaidStrategy(IncidentParms parms, PawnGroupKindDef groupKind)
		{
			if (parms.raidStrategy == null)
			{
				Map map = (Map)parms.target;
				DefDatabase<RaidStrategyDef>.AllDefs.Where((RaidStrategyDef d) => d.Worker.CanUseWith(parms, groupKind) && (parms.raidArrivalMode != null || (d.arriveModes != null && d.arriveModes.Any((PawnsArrivalModeDef x) => x.Worker.CanUseWith(parms))))).TryRandomElementByWeight((RaidStrategyDef d) => d.Worker.SelectionWeight(map, parms.points), out RaidStrategyDef result);
				parms.raidStrategy = result;
				if (parms.raidStrategy == null)
				{
					Log.Error(string.Concat("[Bastyon] No raid stategy found, defaulting to ImmediateAttack. Faction=", parms.faction.def.defName, ", points=", parms.points, ", groupKind=", groupKind, ", parms=", parms));
					parms.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
				}
			}
		}

		protected string GetLetterLabel(IncidentParms parms, RaidOptionsModExt options)
		{
			if (!options.letterTitle.NullOrEmpty())
			{
				return options.letterTitle;
			}
			return parms.raidStrategy.letterLabelEnemy + ": " + parms.faction.Name;
		}

		protected string GetLetterText(IncidentParms parms, List<Pawn> pawns, RaidOptionsModExt options)
		{
			if (!options.letterText.NullOrEmpty())
			{
				return options.letterText;
			}
			string str = string.Format(parms.raidArrivalMode.textEnemy, parms.faction.def.pawnsPlural, parms.faction.Name.ApplyTag(parms.faction)).CapitalizeFirst();
			str += "\n\n";
			str += parms.raidStrategy.arrivalTextEnemy;
			Pawn pawn = pawns.Find((Pawn x) => x.Faction.leader == x);
			if (pawn != null)
			{
				str += "\n\n";
				str += "EnemyRaidLeaderPresent".Translate(pawn.Faction.def.pawnsPlural, pawn.LabelShort, pawn.Named("LEADER"));
			}
			return str;
		}

		protected LetterDef GetLetterDef(RaidOptionsModExt options)
		{
			if (options.letterDef != null)
			{
				return options.letterDef;
			}
			return LetterDefOf.ThreatBig;
		}

		protected override string GetRelatedPawnsInfoLetterText(IncidentParms parms)
		{
			return "LetterRelatedPawnsRaidEnemy".Translate(Faction.OfPlayer.def.pawnsPlural, parms.faction.def.pawnsPlural);
		}
	}
}