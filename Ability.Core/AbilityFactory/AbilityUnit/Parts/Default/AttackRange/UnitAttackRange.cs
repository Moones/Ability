// <copyright file="UnitAttackRange.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackRange
{
    using Ensage;
    using Ensage.Common.Extensions;

    public class UnitAttackRange : IUnitAttackRange
    {
        #region Fields

        private bool dragonLance;

        private bool hurricanePike;

        #endregion

        #region Constructors and Destructors

        public UnitAttackRange(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public IAbilityUnit Unit { get; set; }

        public float Value { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public virtual void Initialize()
        {
            this.Value = this.Unit.SourceUnit.AttackRange;

            // if (this.Unit.)
        }

        public bool IsInAttackRange(IAbilityUnit target)
        {
            return
                target.Position.Predict((float)(Game.Ping + this.Unit.TurnRate.GetTurnTime(target) * 1000f))
                    .Distance2D(this.Unit.Position.Current)
                <= this.Value + target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 50;
        }

        #endregion
    }
}