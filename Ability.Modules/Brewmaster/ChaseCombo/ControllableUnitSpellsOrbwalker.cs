namespace Ability.Brewmaster.ChaseCombo
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;
    using Ensage.Common.Extensions;

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
