// <copyright file="BattleCryCastingFunction.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.BattleCry.CastingFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.ControllableUnits;

    using Ensage.Common.Extensions;

    public class BattleCryCastingFunction : DefaultCastingFunction
    {
        #region Fields

        private LoneDruidControllableUnits controllableUnits;

        #endregion

        #region Constructors and Destructors

        public BattleCryCastingFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override bool CanCast()
        {
            return base.CanCast()
                   && (this.Skill.Owner.AttackRange.IsInAttackRange(this.Skill.Owner.TargetSelector.Target)
                       || this.controllableUnits.Bear != null
                       && this.controllableUnits.Bear.AttackRange.IsInAttackRange(
                           this.Skill.Owner.TargetSelector.Target))
                   && (this.Skill.Owner.TargetSelector.Target.SourceUnit.IsAttacking()
                       || !this.Skill.Owner.TargetSelector.Target.SourceUnit.CanMove()
                       || this.Skill.Owner.TargetSelector.Target.SourceUnit.MovementSpeed
                       < this.Skill.Owner.SourceUnit.MovementSpeed);
        }

        public override void Initialize()
        {
            base.Initialize();

            this.controllableUnits = this.Skill.Owner.ControllableUnits as LoneDruidControllableUnits;
        }

        #endregion
    }
}