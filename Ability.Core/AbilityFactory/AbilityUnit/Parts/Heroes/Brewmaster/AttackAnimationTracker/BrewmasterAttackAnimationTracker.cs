﻿// <copyright file="BrewmasterAttackAnimationTracker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Brewmaster.AttackAnimationTracker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker;

    public class BrewmasterAttackAnimationTracker : AttackAnimationTracker
    {
        #region Constructors and Destructors

        public BrewmasterAttackAnimationTracker(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override void AttackStarted()
        {
            this.LastAttackStartTime = GlobalVariables.Time * 1000;

            this.CancelAnimationTime = this.Unit.AttackAnimation.GetAttackPoint() * 1000f + this.LastAttackStartTime
                                       + 70;
            this.NextAttackTime = this.Unit.AttackAnimation.GetAttackRate() * 1000f + this.LastAttackStartTime - 1;
        }

        #endregion
    }
}