﻿// <copyright file="ThunderClapCastRange.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Brewmaster.ThunderClap.CastRange
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastRange;

    using Ensage.Common.Extensions;

    public class ThunderClapCastRange : CastRange
    {
        #region Constructors and Destructors

        public ThunderClapCastRange(IAbilitySkill skill)
            : base(skill)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override void UpdateValue()
        {
            this.BaseValue = this.Skill.SourceAbility.GetAbilityData("radius", this.Skill.Level.Current);
        }

        #endregion
    }
}