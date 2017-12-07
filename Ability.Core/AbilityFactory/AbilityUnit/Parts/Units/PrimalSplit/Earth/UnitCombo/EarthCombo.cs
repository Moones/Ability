// <copyright file="EarthCombo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.PrimalSplit.Earth.UnitCombo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;

    using Ensage;

    public class EarthCombo : UnitComboNoItems
    {
        #region Constructors and Destructors

        public EarthCombo(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <exception cref="NullReferenceException">Happens at the end of game where aghanim is deleted.</exception>
        public override void Initialize()
        {
            this.DisableEntries = new List<ComboEntry>
                                      {
                                          new ComboEntry(
                                              this.Unit.SkillBook.AllSkills.FirstOrDefault(
                                                  x =>
                                                      x.Value.SourceAbility.Id
                                                      == AbilityId.brewmaster_earth_hurl_boulder).Value)
                                      };

            if (this.Unit.Owner.SkillBook.HasAghanim)
            {
                this.DisableEntries.Add(
                    new ComboEntry(
                        this.Unit.SkillBook.AllSkills.FirstOrDefault(
                            x => x.Value.SourceAbility.Id == AbilityId.brewmaster_thunder_clap).Value));
            }
        }

        #endregion
    }
}