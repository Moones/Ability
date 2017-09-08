// <copyright file="IUnitTargetSelector.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TargetSelector
{
    using Ability.Core.AbilityFactory.Utilities;

    public interface IUnitTargetSelector : IAbilityUnitPart
    {
        #region Public Properties

        float LastDistanceToTarget { get; set; }

        float MaxTargetDistance { get; set; }

        IAbilityUnit Target { get; set; }

        Notifier TargetChanged { get; }

        Notifier TargetDistanceChanged { get; }

        bool TargetIsSet { get; set; }

        Notifier TargetStartAttacking { get; }

        Notifier TargetStartMoving { get; }

        Notifier FightingNotifier { get; }

        #endregion

        #region Public Methods and Operators

        IAbilityUnit GetTarget();

        IAbilityUnit[] GetTargets();

        void ResetTarget();

        #endregion
    }
}