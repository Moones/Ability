// <copyright file="DamageReductionEffectApplierWorker.cs" company="EnsageSharp">
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

    internal class DamageReductionEffectApplierWorker : EffectApplierWorker
    {
        #region Constructors and Destructors

        public DamageReductionEffectApplierWorker(
            IAbilitySkill skill,
            bool updateWithLevel,
            Func<IAbilitySkill, double> valueGetter)
            : base(updateWithLevel)
        {
            this.Skill = skill;
            this.ValueGetter = valueGetter;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ValueGetter.Invoke(this.Skill);
                            unit.DamageManipulation.DamageReduction.AddSkillValue(skill, this.Value);
                        };
                };
            this.RemoveEffectActionGetter =
                () => unit => { unit.DamageManipulation.DamageReduction.RemoveSkillValue(skill, this.Value); };

            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ValueGetter.Invoke(this.Skill);
                            unit.DamageManipulation.DamageReduction.UpdateSkillValue(skill, this.Value);
                        };
                };
        }

        internal DamageReductionEffectApplierWorker(
            IAbilityModifier modifier,
            bool updateWithLevel,
            Func<IAbilityModifier, double> modifierValueGetter)
            : base(updateWithLevel)
        {
            this.Modifier = modifier;
            this.ModifierValueGetter = modifierValueGetter;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ModifierValueGetter.Invoke(this.Modifier);
                            unit.DamageManipulation.DamageReduction.AddModifierValue(modifier, this.Value, true);
                        };
                };
            this.RemoveEffectActionGetter =
                () => unit => { unit.DamageManipulation.DamageReduction.RemoveModifierValue(modifier, this.Value); };

            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ModifierValueGetter.Invoke(this.Modifier);
                            unit.DamageManipulation.DamageReduction.UpdateModifierValue(modifier, this.Value);
                        };
                };
        }

        #endregion

        #region Public Properties

        public IAbilityModifier Modifier { get; }

        public Func<IAbilityModifier, double> ModifierValueGetter { get; }

        public IAbilitySkill Skill { get; }

        public double Value { get; set; }

        public Func<IAbilitySkill, double> ValueGetter { get; }

        #endregion
    }
}