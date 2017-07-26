// <copyright file="DamageAmplificationEffectApplierWorker.cs" company="EnsageSharp">
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

    internal class DamageAmplificationEffectApplierWorker : EffectApplierWorker
    {
        #region Constructors and Destructors

        internal DamageAmplificationEffectApplierWorker(
            IAbilityModifier modifier,
            bool updateWithLevel,
            Func<IAbilityModifier, double> valueGetter)
            : base(updateWithLevel)
        {
            this.Modifier = modifier;
            this.ValueGetter = valueGetter;
            this.ApplyEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ValueGetter.Invoke(this.Modifier);
                            unit.DamageManipulation.DamageAmplification.AddModifierValue(modifier, this.Value, true);
                        };
                };
            this.RemoveEffectActionGetter =
                () => unit => { unit.DamageManipulation.DamageAmplification.RemoveModifierValue(modifier, this.Value); };
            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ValueGetter.Invoke(this.Modifier);
                            unit.DamageManipulation.DamageAmplification.UpdateModifierValue(modifier, this.Value);
                        };
                };
        }

        #endregion

        #region Public Properties

        public IAbilityModifier Modifier { get; }

        public double Value { get; set; }

        public Func<IAbilityModifier, double> ValueGetter { get; }

        #endregion
    }
}