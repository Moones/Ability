// <copyright file="ControllableUnitOrbwalker.cs" company="EnsageSharp">
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
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits;

    using Ensage;

    public class ControllableUnitOrbwalker : UnitOrbwalkerBase
    {
        #region Fields

        private IControllableUnits controllableUnits;

        #endregion

        #region Constructors and Destructors

        public ControllableUnitOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
            this.IssueSleep = 150;
            this.controllableUnits = this.Unit.ControllableUnits;
        }

        #endregion

        #region Public Methods and Operators

        public override bool Move()
        {
            // Console.WriteLine(this.Unit.Owner == null);
            if (
                this.Unit.Owner.ControllableUnits.Units.Any(
                    x => x.Value.UnitHandle != this.Unit.UnitHandle && this.RunAround(x.Value, this.Target)))
            {
                return true;
            }

            return this.Bodyblocker.Bodyblock() || this.MoveToMouse();
        }

        public override void MoveBeforeAttack()
        {
            this.Move();
        }

        public override bool NoTarget()
        {
            if (
                this.Unit.Owner.ControllableUnits.Units.Any(
                    x => x.Value.UnitHandle != this.Unit.UnitHandle && this.RunAround(x.Value, Game.MousePosition)))
            {
                return true;
            }

            this.Unit.SourceUnit.Move(Game.MousePosition);
            return true;
        }

        #endregion
    }
}