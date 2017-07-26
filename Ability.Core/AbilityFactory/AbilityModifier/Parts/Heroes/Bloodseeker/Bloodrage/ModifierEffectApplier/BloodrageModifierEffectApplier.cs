// <copyright file="BloodrageModifierEffectApplier.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Heroes.Bloodseeker.Bloodrage.ModifierEffectApplier
{
    using System;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;

    using Ensage.Common.Extensions;

    public class BloodrageModifierEffectApplier : ModifierEffectApplier
    {
        #region Fields

        private EffectApplierWorker ampApplier;

        private double realValue;

        private double value;

        #endregion

        #region Constructors and Destructors

        public BloodrageModifierEffectApplier(IAbilityModifier modifier)
            : base(modifier)
        {
            this.ampApplier = new EffectApplierWorker(
                true,
                () =>
                    {
                        return unit =>
                            {
                                this.realValue =
                                    Math.Floor(
                                        this.Modifier.SourceSkill.SourceAbility.GetAbilityData("damage_increase_pct"))
                                    / 100;
                                this.value = this.realValue;
                                unit.DamageManipulation.DamageAmplification.AddSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, damageValue) =>
                                        {
                                            if (
                                                modifier.AffectedUnit.Position.Current.Distance2D(
                                                    abilityUnit.Position.Current) < 2200)
                                            {
                                                return this.value;
                                            }
                                            else
                                            {
                                                return this.value / 2;
                                            }
                                        },
                                    true);
                                unit.DamageManipulation.AmpFromMe.AddSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, damageValue) =>
                                        {
                                            if (
                                                modifier.AffectedUnit.Position.Current.Distance2D(
                                                    abilityUnit.Position.Current) < 2200)
                                            {
                                                return this.value;
                                            }
                                            else
                                            {
                                                return this.value / 2;
                                            }
                                        },
                                    true);
                            };
                    },
                () =>
                    {
                        return unit =>
                            {
                                unit.DamageManipulation.DamageAmplification.RemoveSpecialModifierValue(modifier);
                                unit.DamageManipulation.AmpFromMe.RemoveSpecialModifierValue(modifier);
                            };
                    },
                () =>
                    {
                        return unit =>
                            {
                                this.realValue =
                                    Math.Floor(
                                        this.Modifier.SourceSkill.SourceAbility.GetAbilityData("damage_increase_pct"))
                                    / 100;
                                this.value = this.realValue;
                                unit.DamageManipulation.DamageAmplification.UpdateSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, damageValue) =>
                                        {
                                            if (
                                                modifier.AffectedUnit.Position.Current.Distance2D(
                                                    abilityUnit.Position.Current) < 2200)
                                            {
                                                return this.value;
                                            }
                                            else
                                            {
                                                return this.value / 2;
                                            }
                                        });
                                unit.DamageManipulation.AmpFromMe.UpdateSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, damageValue) =>
                                        {
                                            if (
                                                modifier.AffectedUnit.Position.Current.Distance2D(
                                                    abilityUnit.Position.Current) < 2200)
                                            {
                                                return this.value;
                                            }
                                            else
                                            {
                                                return this.value / 2;
                                            }
                                        });
                            };
                    });

            this.Workers.Add(this.ampApplier);
        }

        #endregion
    }
}