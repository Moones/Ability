using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Orchid.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;

    using Ensage;

    public class OrchidCastFunction : DefaultCastingFunction
    {
        public OrchidCastFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool CanCast()
        {
            return base.CanCast() && !this.Skill.Owner.TargetSelector.Target.Modifiers.Silenced
                   && this.Skill.Owner.TargetSelector.Target.DisableManager.CanDisable(200 + Game.Ping)
                   && (this.Skill.Owner.SourceUnit.IsVisibleToEnemies
                           ? this.Skill.Owner.TargetSelector.LastDistanceToTarget < 650
                           : this.Skill.Owner.TargetSelector.LastDistanceToTarget < 300);
        }
    }
}
