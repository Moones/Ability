// <copyright file="IceBlastModifierEffectApplier.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Heroes.AncientApparition.IceBlast.ModifierEffectApplier
{
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation;

    using Ensage.Common.Extensions;

    public class IceBlastModifierEffectApplier : ModifierEffectApplier
    {
        #region Constructors and Destructors

        public IceBlastModifierEffectApplier(IAbilityModifier modifier)
            : base(modifier)
        {
            var value = 0d;
            var worker = new EffectApplierWorker(
                true,
                () =>
                    {
                        return unit =>
                            {
                                value = modifier.SourceSkill.SourceAbility.GetAbilityData("kill_pct") / 100;
                                unit.DamageManipulation.Aa = new ValueHolder<float>(
                                    (abilityUnit, f) =>
                                        {
                                            var percent = modifier.AffectedUnit.Health.Maximum * value;
                                            if (modifier.AffectedUnit.Health.Current - f <= percent)
                                            {
                                                return (float)percent;
                                            }

                                            return 0;
                                        },
                                    true,
                                    () => modifier.SourceModifier.DieTime);
                            };
                    },
                () => unit => unit.DamageManipulation.Aa = null,
                () =>
                    {
                        return unit =>
                            {
                                value = modifier.SourceSkill.SourceAbility.GetAbilityData("kill_pct") / 100;
                                unit.DamageManipulation.Aa = new ValueHolder<float>(
                                    (abilityUnit, f) =>
                                        {
                                            var percent = modifier.AffectedUnit.Health.Maximum * value;
                                            if (modifier.AffectedUnit.Health.Current - f <= percent)
                                            {
                                                return (float)percent;
                                            }

                                            return 0;
                                        },
                                    true,
                                    () => modifier.SourceModifier.DieTime);
                            };
                    });
            this.Workers.Add(worker);
        }

        #endregion
    }
}