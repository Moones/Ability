// <copyright file="IDamageManipulation.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>The DamageReduction interface.</summary>
    public interface IDamageManipulation : IAbilityUnitPart
    {
        #region Public Properties

        IValueHolder<float> Aa { get; set; }

        Dictionary<double, Tuple<float, float>> AaMinusEvents { get; }

        Dictionary<double, Tuple<float, float>> AaPlusEvents { get; }

        IDamageManipulationValues AmpFromMe { get; set; }

        Notifier BecameInvulnerableNotifier { get; }

        Notifier BecameMagicImmuneNotifier { get; }

        IDamageManipulationValues DamageAmplification { get; set; }

        float DamageBlock { get; set; }

        /// <summary>Gets the damage blocks.</summary>
        Dictionary<double, float> DamageBlocks { get; }

        IDamageManipulationValues DamageNegation { get; }

        IDamageManipulationValues DamageReduction { get; set; }

        bool IsAttackImmune { get; set; }

        bool IsInvul { get; set; }

        bool IsMagicImmune { get; set; }

        IDamageManipulationValues MagicalDamageAbsorb { get; set; }

        Notifier MagicalDamageReductionChanged { get; }

        bool MagicalDamageShield { get; }

        IDamageManipulationValues ManaShield { get; set; }

        Notifier PhysicalDamageReductionChanged { get; }

        bool PhysicalDamageShield { get; }

        Notifier PureDamageReductionChanged { get; }

        bool PureDamageShield { get; }

        double ReduceOther { get; set; }

        Dictionary<double, Tuple<float, double>> ReduceOtherMinusEvents { get; }

        Dictionary<double, Tuple<float, double>> ReduceOtherPlusEvents { get; }

        float ReduceStatic { get; set; }

        Dictionary<double, Tuple<float, float>> ReduceStaticMinusEvents { get; }

        Dictionary<double, Tuple<float, float>> ReduceStaticPlusEvents { get; }

        #endregion

        #region Public Methods and Operators

        void AddDamageBlock(double handle, float value);

        void AddDamageShield(double handle, bool magical, bool physical, bool pure);

        /// <summary>The reduce auto attack damage.</summary>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage amplification.</param>
        /// <param name="minusArmor">The minus armor.</param>
        /// <returns>The <see cref="float" />.</returns>
        float ManipulateIncomingAutoAttackDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusArmor,
            float time);

        float ManipulateIncomingAutoAttackDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusArmor);

        /// <summary>The reduce magical damage.</summary>
        /// <param name="source">The source.</param>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage Amplification.</param>
        /// <param name="minusMagicResistancePerc">The minus Magic Resistance Perc.</param>
        /// <returns>The <see cref="float" />.</returns>
        float ManipulateIncomingMagicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusMagicResistancePerc,
            float time);

        float ManipulateIncomingMagicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusMagicResistancePerc);

        /// <summary>The reduce physical damage.</summary>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage Amplification.</param>
        /// <returns>The <see cref="float" />.</returns>
        float ManipulateIncomingPhysicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusDamageResistancePerc,
            float minusArmor,
            float time);

        float ManipulateIncomingPhysicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusDamageResistancePerc,
            float minusArmor);

        /// <summary>The reduce pure damage.</summary>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage Amplification.</param>
        /// <returns>The <see cref="float" />.</returns>
        float ManipulateIncomingPureDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float time);

        float ManipulateIncomingPureDamage(IAbilityUnit source, float damageValue, double damageAmplification);

        void RemoveDamageBlock(double handle);

        void RemoveDamageShield(double handle, bool magical, bool physical, bool pure);

        void UpdateDamageBlock(double handle, float newValue);

        #endregion
    }
}