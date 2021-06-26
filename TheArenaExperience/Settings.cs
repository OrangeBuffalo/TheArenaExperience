using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;

namespace TheArenaExperience
{
    class Settings : AttributeGlobalSettings<Settings>
    {
        public override string Id => "TheArenaExperience";
        public override string DisplayName => "The Arena Experience";
        public override string FolderName => "TheArenaExperience";
        public override string FormatType => "json2";

        [SettingPropertyFloatingInteger("XP Gains in Practice Fights", 0f, 1f, "0%", Order = 0, RequireRestart = false, HintText = "XP gains in practice fights (Vanilla=6%, Default=25%).")]
        [SettingPropertyGroup("The Arena Experience")]
        public float XpGainsPracticeFights { get; set; } = 0.25f;

        [SettingPropertyFloatingInteger("XP Gains in Tournaments", 0f, 1f, "0%", Order = 1, RequireRestart = false, HintText = "XP gains in tournaments (Vanilla=33%, Default=50%).")]
        [SettingPropertyGroup("The Arena Experience")]
        public float XpGainsTournaments { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Impressed Notable Chance", 0f, 1f, "0%", Order = 2, RequireRestart = false, HintText = "Chance to impress a local notable when winning a tournament (Default=50%)")]
        [SettingPropertyGroup("The Arena Experience")]
        public float ImpressedNotableChance { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Impressed Noble Chance", 0f, 1f, "0%", Order = 3, RequireRestart = false, HintText = "Chance to impress a local noble when winning a tournament (Default=50%)")]
        [SettingPropertyGroup("The Arena Experience")]
        public float ImpressedNobleChance { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Impressed Wanderer Chance", 0f, 1f, "0%", Order = 4, RequireRestart = false, HintText = "Chance to impress a local wanderer when winning a tournament (Default=50%)")]
        [SettingPropertyGroup("The Arena Experience")]
        public float ImpressedWandererChance { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Base Relationship Change", 0, 10, "0 Points", Order = 5, RequireRestart = false, HintText = "Base relationship change with impressed heroes (Default=1).")]
        [SettingPropertyGroup("The Arena Experience")]
        public int BaseRelationChange { get; set; } = 1;
    }
}
