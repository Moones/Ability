// <copyright file="ControllableUnitSpellsOrbwalker.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Brewmaster.ChaseCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;

    public class ControllableUnitSpellsOrbwalker : UnitOrbwalkerBase
    {
        #region Constructors and Destructors

        public ControllableUnitSpellsOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override bool AfterAttack()
        {
            if (this.CastSpells())
            {
                return true;
            }

            return base.AfterAttack();
        }

        public override bool BeforeAttack()
        {
            if (this.CastSpells())
            {
                return false;
            }

            return base.BeforeAttack();
        }

        public override bool CastSpells()
        {
            // Console.WriteLine(this.Unit.TargetSelector.TargetIsSet);
            if (!this.Unit.TargetSelector.TargetIsSet)
            {
                return false;
            }

            return this.Unit.UnitCombo.CastAllSpellsOnTarget();
        }

        public override bool Meanwhile()
        {
            if (this.CastSpells())
            {
                return true;
            }

            return base.Meanwhile();
        }

        #endregion
    }
}