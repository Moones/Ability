// <copyright file="SkillRawDamageCalculatorWorker.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    internal abstract class SkillRawDamageCalculatorWorker : ISkillRawDamageCalculatorWorker
    {
        #region Fields

        private readonly DataObserver<ISkillLevel> levelObserver;

        private ISkillManipulatedDamageCalculatorWorker manipulatedDamageWorker;

        private float rawDamageValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="SkillRawDamageCalculatorWorker" /> class.</summary>
        /// <param name="skill">The skill.</param>
        /// <param name="target">The target.</param>
        /// <param name="manipulatedDamageWorker">The manipulated damage worker.</param>
        protected SkillRawDamageCalculatorWorker(
            IAbilitySkill skill,
            IAbilityUnit target,
            ISkillManipulatedDamageCalculatorWorker manipulatedDamageWorker)
        {
            this.Skill = skill;
            this.Target = target;
            this.ManipulatedDamageWorker = manipulatedDamageWorker;

            this.levelObserver = new DataObserver<ISkillLevel>(level => { this.UpdateRawDamage(); });
            this.levelObserver.Subscribe(this.Skill.Level);
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the damage changed.</summary>
        public Notifier DamageChanged { get; } = new Notifier();

        public ISkillManipulatedDamageCalculatorWorker ManipulatedDamageWorker
        {
            get
            {
                return this.manipulatedDamageWorker;
            }

            set
            {
                this.manipulatedDamageWorker = value;
                this.manipulatedDamageWorker.UpdateDamage(this.RawDamageValue);
            }
        }

        /// <summary>Gets or sets the raw damage value.</summary>
        public float RawDamageValue
        {
            get
            {
                return this.rawDamageValue;
            }

            set
            {
                if (Math.Abs(value - this.rawDamageValue) < 1)
                {
                    return;
                }

                this.manipulatedDamageWorker.UpdateDamage(this.RawDamageValue);
                this.DamageChanged.Notify();
                this.rawDamageValue = value;
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
            this.levelObserver.Dispose();
            this.DamageChanged.Dispose();
            this.ManipulatedDamageWorker.Dispose();
        }

        public abstract void UpdateRawDamage();

        #endregion
    }
}