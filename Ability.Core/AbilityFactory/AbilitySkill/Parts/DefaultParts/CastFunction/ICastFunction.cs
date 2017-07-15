namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    public interface ICastFunction : IAbilitySkillPart
    {
        bool Cast(IAbilityUnit target);
        bool Cast();
        bool Cast(IAbilityUnit[] targets);
    }
}
