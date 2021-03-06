﻿// <copyright file="IAttackAnimationTracker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker
{
    using Ability.Core.AbilityFactory.Utilities;

    public interface IAttackAnimationTracker : IAbilityUnitPart
    {
        #region Public Properties

        bool AttackReady { get; set; }

        Notifier AttackReadyNotifier { get; set; }

        float CancelAnimationTime { get; set; }

        float LastAttackStartTime { get; set; }

        float NextAttackTime { get; set; }

        #endregion

        #region Public Methods and Operators

        void AttackStarted();

        #endregion
    }
}