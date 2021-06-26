using System;
using System.Collections.Generic;
using System.Linq;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;

namespace TheArenaExperience.CampaignBehaviours
{
    internal sealed class TournamentRelationships : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.TournamentFinished.AddNonSerializedListener(this, new Action<CharacterObject, Town>(OnTournamentFinished));
        }

        public override void SyncData(IDataStore dataStore)
        {
        }

        bool IsDaring(Hero hero)
        {
            return hero.GetTraitLevel(DefaultTraits.Valor) >= 1;
        }

        // 10% chance to get +3, 20% chance to get +2, 70% chance to get +1
        int RelationChange(Hero hero)
        {
            float roll = MBRandom.RandomFloatRanged(0f, 1f);

            if (hero.IsNotable && IsDaring(hero))
            {
                roll += 0.25f;
            }

            if (roll > 0.9f)
            {
                return Settings.Instance.BaseRelationChange + 2;
            }
            else if (roll > 0.7f)
            {
                return Settings.Instance.BaseRelationChange + 1;
            }
            else
            {
                return Settings.Instance.BaseRelationChange;
            }
        }

        private void OnTournamentFinished(CharacterObject winner, Town town)
        {
            if (!winner.IsPlayerCharacter)
            {
                return;
            }

            var heroesInTown = new List<Hero>();

            foreach (MobileParty party in town.Settlement.Parties)
            {
                if (!party.IsLeaderless && !party.IsMainParty)
                {
                    if (party.Leader.IsHero)
                    {
                        heroesInTown.Add(party.LeaderHero);
                    }
                }
            }

            foreach (Hero hero in town.Settlement.HeroesWithoutParty)
            {
                heroesInTown.Add(hero);
            }

            var notables = new List<Hero>();
            var nobles = new List<Hero>();
            var wanderers = new List<Hero>();

            foreach (Hero hero in heroesInTown)
            {
                if (hero.IsNotable)
                {
                    notables.Add(hero);
                }
                else if (hero.IsWanderer && hero.CompanionOf != Clan.PlayerClan)
                {
                    wanderers.Add(hero);
                }
                else if (hero.IsNoble && IsDaring(hero))
                {
                    nobles.Add(hero);
                }
            }

            Random random = new Random();

            // Notable impressed
            if (notables.Any() && MBRandom.RandomFloatRanged(0f, 1f) > Settings.Instance.ImpressedNotableChance)
            {
                int r = random.Next(notables.Count);
                Hero impressedHero = notables[r];
                ChangeRelationAction.ApplyPlayerRelation(impressedHero, RelationChange(impressedHero), false, true);
                InformationManager.DisplayMessage(new InformationMessage($"{impressedHero.Name} was impressed by your performance !"));
            }

            // Noble impressed
            if (nobles.Any() && MBRandom.RandomFloatRanged(0f, 1f) > Settings.Instance.ImpressedNobleChance)
            {
                int r = random.Next(nobles.Count);
                Hero impressedHero = nobles[r];
                ChangeRelationAction.ApplyPlayerRelation(impressedHero, RelationChange(impressedHero), false, true);
                InformationManager.DisplayMessage(new InformationMessage($"{impressedHero.Name} was impressed by your performance !"));
            }
            
            // Wanderer impressed
            if (wanderers.Any() && MBRandom.RandomFloatRanged(0f, 1f) > Settings.Instance.ImpressedWandererChance)
            {
                int r = random.Next(wanderers.Count);
                Hero impressedHero = wanderers[r];
                ChangeRelationAction.ApplyPlayerRelation(impressedHero, RelationChange(impressedHero), false, true);
                InformationManager.DisplayMessage(new InformationMessage($"{impressedHero.Name} was impressed by your performance !"));
            }
        }
    }
}
