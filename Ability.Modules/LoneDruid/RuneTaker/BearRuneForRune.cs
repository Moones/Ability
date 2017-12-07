// <copyright file="BearRuneForRune.cs" company="EnsageSharp">
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
namespace Ability.LoneDruid.RuneTaker
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;

    using SharpDX;

    public class BearRuneForRune : RunForRune<BountyRune>
    {
        #region Constructors and Destructors

        public BearRuneForRune(IAbilityUnit unit, RunePosition<BountyRune> rune, List<Vector3> path)
            : base(unit, rune, path)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override bool CanExecute()
        {
            if (this.Unit.Owner.Fighting && this.LastDistanceToRune / this.Unit.SourceUnit.MovementSpeed > 3.5)
            {
                Console.WriteLine("Canceled because fighting");
                return false;
            }

            return base.CanExecute();
        }

        #endregion
    }
}