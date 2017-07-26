// <copyright file="BristlebackEffectApplier.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Bristleback.Bristleback.EffectApplier
{
    using System;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier;

    using Ensage.Common.Extensions;

    public class BristlebackEffectApplier : EffectApplier
    {
        #region Constructors and Destructors

        public BristlebackEffectApplier(IAbilitySkill skill)
            : base(skill)
        {
            var worker = new EffectApplierWorker(
                false,
                () =>
                    {
                        return unit =>
                            {
                                unit.DamageManipulation.DamageReduction.AddSpecialSkillValue(
                                    skill,
                                    (abilityUnit, damageValue) =>
                                        {
                                            var angle = unit.SourceUnit.FindRelativeAngle(abilityUnit.Position.Current)
                                                        % (2 * Math.PI * 180) / Math.PI;
                                            if (angle >= 110 && angle <= 250)
                                            {
                                                return (1 + skill.Level.Current) * 0.08;
                                            }
                                            else if (angle >= 70 && angle <= 290)
                                            {
                                                return (1 + skill.Level.Current) * 0.04;
                                            }

                                            return 0;
                                        });
                            };
                    },
                () => unit => { unit.DamageManipulation.DamageReduction.RemoveSpecialSkillValue(skill); },
                null);
            this.Workers.Add(worker);
        }

        #endregion
    }
}