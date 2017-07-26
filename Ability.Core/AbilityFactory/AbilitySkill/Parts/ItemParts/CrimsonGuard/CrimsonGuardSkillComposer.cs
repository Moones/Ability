// <copyright file="CrimsonGuardSkillComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.CrimsonGuard
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;

    using Ensage;
    using Ensage.Common.Extensions;

    [Export(typeof(IAbilitySkillItemComposer))]
    [AbilitySkillItemMetadata((uint)AbilityId.item_crimson_guard)]
    internal class CrimsonGuardSkillComposer : DefaultSkillComposer
    {
        #region Constructors and Destructors

        internal CrimsonGuardSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_item_crimson_guard_extra",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new DamageBlockEffectApplierWorker(
                                                                            modifier,
                                                                            false,
                                                                            abilityModifier =>
                                                                                abilityModifier.SourceSkill.SourceItem
                                                                                    .GetAbilityData(
                                                                                        abilityModifier.AffectedUnit
                                                                                            .SourceUnit.IsRanged
                                                                                            ? "block_damage_ranged_active"
                                                                                            : "block_damage_melee_active"))
                                                                    }
                                                        }),
                                            false,
                                            true,
                                            true)
                                    }
                        });
            this.AssignPart<IEffectApplier>(
                skill =>
                    new EffectApplier(skill)
                        {
                            Workers =
                                new List<IEffectApplierWorker>
                                    {
                                        new DamageBlockEffectApplierWorker(
                                            skill,
                                            false,
                                            abilitySkill =>
                                                abilitySkill.SourceItem.GetAbilityData(
                                                    abilitySkill.Owner.SourceUnit.IsRanged
                                                        ? "block_damage_ranged"
                                                        : "block_damage_melee"))
                                    }
                        });
        }

        #endregion
    }
}