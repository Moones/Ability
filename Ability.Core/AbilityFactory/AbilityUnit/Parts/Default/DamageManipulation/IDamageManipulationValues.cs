// <copyright file="IDamageManipulationValues.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation
{
    using System;

    using Ability.Core.AbilityFactory.AbilityModifier;
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.Utilities;

    public interface IDamageManipulationValues
    {
        #region Public Properties

        IAbilityUnit Unit { get; }

        Notifier ValueChanged { get; }

        #endregion

        #region Public Methods and Operators

        void AddModifierValue(IAbilityModifier modifier, double value, bool willExpire = false);

        void AddSkillValue(IAbilitySkill skill, double value);

        void AddSpecialModifierValue(
            IAbilityModifier modifier,
            Func<IAbilityUnit, float, double> getValue,
            bool willExpire = false);

        void AddSpecialSkillValue(IAbilitySkill skill, Func<IAbilityUnit, float, double> getValue);

        double GetPredictedValue(IAbilityUnit source, float damageValue, float time);

        double GetValue(IAbilityUnit source, float damageValue);

        void RemoveModifierValue(IAbilityModifier modifier, double value);

        void RemoveSkillValue(IAbilitySkill skill, double value);

        void RemoveSpecialModifierValue(IAbilityModifier modifier);

        void RemoveSpecialSkillValue(IAbilitySkill skill);

        void UpdateModifierValue(IAbilityModifier modifier, double newValue);

        void UpdateSkillValue(IAbilitySkill skill, double newValue);

        void UpdateSpecialModifierValue(IAbilityModifier modifier, Func<IAbilityUnit, float, double> newGetValue);

        void UpdateSpecialSkillValue(IAbilitySkill skill, Func<IAbilityUnit, float, double> newGetValue);

        #endregion
    }
}