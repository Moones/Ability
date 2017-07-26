// <copyright file="SkillManipulatedDamageCalculatorWorker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>The skill damage calculator worker.</summary>
    internal abstract class SkillManipulatedDamageCalculatorWorker : ISkillManipulatedDamageCalculatorWorker
    {
        #region Fields

        private float damageValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="SkillManipulatedDamageCalculatorWorker" /> class.</summary>
        /// <param name="skill">The skill.</param>
        /// <param name="target">The target.</param>
        protected SkillManipulatedDamageCalculatorWorker(IAbilitySkill skill, IAbilityUnit target)
        {
            this.Skill = skill;
            this.Target = target;
        }

        #endregion

        #region Public Properties

        public Notifier DamageChanged { get; } = new Notifier();

        /// <summary>Gets or sets the damage value.</summary>
        public float DamageValue
        {
            get
            {
                return this.damageValue;
            }

            set
            {
                if (Math.Abs(this.damageValue - value) < 1)
                {
                    return;
                }

                this.DamageChanged.Notify();
                this.damageValue = value;
            }
        }

        /// <summary>The skill.</summary>
        public IAbilitySkill Skill { get; set; }

        /// <summary>The target.</summary>
        public IAbilityUnit Target { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual void Dispose()
        {
            this.DamageChanged.Dispose();
        }

        public abstract void UpdateDamage(float rawDamage);

        #endregion
    }
}