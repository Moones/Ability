// <copyright file="LoneDruidSkillBook.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.SkillBook
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    public class LoneDruidSkillBook : SkillBook<IAbilitySkill>
    {
        #region Constructors and Destructors

        public LoneDruidSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        public IAbilitySkill BattleCry { get; set; }

        public IAbilitySkill Rabid { get; set; }

        public IAbilitySkill SavageRoar { get; set; }

        public IAbilitySkill TrueForm { get; set; }

        public IAbilitySkill TrueFormDruid { get; set; }

        #endregion

        #region Public Methods and Operators

        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);

            switch (skill.SourceAbility.Id)
            {
                case AbilityId.lone_druid_rabid:
                    this.Rabid = skill;
                    return;
                case AbilityId.lone_druid_savage_roar:
                    this.SavageRoar = skill;
                    return;
                case AbilityId.lone_druid_true_form:
                    this.TrueForm = skill;
                    return;
                case AbilityId.lone_druid_true_form_druid:
                    this.TrueFormDruid = skill;
                    return;
                case AbilityId.lone_druid_true_form_battle_cry:
                    this.BattleCry = skill;
                    return;
            }
        }

        #endregion
    }
}