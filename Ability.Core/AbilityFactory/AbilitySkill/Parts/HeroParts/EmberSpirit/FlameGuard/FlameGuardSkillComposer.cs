// <copyright file="FlameGuardSkillComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.EmberSpirit.FlameGuard
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;

    using Ensage.Common.Enums;
    using Ensage.Common.Extensions;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.ember_spirit_flame_guard)]
    internal class FlameGuardSkillComposer : DefaultSkillComposer
    {
        #region Constructors and Destructors

        internal FlameGuardSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_ember_spirit_flame_guard",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new MagicOnlyEffectApplierWorker(
                                                                            modifier,
                                                                            true,
                                                                            abilityModifier =>
                                                                                abilityModifier.SourceSkill
                                                                                    .SourceAbility.GetAbilityData(
                                                                                        "absorb_amount"))
                                                                    }
                                                        }),
                                            false,
                                            false,
                                            true)
                                    }
                        });
        }

        #endregion
    }
}