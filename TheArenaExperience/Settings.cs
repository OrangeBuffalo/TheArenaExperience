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

        [SettingPropertyFloatingInteger("XP Gains in Practice Fights", 0f, 1f, "0%", Order = 0, RequireRestart = false, HintText = "XP gains in practice fights (Vanilla=6%, Default=50%).")]
        [SettingPropertyGroup("The Arena Experience")]
        public float XpGainsPracticeFights { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("XP Gains in Tournaments", 0f, 1f, "0%", Order = 1, RequireRestart = false, HintText = "XP gains in tournaments (Vanilla=33%, Default=75%).")]
        [SettingPropertyGroup("The Arena Experience")]
        public float XpGainsTournaments { get; set; } = 0.75f;

        [SettingPropertyFloatingInteger("Impressed Notable Chance", 0f, 1f, "0%", Order = 2, RequireRestart = false, HintText = "Chance to impress a local notable when winning a tournament (Default=50%)")]
        [SettingPropertyGroup("The Arena Experience")]
        public float ImpressedNotableChance { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Relationship gain", 0, 10, "0 Points", Order = 3, RequireRestart = false, HintText = "Relationship gain with local heroes and notables (Default=1).")]
        [SettingPropertyGroup("The Arena Experience")]
        public int RelationshipGain { get; set; } = 1;
    }
}
