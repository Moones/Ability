// <copyright file="AbilityModifier.cs" company="EnsageSharp">
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

    /// <summary>The ability modifier.</summary>
    public class AbilityModifier : IAbilityModifier
    {
        #region Fields

        private readonly Dictionary<Type, IAbilityModifierPart> parts = new Dictionary<Type, IAbilityModifierPart>();

        #endregion

        #region Constructors and Destructors

        public AbilityModifier(Modifier modifier)
        {
            this.SourceModifier = modifier;
            this.ModifierHandle = modifier.Handle;
            this.Name = modifier.Name;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the affected unit.</summary>
        public IAbilityUnit AffectedUnit { get; set; }

        public IModifierEffectApplier ModifierEffectApplier { get; set; }

        public double ModifierHandle { get; set; }

        public string Name { get; set; }

        public IReadOnlyDictionary<Type, IAbilityModifierPart> Parts => this.parts;

        /// <summary>Gets or sets the source modifier.</summary>
        public Modifier SourceModifier { get; set; }

        /// <summary>Gets or sets the source skill.</summary>
        public IAbilitySkill SourceSkill { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add part.</summary>
        /// <param name="part">The part.</param>
        /// <typeparam name="T">The type of part</typeparam>
        public void AddPart<T>(T part) where T : IAbilityModifierPart
        {
            var type = typeof(T);

            // if (this.Parts.ContainsKey(type))
            // {
            // return;
            // }

            // this.GetType().GetProperties().FirstOrDefault(x => x.PropertyType == type)?.SetValue(this, part);
            this.parts.Add(type, part);
        }

        public void AssignModifierEffectApplier(IModifierEffectApplier modifierEffectApplier)
        {
            this.ModifierEffectApplier = modifierEffectApplier;
            this.AddPart(modifierEffectApplier);
            this.ModifierEffectApplier.Initialize();
        }

        /// <summary>The dispose.</summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            foreach (var keyValuePair in this.Parts)
            {
                keyValuePair.Value.Dispose();
            }
        }

        public T GetPart<T>() where T : IAbilityModifierPart
        {
            IAbilityModifierPart part;
            if (!this.Parts.TryGetValue(typeof(T), out part))
            {
                return (T)part;
            }

            return (T)part;
        }

        public void OnDraw()
        {
        }

        public void RemovePart<T>() where T : IAbilityModifierPart
        {
            this.parts.Remove(typeof(T));
        }

        #endregion
    }
}