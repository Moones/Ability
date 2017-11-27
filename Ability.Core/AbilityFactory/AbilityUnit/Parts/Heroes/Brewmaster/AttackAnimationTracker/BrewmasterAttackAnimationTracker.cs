using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Brewmaster.AttackAnimationTracker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker;
    public class BrewmasterAttackAnimationTracker : AttackAnimationTracker
    {
        public BrewmasterAttackAnimationTracker(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override void AttackStarted()
        {
            this.LastAttackStartTime = GlobalVariables.Time * 1000;

            this.CancelAnimationTime = this.Unit.AttackAnimation.GetAttackPoint() * 1000f + this.LastAttackStartTime + 70;
            this.NextAttackTime = this.Unit.AttackAnimation.GetAttackRate() * 1000f + this.LastAttackStartTime - 1;
        }
    }
}
