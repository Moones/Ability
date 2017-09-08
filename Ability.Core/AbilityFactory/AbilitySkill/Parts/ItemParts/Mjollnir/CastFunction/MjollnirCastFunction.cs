using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Mjollnir.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    using Ability.Core.AbilityFactory.AbilityUnit;

    public class MjollnirCastFunction : DefaultCastingFunction
    {
        public MjollnirCastFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool CanCast()
        {
            return base.CanCast()
                   && this.Skill.Owner.TargetSelector.LastDistanceToTarget
                   < this.Skill.Owner.TargetSelector.Target.AttackRange.Value + 200;
        }

        public override bool TargetIsValid(IAbilityUnit target)
        {
            return target.SourceUnit.IsAlive && !this.Skill.Owner.Modifiers.AttackImmune && !target.Modifiers.Invul;
        }
    }
}
