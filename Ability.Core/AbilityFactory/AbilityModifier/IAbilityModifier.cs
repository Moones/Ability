// <copyright file="IAbilityModifier.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityModifier
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;

    /// <summary>The AbilityModifier interface.</summary>
    public interface IAbilityModifier : IDisposable
    {
        #region Public Properties

        /// <summary>Gets or sets the affected unit.</summary>
        IAbilityUnit AffectedUnit { get; set; }

        /// <summary>Gets or sets the modifier effect applier.</summary>
        IModifierEffectApplier ModifierEffectApplier { get; set; }

        /// <summary>Gets or sets the modifier handle.</summary>
        double ModifierHandle { get; set; }

        /// <summary>Gets or sets the name.</summary>
        string Name { get; set; }

        IReadOnlyDictionary<Type, IAbilityModifierPart> Parts { get; }

        /// <summary>Gets or sets the source modifier.</summary>
        Modifier SourceModifier { get; set; }

        /// <summary>Gets or sets the source skill.</summary>
        IAbilitySkill SourceSkill { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <param name="part">The part.</param>
        void AddPart<T>(T part) where T : IAbilityModifierPart;

        /// <summary>The assign modifier effect applier.</summary>
        /// <param name="modifierEffectApplier">The modifier effect applier.</param>
        void AssignModifierEffectApplier(IModifierEffectApplier modifierEffectApplier);

        /// <summary>The get part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        T GetPart<T>() where T : IAbilityModifierPart;

        /// <summary>
        ///     The on draw.
        /// </summary>
        void OnDraw();

        /// <summary>The remove part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        void RemovePart<T>() where T : IAbilityModifierPart;

        #endregion
    }
}