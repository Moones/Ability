// <copyright file="PickUpRune.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;

    using Ensage.Common.Extensions.SharpDX;

    public class PickUpRune : UnitOrderBase
    {
        #region Fields

        private bool executed;

        private IAbilityRune rune;

        private bool runeDisposed;

        private IAbilityUnit unit;

        #endregion

        #region Constructors and Destructors

        public PickUpRune(IAbilityUnit unit, IAbilityRune rune)
            : base(OrderType.TakeRune, unit, "PickUp rune")
        {
            this.unit = unit;
            this.rune = rune;
            rune.RuneDisposed.Subscribe(() => { this.runeDisposed = true; });
            this.ExecuteOnce = false;
            this.PrintInLog = true;
            this.ShouldExecuteFast = true;
        }

        #endregion

        #region Public Methods and Operators

        public override bool CanExecute()
        {
            if (this.runeDisposed || this.executed || !this.rune.SourceRune.IsValid)
            {
                return false;
            }

            return true;
        }

        public override float Execute()
        {
            if (this.unit.Position.PredictedByLatency.Distance(this.rune.SourceRune.Position) <= 350)
            {
                this.DoIt();
                return 0;
            }

            return 0;
        }

        public override float ExecuteFast()
        {
            return 0;
        }

        #endregion

        #region Methods

        private void DoIt()
        {
            this.unit.SourceUnit.PickUpRune(this.rune.SourceRune);
        }

        #endregion
    }
}