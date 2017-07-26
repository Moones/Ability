// <copyright file="AphoticShieldSkillComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Abaddon.AphoticShield
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilityTalent;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.abaddon_aphotic_shield)]
    internal class AphoticShieldSkillComposer : DefaultSkillComposer
    {
        #region Constructors and Destructors

        internal AphoticShieldSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    {
                        Func<IAbilityModifier, double> getValue =
                            abilityModifier => abilityModifier.SourceSkill.SourceAbility.GetAbilityData("damage_absorb");

                        skill.Owner.SkillBook.TalentAdded.Subscribe(
                            new DataObserver<IAbilityTalent>(
                                talent =>
                                    {
                                        if (talent.SourceAbility.Id == AbilityId.special_bonus_unique_abaddon)
                                        {
                                            talent.TalentLeveledNotifier.Subscribe(
                                                () =>
                                                    {
                                                        getValue =
                                                            modifier =>
                                                                modifier.SourceSkill.SourceAbility.GetAbilityData(
                                                                    "damage_absorb")
                                                                + talent.SourceAbility.GetAbilityData("value");
                                                    });
                                        }
                                    }));

                        return new ModifierGenerator(skill)
                                   {
                                       Workers =
                                           new List<ModifierGeneratorWorker>
                                               {
                                                   new ModifierGeneratorWorker(
                                                       "modifier_abaddon_aphotic_shield",
                                                       modifier =>
                                                           {
                                                               modifier.AssignModifierEffectApplier(
                                                                   new ModifierEffectApplier(modifier)
                                                                       {
                                                                           Workers =
                                                                               new List<IEffectApplierWorker>
                                                                                   {
                                                                                       new DamageNegationEffectApplierWorker
                                                                                           (modifier, true, getValue)
                                                                                   }
                                                                       });
                                                           },
                                                       false,
                                                       true,
                                                       true)
                                               }
                                   };
                    });
        }

        #endregion
    }
}