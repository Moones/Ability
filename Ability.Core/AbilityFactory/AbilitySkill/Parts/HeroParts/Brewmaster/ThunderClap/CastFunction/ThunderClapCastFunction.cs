using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Brewmaster.ThunderClap.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;

    public class ThunderClapCastFunction : DefaultCastingFunction
    {
        public ThunderClapCastFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool CanCast()
        {
            return base.CanCast() && this.Skill.CastRange.IsTargetInRange
                   && (this.Skill.Owner.TargetSelector.Target.Health.Current
                       > this.Skill.Owner.AttackDamage.GetDamage(this.Skill.Owner.TargetSelector.Target) * 1.5
                       || this.Skill.Owner.TargetSelector.LastDistanceToTarget > 350);
        }

        public override bool TargetIsValid(IAbilityUnit target)
        {
            return base.TargetIsValid(target) && this.Skill.CastRange.TargetInRange(target);
        }
    }
}
