using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.LoneDruid.ChaseCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    public class ControllableUnitSpellsOrbwalker : ControllableUnitOrbwalker
    {
        public ControllableUnitSpellsOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override bool CastSpells()
        {
            //Console.WriteLine(this.Unit.TargetSelector.TargetIsSet);
            if (!this.Unit.TargetSelector.TargetIsSet)
            {
                return false;
            }

            return this.Unit.UnitCombo.CastAllSpellsOnTarget();
        }
        public override bool BeforeAttack()
        {
            if (this.CastSpells())
            {
                return false;
            }

            return base.BeforeAttack();
        }

        public override bool AfterAttack()
        {
            if (this.CastSpells())
            {
                return true;
            }

            return base.AfterAttack();
        }

        public override bool Meanwhile()
        {
            if (this.CastSpells())
            {
                return true;
            }

            return base.Meanwhile();
        }
    }
}
