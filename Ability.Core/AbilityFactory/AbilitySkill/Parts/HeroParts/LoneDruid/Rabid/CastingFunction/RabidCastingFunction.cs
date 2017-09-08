using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.Rabid.CastingFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    public class RabidCastingFunction : DefaultCastingFunction
    {
        public RabidCastingFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool CanCast()
        {
            return base.CanCast() && this.Skill.Owner.TargetSelector.LastDistanceToTarget < 1500;
        }
    }
}
