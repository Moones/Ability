// <copyright file="NecroArcherUnitComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.NecroArcher
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.NecroArcher.UnitCombo;

    [Export(typeof(IAbilityUnitComposer))]
    [AbilityUnitMetadata("npc_dota_necronomicon_archer_1", "npc_dota_necronomicon_archer_2",
        "npc_dota_necronomicon_archer_3")]
    internal class NecroArcherUnitComposer : AbilityUnitComposer
    {
        #region Constructors and Destructors

        internal NecroArcherUnitComposer()
        {
            this.AssignControllablePart<IUnitCombo>(unit => new NecroArcherUnitCombo(unit));
        }

        #endregion
    }
}