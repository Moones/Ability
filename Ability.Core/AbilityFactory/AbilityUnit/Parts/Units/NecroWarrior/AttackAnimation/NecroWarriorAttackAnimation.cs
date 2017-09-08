using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.NecroWarrior.AttackAnimation
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimation;
    class NecroWarriorAttackAnimation : AttackAnimation
    {
        public NecroWarriorAttackAnimation(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override float GetAttackPoint()
        {
            return 0.56f;
        }
    }
}
