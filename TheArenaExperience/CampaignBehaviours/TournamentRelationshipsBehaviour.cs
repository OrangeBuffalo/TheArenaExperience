using System;

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

        private void OnTournamentFinished(CharacterObject winner, Town town)
        {
            if (!winner.IsPlayerCharacter)
            {
                return;
            }

            // Daring local heroes gain 1 relationship points
            foreach (MobileParty party in town.Settlement.Parties)
            {
                if (!party.IsLeaderless && !party.IsMainParty)
                {
                    if (party.Leader.IsHero)
                    {
                        if (party.Leader.GetTraitLevel(DefaultTraits.Valor) >= 1)
                        {
                            ChangeRelationAction.ApplyPlayerRelation(party.LeaderHero, Settings.Instance.RelationshipGain, false, true);
                            InformationManager.DisplayMessage(new InformationMessage($"{party.Leader.Name} was impressed by your performance!"));
                        }
                    }
                }
            }
            foreach (Hero hero in town.Settlement.HeroesWithoutParty)
            {
                if (hero.GetTraitLevel(DefaultTraits.Valor) >= 1)
                {
                    ChangeRelationAction.ApplyPlayerRelation(hero, Settings.Instance.RelationshipGain, false, true);
                    InformationManager.DisplayMessage(new InformationMessage($"{hero.Name} was impressed by your performance!"));
                }
            }

            // Chance to impress a random local notable
            if (MBRandom.RandomFloatRanged(0f, 1f) < Settings.Instance.ImpressedNotableChance)
            {
                var notable = town.Settlement.Notables.GetRandomElement();
                ChangeRelationAction.ApplyPlayerRelation(notable, Settings.Instance.RelationshipGain, false, true);
                InformationManager.DisplayMessage(new InformationMessage($"{notable.Name} was impressed by your performance!"));
            }
        }
    }
}
