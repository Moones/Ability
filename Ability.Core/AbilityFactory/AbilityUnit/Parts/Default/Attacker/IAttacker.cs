using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Attacker
{
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public interface IAttacker : IAbilityUnitPart
    {
        Notifier AttackOrderSent { get; }
        void Attack(IAbilityUnit target);

        void Attack(Unit unit);


    }
}
