// <copyright file="ManaBurnDamageCalculator.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.UnitParts.Necronomicon.ManaBurn.DamageCalculator
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator;

    using Ensage.Common.Extensions;

    public class ManaBurnDamageCalculator : SkillDamageCalculator
    {
        #region Constructors and Destructors

        public ManaBurnDamageCalculator(IAbilitySkill skill)
            : base(skill)
        {
        }

        #endregion

        #region Public Properties

        public float ManaBurnValue { get; set; }

        #endregion

        #region Public Methods and Operators

        public override void Initialize()
        {
            base.Initialize();

            this.ManaBurnValue = this.Skill.SourceAbility.GetAbilityData("burn_amount");

            this.RawDamageWorkerAssign =
                unit =>
                    new ManaBurnDamageCalculatorWorker(
                        this.Skill,
                        unit,
                        this.DamageWorkerAssign(unit),
                        this.ManaBurnValue);
        }

        #endregion
    }
}