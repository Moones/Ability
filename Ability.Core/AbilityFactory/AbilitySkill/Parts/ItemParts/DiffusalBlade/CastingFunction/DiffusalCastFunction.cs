using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.DiffusalBlade.CastingFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    public class DiffusalCastFunction : DefaultCastingFunction
    {
        public DiffusalCastFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool CanCast()
        {
            return base.CanCast() && this.Skill.Owner.TargetSelector.Target.Modifiers.HasBuffs;
        }
    }
}
