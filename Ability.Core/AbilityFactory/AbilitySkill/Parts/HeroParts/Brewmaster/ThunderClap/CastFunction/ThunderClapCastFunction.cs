// <copyright file="ThunderClapCastFunction.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Brewmaster.ThunderClap.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    using Ability.Core.AbilityFactory.AbilityUnit;

    public class ThunderClapCastFunction : DefaultCastingFunction
    {
        #region Constructors and Destructors

        public ThunderClapCastFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override bool CanCast()
        {
            return base.CanCast() && this.Skill.CastRange.IsTargetInRange
                   && (this.Skill.Owner.TargetSelector.Target.Health.Current
                       > this.Skill.Owner.AttackDamage.GetDamage(this.Skill.Owner.TargetSelector.Target) * 1.5
                       || this.Skill.Owner.TargetSelector.LastDistanceToTarget > 350);
        }

        public override bool TargetIsValid(IAbilityUnit target)
        {
            return base.TargetIsValid(target) && this.Skill.CastRange.TargetInRange(target);
        }

        #endregion
    }
}