using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Brewmaster.Cyclone.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;

    public class CycloneCastFunction : DefaultCastingFunction
    {
        public CycloneCastFunction(IAbilitySkill skill)
            : base(skill)
        {
        }
        

        public override bool Cast(IAbilityUnit target)
        {
            if (!this.Skill.CanCast() || !target.SourceUnit.IsVisible
                || !this.Skill.CastRange.TargetInRange(target)
                || this.Skill.CastData.Queued || !this.TargetIsValid(target))
            {
                //Console.WriteLine("Cant cast on target cancast:");
                return false;
            }

            target.DisableManager.CastingDisable(
                this.Skill.HitDelay.Get());
            this.Skill.Owner.OrderQueue.EnqueueOrder(
                new CastSkill(
                    OrderType.DealDamageToEnemy,
                    this.Skill,
                    () => this.Skill.SourceAbility.UseAbility(target.SourceUnit), target));
            return true;
        }
    }
}
