// <copyright file="ExternalPartManager.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer;
    using Ability.Core.AbilityManager;

    using Ensage;

    [Export(typeof(IExternalPartManager))]
    internal class ExternalPartManager : IExternalPartManager
    {
        #region Properties

        [Import(typeof(IAbilityManager))]
        protected Lazy<IAbilityManager> AbilityManager { get; set; }

        [ImportMany]
        protected IEnumerable<Lazy<IAbilitySkillComposer, IAbilitySkillMetadata>> SkillComposers { get; set; }

        /// <summary>Gets or sets the skill item composers.</summary>
        [ImportMany]
        protected IEnumerable<Lazy<IAbilitySkillItemComposer, IAbilitySkillItemMetadata>> SkillItemComposers { get; set;
        }

        /// <summary>Gets or sets the unit composers.</summary>
        [ImportMany]
        protected IEnumerable<Lazy<IAbilityUnitHeroComposer, IAbilityUnitHeroMetadata>> UnitComposers { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AddSkillItemPart<T>(Func<IAbilitySkill, T> factory, params uint[] abilityIds)
            where T : IAbilitySkillPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                foreach (var skillBookAllSkill in keyValuePair.Value.SkillBook.Items)
                {
                    if (abilityIds.Contains((uint)skillBookAllSkill.Value.SourceItem.Id))
                    {
                        skillBookAllSkill.Value.AddPart(factory);
                    }
                }
            }

            foreach (var skillComposer in this.SkillItemComposers)
            {
                foreach (var abilityId in abilityIds)
                {
                    if (skillComposer.Metadata.AbilityIds.Contains((uint)abilityId))
                    {
                        skillComposer.Value.AssignPart(factory);
                    }
                }
            }
        }

        public void AddSkillPart<T>(Func<IAbilitySkill, T> factory) where T : IAbilitySkillPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                foreach (var skillBookAllSkill in keyValuePair.Value.SkillBook.AllSkills)
                {
                    skillBookAllSkill.Value.AddPart(factory);
                }
            }

            foreach (var skillComposer in this.SkillComposers)
            {
                if (skillComposer.Metadata.OwnerClassId == ClassId.CBaseEntity)
                {
                    skillComposer.Value.AssignPart(factory);
                }
            }
        }

        public void AddSkillPart<T>(Func<IAbilitySkill, T> factory, params uint[] abilityIds)
            where T : IAbilitySkillPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                foreach (var skillBookAllSkill in keyValuePair.Value.SkillBook.Spells)
                {
                    if (abilityIds.Contains((uint)skillBookAllSkill.Value.SourceAbility.Id))
                    {
                        skillBookAllSkill.Value.AddPart(factory);
                    }
                }
            }

            foreach (var skillComposer in this.SkillComposers)
            {
                foreach (var abilityId in abilityIds)
                {
                    if (skillComposer.Metadata.AbilityIds.Contains((uint)abilityId))
                    {
                        skillComposer.Value.AssignPart(factory);
                    }
                }
            }
        }

        public void AddUnitPart<T>(HeroId unitClassId, Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                var unit = keyValuePair.Value;
                if ((unit.SourceUnit as Hero).HeroId == unitClassId)
                {
                    unit.AddPart(factory);
                }
            }

            foreach (var unitComposer in this.UnitComposers)
            {
                if (unitComposer.Metadata.HeroIds.Contains((uint)unitClassId))
                {
                    unitComposer.Value.AssignPart<T>(factory);
                }
            }
        }

        public void AddUnitPart<T>(Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart
        {
            foreach (var keyValuePair in this.AbilityManager.Value.Units)
            {
                keyValuePair.Value.AddPart(factory);
            }

            foreach (var unitComposer in this.UnitComposers)
            {
                if (unitComposer.Metadata.HeroIds.Contains(uint.MaxValue))
                {
                    unitComposer.Value.AssignPart<T>(factory);
                }
            }
        }

        #endregion
    }
}