// <copyright file="StampedeSkillComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.CentaurWarrunner.Stampede
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.centaur_stampede)]
    internal class StampedeSkillComposer : DefaultSkillComposer
    {
        #region Public Methods and Operators

        /// <summary>The compose.</summary>
        /// <param name="skill">The skill.</param>
        public override void Compose(IAbilitySkill skill)
        {
            base.Compose(skill);

            var skillAddedObserver = new DataObserver<IAbilitySkill>(
                add =>
                    {
                        if (add.IsItem && add.SourceItem.Id == AbilityId.item_ultimate_scepter)
                        {
                            skill.AddPart<IModifierGenerator>(
                                abilitySkill =>
                                    new ModifierGenerator(skill)
                                        {
                                            Workers =
                                                new List<ModifierGeneratorWorker>
                                                    {
                                                        new ModifierGeneratorWorker(
                                                            "modifier_centaur_stampede",
                                                            modifier =>
                                                                modifier.AssignModifierEffectApplier(
                                                                    new ModifierEffectApplier(modifier)
                                                                        {
                                                                            Workers =
                                                                                new List<IEffectApplierWorker>
                                                                                    {
                                                                                        new DamageReductionEffectApplierWorker
                                                                                        (
                                                                                            modifier,
                                                                                            false,
                                                                                            abilityModifier => 0.6)
                                                                                    }
                                                                        }),
                                                            false,
                                                            true,
                                                            true)
                                                    }
                                        });
                        }
                    });
            skillAddedObserver.Subscribe(skill.Owner.SkillBook.SkillAdded);
            skill.DisposeNotifier.Subscribe(() => skillAddedObserver.Dispose());
        }

        #endregion
    }
}