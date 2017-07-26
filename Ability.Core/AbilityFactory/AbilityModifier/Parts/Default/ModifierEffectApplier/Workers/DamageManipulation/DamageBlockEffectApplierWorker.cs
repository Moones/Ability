// <copyright file="DamageBlockEffectApplierWorker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;

    internal class DamageBlockEffectApplierWorker : EffectApplierWorker
    {
        #region Constructors and Destructors

        public DamageBlockEffectApplierWorker(
            IAbilitySkill skill,
            bool updateWithLevel,
            Func<IAbilitySkill, float> valueGetter)
            : base(updateWithLevel)
        {
            this.ValueGetter = valueGetter;
            this.Skill = skill;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ValueGetter.Invoke(this.Skill);
                            unit.DamageManipulation.AddDamageBlock(this.Skill.SkillHandle, this.Value);
                        };
                };
            this.RemoveEffectActionGetter =
                () => unit => { unit.DamageManipulation.RemoveDamageBlock(this.Skill.SkillHandle); };
            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ValueGetter.Invoke(this.Skill);
                            unit.DamageManipulation.UpdateDamageBlock(this.Skill.SkillHandle, this.Value);
                        };
                };
        }

        internal DamageBlockEffectApplierWorker(
            IAbilityModifier modifier,
            bool updateWithLevel,
            Func<IAbilityModifier, float> modifierValueGetter)
            : base(updateWithLevel)
        {
            this.Modifier = modifier;
            this.ModifierValueGetter = modifierValueGetter;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ModifierValueGetter.Invoke(this.Modifier);
                            unit.DamageManipulation.AddDamageBlock(this.Modifier.ModifierHandle, this.Value);
                        };
                };
            this.RemoveEffectActionGetter =
                () => unit => { unit.DamageManipulation.RemoveDamageBlock(this.Modifier.ModifierHandle); };
            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ModifierValueGetter.Invoke(this.Modifier);
                            unit.DamageManipulation.UpdateDamageBlock(this.Modifier.ModifierHandle, this.Value);
                        };
                };
        }

        #endregion

        #region Public Properties

        public IAbilityModifier Modifier { get; }

        public Func<IAbilityModifier, float> ModifierValueGetter { get; }

        public IAbilitySkill Skill { get; }

        public float Value { get; set; }

        public Func<IAbilitySkill, float> ValueGetter { get; }

        #endregion
    }
}