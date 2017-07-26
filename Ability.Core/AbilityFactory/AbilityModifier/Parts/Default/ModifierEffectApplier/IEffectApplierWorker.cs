// <copyright file="IEffectApplierWorker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;

    public interface IEffectApplierWorker
    {
        #region Public Properties

        Func<Action<IAbilityUnit>> ApplyEffectActionGetter { get; set; }

        bool EffectWasApplied { get; }

        Func<Action<IAbilityUnit>> RemoveEffectActionGetter { get; set; }

        Func<Action<IAbilityUnit>> UpdateEffectActionGetter { get; set; }

        bool UpdateWithLevel { get; }

        #endregion

        #region Public Methods and Operators

        void ApplyEffect(IAbilityUnit affectedUnit);

        void RemoveEffect();

        void UpdateEffect();

        #endregion
    }
}