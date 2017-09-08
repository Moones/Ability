// <copyright file="CastRange.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastRange
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    public class CastRange : ICastRange
    {
        #region Fields

        private float baseValue;

        private float bonusValue;

        #endregion

        #region Constructors and Destructors

        public CastRange(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        public float BaseValue
        {
            get
            {
                return this.baseValue;
            }

            set
            {
                this.baseValue = value;
                this.Value = this.baseValue + this.bonusValue;
            }
        }

        public float BonusValue
        {
            get
            {
                return this.bonusValue;
            }

            set
            {
                this.bonusValue = value;
                this.Value = this.bonusValue + this.baseValue;
            }
        }

        public bool IsTargetInRange { get; set; }

        public IAbilitySkill Skill { get; set; }

        public float Value { get; set; }

        public bool TargetInRange(IAbilityUnit target)
        {
            return this.Skill.Owner.TargetSelector.LastDistanceToTarget <= this.Value;
        }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public virtual void Initialize()
        {
            this.UpdateValue();
            this.Skill.Level.Subscribe(new DataObserver<ISkillLevel>(level => { this.UpdateValue(); }));

            this.Skill.Owner.TargetSelector?.TargetDistanceChanged.Subscribe(
                () => { this.IsTargetInRange = this.Skill.Owner.TargetSelector.LastDistanceToTarget <= this.Value; });
        }

        public virtual void UpdateValue()
        {
            this.BaseValue = Math.Max(this.Skill.SourceAbility.CastRange + 150, 300) + this.Skill.Owner.SourceUnit.HullRadius;
        }

        #endregion
    }
}