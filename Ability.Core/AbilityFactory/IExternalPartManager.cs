// <copyright file="IExternalPartManager.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts;

    using Ensage;

    /// <summary>The ExternalPartManager interface.</summary>
    public interface IExternalPartManager
    {
        #region Public Methods and Operators

        void AddSkillItemPart<T>(Func<IAbilitySkill, T> factory, params uint[] abilityIds) where T : IAbilitySkillPart;

        void AddSkillPart<T>(Func<IAbilitySkill, T> factory) where T : IAbilitySkillPart;

        void AddSkillPart<T>(Func<IAbilitySkill, T> factory, params uint[] abilityIds) where T : IAbilitySkillPart;

        /// <summary>The add unit part.</summary>
        /// <param name="unitClassId">The unit class id.</param>
        /// <param name="factory">The factory.</param>
        /// <typeparam name="T">Part type</typeparam>
        void AddUnitPart<T>(HeroId unitClassId, Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart;

        /// <summary>The add unit part.</summary>
        /// <param name="factory">The factory.</param>
        /// <typeparam name="T">Part type</typeparam>
        void AddUnitPart<T>(Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart;

        #endregion
    }
}