// <copyright file="ManaBurnCastingFunction.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.UnitParts.Necronomicon.ManaBurn.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.UnitParts.Necronomicon.ManaBurn.DamageCalculator;

    public class ManaBurnCastingFunction : DefaultCastingFunction
    {
        #region Fields

        private ManaBurnDamageCalculator damageCalculator;

        #endregion

        #region Constructors and Destructors

        public ManaBurnCastingFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override bool CanCast()
        {
            // Console.WriteLine(this.Skill.Owner.TargetSelector.Target.Mana.Current + 70 > this.damageCalculator.ManaBurnValue);
            return base.CanCast()
                   && this.Skill.Owner.TargetSelector.Target.Mana.Current + 70 > this.damageCalculator.ManaBurnValue;
        }

        public override void Initialize()
        {
            base.Initialize();

            this.damageCalculator = this.Skill.DamageCalculator as ManaBurnDamageCalculator;
        }

        #endregion
    }
}