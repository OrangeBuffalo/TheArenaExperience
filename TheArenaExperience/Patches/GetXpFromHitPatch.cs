using System;
using System.Linq;

using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

using HarmonyLib;

namespace TheArenaExperience.Patches
{
    [HarmonyPatch(typeof(DefaultCombatXpModel), "GetXpFromHit")]
    internal class GetXpFromHitPatch
    {
        public static void Postfix(CharacterObject attackerTroop, CharacterObject captain, CharacterObject attackedTroop, PartyBase party, int damage, bool isFatal, CombatXpModel.MissionTypeEnum missionType, ref int xpAmount)
        {
            if (missionType == CombatXpModel.MissionTypeEnum.PracticeFight)
            {
                xpAmount = (int)Math.Round((float)xpAmount * (Settings.Instance.XpGainsPracticeFights / 0.0625f));
            }
            if (missionType == CombatXpModel.MissionTypeEnum.Tournament)
            {
                xpAmount = (int)Math.Round((float)xpAmount * (Settings.Instance.XpGainsTournaments / 0.33f));
            }
        }
    }
}