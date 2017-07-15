namespace LoneDruid.RetreatCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;

    using Ensage;

    public class LoneDruidRetreatOrbwalker : UnitOrbwalkerBase
    {
        public LoneDruidRetreatOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override bool PreciseIssue()
        {
            return false;
        }

        public override bool IssueMeanwhileActions()
        {
            this.Unit.SourceUnit.Move(Game.MousePosition);
            return true;
        }

        public override void Initialize()
        {
        }
    }
}
