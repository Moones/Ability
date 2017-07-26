// <copyright file="ModifierGenerator.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier;

    public class ModifierGenerator : IModifierGenerator
    {
        #region Constructors and Destructors

        public ModifierGenerator(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        public IAbilitySkill Skill { get; set; }

        public ICollection<ModifierGeneratorWorker> Workers { get; set; } = new List<ModifierGeneratorWorker>();

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            foreach (var modifierGeneratorWorker in this.Workers)
            {
                modifierGeneratorWorker.Dispose();
            }

            this.Workers.Clear();
        }

        public virtual void Initialize()
        {
        }

        public virtual bool TryGenerateModifier(IAbilityModifier modifier)
        {
            var isEnemy = modifier.AffectedUnit.Team != this.Skill.Owner.Team;
            foreach (var modifierGeneratorWorker in this.Workers)
            {
                if ((!isEnemy && modifierGeneratorWorker.AffectsAllies
                     || isEnemy && modifierGeneratorWorker.AffectsEnemies || modifierGeneratorWorker.AffectsEveryone
                     || !modifier.AffectedUnit.UnitHandle.Equals(this.Skill.Owner.UnitHandle)
                     && modifierGeneratorWorker.AffectsSelf || modifierGeneratorWorker.AffectsSelf)
                    && modifierGeneratorWorker.ModifierName.Equals(modifier.Name))
                {
                    modifier.SourceSkill = this.Skill;
                    modifierGeneratorWorker.GenerateModifier(modifier);
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}