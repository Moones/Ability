using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Attacker
{
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public class Attacker : IAttacker
    {
        public void Dispose()
        {
            
        }

        public IAbilityUnit Unit { get; set; }

        public void Initialize()
        {
        }

        public Notifier AttackOrderSent { get; } = new Notifier();

        public void Attack(IAbilityUnit target)
        {
            this.Attack(target.SourceUnit);
        }

        public void Attack(Unit unit)
        {
            this.AttackOrderSent.Notify();
            this.Unit.SourceUnit.Attack(unit);
        }
    }
}
