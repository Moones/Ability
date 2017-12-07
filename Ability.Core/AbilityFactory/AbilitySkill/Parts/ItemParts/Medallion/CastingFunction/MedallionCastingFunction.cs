// <copyright file="MedallionCastingFunction.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Medallion.CastingFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;

    public class MedallionCastingFunction : DefaultCastingFunction
    {
        #region Constructors and Destructors

        public MedallionCastingFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override bool CanCast()
        {
            return base.CanCast()
                   && this.Skill.Owner.TargetSelector.LastDistanceToTarget < this.Skill.Owner.AttackRange.Value + 150
                   && this.Skill.Owner.TargetSelector.Target.Modifiers.Attackable
                   && this.Skill.Owner.TargetSelector.Target.Health.Current
                   > this.Skill.Owner.AttackDamage.GetDamage(this.Skill.Owner.TargetSelector.Target) * 1.5;
        }

        #endregion
    }
}