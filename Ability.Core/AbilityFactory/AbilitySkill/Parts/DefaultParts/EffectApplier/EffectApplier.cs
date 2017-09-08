// <copyright file="EffectApplier.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.Utilities;

    public class EffectApplier : IEffectApplier
    {
        #region Fields

        private DataObserver<ISkillLevel> levelObserver;

        #endregion

        #region Constructors and Destructors

        public EffectApplier(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        public IAbilitySkill Skill { get; set; }

        public ICollection<IEffectApplierWorker> Workers { get; set; } = new List<IEffectApplierWorker>();

        #endregion

        #region Public Methods and Operators

        public void ApplyEffect()
        {
            foreach (var modifierEffectApplierWorker in this.Workers)
            {
                modifierEffectApplierWorker.ApplyEffect(this.Skill.Owner);
            }
        }

        public void Dispose()
        {
            foreach (var modifierEffectApplierWorker in this.Workers)
            {
                modifierEffectApplierWorker.RemoveEffect();
            }

            this.levelObserver.Dispose();
        }

        public void Initialize()
        {
            this.ApplyEffect();
            Console.WriteLine("applying effect " + this.Skill.Name);
            this.levelObserver = new DataObserver<ISkillLevel>(level => this.UpdateEffect());
            this.levelObserver.Subscribe(this.Skill.Level);
        }

        public void UpdateEffect()
        {
            foreach (var modifierEffectApplierWorker in this.Workers)
            {
                if (modifierEffectApplierWorker.UpdateWithLevel)
                {
                    modifierEffectApplierWorker.UpdateEffect();
                }
            }
        }

        #endregion
    }
}