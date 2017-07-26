// <copyright file="EmberSpiritSkillBook.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.EmberSpirit.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    /// <summary>The ember spirit skill book.</summary>
    internal class EmberSpiritSkillBook : SkillBook<IAbilitySkill>
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="EmberSpiritSkillBook" /> class.</summary>
        /// <param name="unit">The unit.</param>
        internal EmberSpiritSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the flame guard.</summary>
        public IAbilitySkill FlameGuard { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add skill.</summary>
        /// <param name="skill">The skill.</param>
        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);

            if (!skill.IsItem && skill.SourceAbility.Id.Equals(AbilityId.ember_spirit_flame_guard))
            {
                this.FlameGuard = skill;
            }
        }

        #endregion
    }
}