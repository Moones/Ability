// <copyright file="OrchidCastFunction.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Orchid.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;

    using Ensage;

    public class OrchidCastFunction : DefaultCastingFunction
    {
        #region Constructors and Destructors

        public OrchidCastFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override bool CanCast()
        {
            return base.CanCast() && !this.Skill.Owner.TargetSelector.Target.Modifiers.Silenced
                   && this.Skill.Owner.TargetSelector.Target.DisableManager.CanDisable(200 + Game.Ping)
                   && (this.Skill.Owner.SourceUnit.IsVisibleToEnemies
                           ? this.Skill.Owner.TargetSelector.LastDistanceToTarget < 650
                           : this.Skill.Owner.TargetSelector.LastDistanceToTarget < 300);
        }

        #endregion
    }
}