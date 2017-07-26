// <copyright file="AbilityModifierComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;

    public class AbilityModifierComposer : IAbilityModifierComposer
    {
        #region Constructors and Destructors

        public AbilityModifierComposer()
        {
            this.AssignPart<IModifierEffectApplier>(
                modifier => new ModifierEffectApplier.ModifierEffectApplier(modifier));
        }

        #endregion

        #region Public Properties

        public IDictionary<Type, Action<IAbilityModifier>> Assignments { get; } =
            new Dictionary<Type, Action<IAbilityModifier>>();

        #endregion

        #region Public Methods and Operators

        public void AssignPart<T>(Func<IAbilityModifier, T> factory) where T : IAbilityModifierPart
        {
            // var type = typeof(T);
            // this.Assignments[type] = modifier => modifier.AddPart(factory);
        }

        public void Compose(IAbilityModifier modifier)
        {
            foreach (var keyValuePair in this.Assignments)
            {
                keyValuePair.Value.Invoke(modifier);
            }

            foreach (var keyValuePair in modifier.Parts)
            {
                keyValuePair.Value.Initialize();
            }
        }

        #endregion
    }
}