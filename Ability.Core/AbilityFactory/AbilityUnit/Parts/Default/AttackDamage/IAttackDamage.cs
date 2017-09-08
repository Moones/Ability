using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackDamage
{
    public interface IAttackDamage : IAbilityUnitPart
    {
        float GetDamage(IAbilityUnit target);
    }
}
