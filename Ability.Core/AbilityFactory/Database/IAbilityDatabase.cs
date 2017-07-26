// <copyright file="IAbilityDatabase.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.Database
{
    using Ability.Core.AbilityFactory.AbilitySkill.Data;

    /// <summary>
    ///     The PriorityDatabase interface.
    /// </summary>
    internal interface IAbilityDatabase
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        uint GetCastPriority(string skillName);

        /// <summary>
        ///     The get priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        uint GetCastPriority(string skillName, string heroName);

        /// <summary>
        ///     The get damage dealt priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        uint GetDamageDealtPriority(string skillName);

        /// <summary>
        ///     The get skill data.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <returns>
        ///     The <see cref="SkillJson" />.
        /// </returns>
        SkillJson GetSkillData(string skillName);

        #endregion
    }
}