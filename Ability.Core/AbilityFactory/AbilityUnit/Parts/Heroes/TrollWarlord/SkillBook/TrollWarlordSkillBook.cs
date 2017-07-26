// <copyright file="TrollWarlordSkillBook.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.TrollWarlord.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    public class TrollWarlordSkillBook : SkillBook<IAbilitySkill>
    {
        #region Constructors and Destructors

        public TrollWarlordSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        public IAbilitySkill BattleTrance { get; set; }

        public IAbilitySkill BerserkersRage { get; set; }

        #endregion
    }
}