// <copyright file="FleshGolemModifierEffectApplier.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Heroes.Undying.FleshGolem.ModifierEffectApplier
{
    using System;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;

    using Ensage.Common.Extensions;

    public class FleshGolemModifierEffectApplier : ModifierEffectApplier
    {
        #region Constructors and Destructors

        public FleshGolemModifierEffectApplier(IAbilityModifier modifier)
            : base(modifier)
        {
            var worker = new EffectApplierWorker(
                false,
                () =>
                    {
                        return unit =>
                            {
                                Console.WriteLine("applying undying");
                                unit.DamageManipulation.DamageAmplification.AddSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, f) =>
                                        {
                                            var baseAmp = .05 * modifier.SourceSkill.Level.Current;
                                            var distance =
                                                modifier.SourceSkill.Owner.Position.Current.Distance2D(
                                                    modifier.AffectedUnit.Position.Current);
                                            if (distance <= 200)
                                            {
                                                return baseAmp + 0.15;
                                            }
                                            else if (distance > 750)
                                            {
                                                return 0.1;
                                            }
                                            else
                                            {
                                                return baseAmp + (750 - distance) * 0.03 / 110;
                                            }
                                        });
                            };
                    },
                () => unit => { unit.DamageManipulation.DamageAmplification.RemoveSpecialModifierValue(modifier); },
                null);
            this.Workers.Add(worker);
        }

        #endregion
    }
}