using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackDamage
{
    public class AttackDamage : IAttackDamage
    {
        public AttackDamage(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public float GetDamage(IAbilityUnit target)
        {
            return target.DamageManipulation.ManipulateIncomingAutoAttackDamage(
                this.Unit,
                this.Unit.SourceUnit.DamageAverage + this.Unit.SourceUnit.BonusDamage,
                0,
                0);
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public void Initialize()
        {
        }
    }
}
