// <copyright file="StormCombo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.PrimalSplit.Storm.UnitCombo
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;

    using Ensage;

    public class StormCombo : UnitComboNoItems
    {
        #region Constructors and Destructors

        public StormCombo(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override void Initialize()
        {
            // Console.WriteLine("allspells");
            // foreach (var skillBookAllSkill in this.Unit.SourceUnit.Spellbook.Spells)
            // {
            // Console.WriteLine(
            // skillBookAllSkill.Name + " " + skillBookAllSkill.Id + " " + AbilityId.brewmaster_storm_wind_walk);
            // }
            this.Entries = new List<ComboEntry>
                               {
                                   new ComboEntry(
                                       this.Unit.SkillBook.AllSkills.FirstOrDefault(
                                               x => x.Value.SourceAbility.Id == AbilityId.brewmaster_storm_dispel_magic)
                                           .Value),
                                   new ComboEntry(
                                       this.Unit.SkillBook.AllSkills.FirstOrDefault(
                                           x => x.Value.SourceAbility.Id == AbilityId.brewmaster_storm_wind_walk).Value)
                               };

            if (this.Unit.Owner.SkillBook.HasAghanim)
            {
                this.DisableEntries.Add(
                    new ComboEntry(
                        this.Unit.SkillBook.AllSkills.FirstOrDefault(
                            x => x.Value.SourceAbility.Id == AbilityId.brewmaster_drunken_haze).Value));
            }
        }

        #endregion
    }
}