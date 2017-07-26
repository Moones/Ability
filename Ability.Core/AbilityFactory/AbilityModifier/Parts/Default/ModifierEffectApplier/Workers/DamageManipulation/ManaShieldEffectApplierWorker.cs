// <copyright file="ManaShieldEffectApplierWorker.cs" company="EnsageSharp">
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

    internal class ManaShieldEffectApplierWorker : EffectApplierWorker
    {
        #region Constructors and Destructors

        internal ManaShieldEffectApplierWorker(
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
                            unit.DamageManipulation.ManaShield.AddSpecialModifierValue(
                                modifier,
                                (unit1, damageValue) =>
                                    {
                                        if (modifier.AffectedUnit.Mana.Current >= damageValue * .6 / this.Value)
                                        {
                                            return 0.6;
                                        }
                                        else
                                        {
                                            return modifier.AffectedUnit.Mana.Current * this.Value / damageValue;
                                        }
                                    });
                        };
                };
            this.RemoveEffectActionGetter =
                () => unit => { unit.DamageManipulation.ManaShield.RemoveSpecialModifierValue(modifier); };
            this.UpdateEffectActionGetter = () =>
                {
                    return unit =>
                        {
                            this.Value = this.ValueGetter.Invoke(this.Modifier);
                            unit.DamageManipulation.ManaShield.UpdateSpecialModifierValue(
                                modifier,
                                (unit1, damageValue) =>
                                    {
                                        if (modifier.AffectedUnit.Mana.Current >= damageValue * .6 / this.Value)
                                        {
                                            return 0.6;
                                        }
                                        else
                                        {
                                            return modifier.AffectedUnit.Mana.Current * this.Value / damageValue;
                                        }
                                    });
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