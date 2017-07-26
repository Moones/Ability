// <copyright file="ModifierGeneratorWorker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator
{
    using System;

    using Ability.Core.AbilityFactory.AbilityModifier;

    public class ModifierGeneratorWorker : IDisposable
    {
        #region Constructors and Destructors

        public ModifierGeneratorWorker(
            string modifierName,
            Action<IAbilityModifier> generateModifier,
            bool affectsEnemies = false,
            bool affectsAllies = false,
            bool affectsSelf = false,
            bool affectsEveryone = false)
        {
            this.ModifierName = modifierName;
            this.GenerateModifier = generateModifier;
            this.AffectsEnemies = affectsEnemies;
            this.AffectsAllies = affectsAllies;
            this.AffectsSelf = affectsSelf;
            this.AffectsEveryone = affectsEveryone;
        }

        #endregion

        #region Public Properties

        public bool AffectsAllies { get; }

        public bool AffectsEnemies { get; }

        public bool AffectsEveryone { get; }

        public bool AffectsSelf { get; }

        public Action<IAbilityModifier> GenerateModifier { get; }

        public string ModifierName { get; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        #endregion
    }
}