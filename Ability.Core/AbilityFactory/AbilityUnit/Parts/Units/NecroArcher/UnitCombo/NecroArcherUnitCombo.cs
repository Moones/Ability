// <copyright file="NecroArcherUnitCombo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.NecroArcher.UnitCombo
{
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;

    using Ensage;

    public class NecroArcherUnitCombo : UnitCombo
    {
        #region Fields

        public ComboEntry ManaBurn;

        #endregion

        #region Constructors and Destructors

        public NecroArcherUnitCombo(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override bool CastAllSpellsOnTarget()
        {
            return this.ManaBurn.Cast();
        }

        public override void Initialize()
        {
            this.ManaBurn =
                new ComboEntry(
                    this.Unit.SkillBook.AllSkills.FirstOrDefault(
                        x => x.Value.SourceAbility.Id == AbilityId.necronomicon_archer_mana_burn).Value);
        }

        #endregion
    }
}