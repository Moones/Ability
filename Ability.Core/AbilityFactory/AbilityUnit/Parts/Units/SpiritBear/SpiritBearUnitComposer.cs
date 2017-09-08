// <copyright file="SpiritBearUnitComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;

    [Export(typeof(IAbilityUnitComposer))]
    [AbilityUnitMetadata("npc_dota_lone_druid_bear1", "npc_dota_lone_druid_bear2", "npc_dota_lone_druid_bear3",
        "npc_dota_lone_druid_bear4")]
    internal class SpiritBearUnitComposer : AbilityUnitComposer
    {
        #region Constructors and Destructors

        public SpiritBearUnitComposer()
        {
            this.AssignPart<ISkillBook<IAbilitySkill>>(unit => new SpiritBearSkillBook(unit));
            this.AssignControllablePart<IUnitCombo>(unit => new UnitCombo(unit));
        }

        #endregion
    }
}