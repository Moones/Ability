using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.PhaseBoots.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    using Ability.Core.AbilityFactory.AbilityUnit;

    public class PhaseBootsCastFunction : DefaultCastingFunction
    {
        public PhaseBootsCastFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool CanCast()
        {
            return this.Skill.CanCast();
        }

        public override bool TargetIsValid(IAbilityUnit target)
        {
            return true;
        }
    }
}
