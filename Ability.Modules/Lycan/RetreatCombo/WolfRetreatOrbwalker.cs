// <copyright file="WolfRetreatOrbwalker.cs" company="EnsageSharp">
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
namespace Ability.Lycan.RetreatCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Lycan.ChaseCombo;

    public class WolfRetreatOrbwalker : WolfOrbwalker
    {
        #region Fields

        private IAbilityUnit unit1;

        #endregion

        #region Constructors and Destructors

        public WolfRetreatOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        public override IAbilityUnit Unit
        {
            get
            {
                return this.unit1;
            }

            set
            {
                this.unit1 = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override bool IssueMeanwhileActions()
        {
            return base.IssueMeanwhileActions();
        }

        public override bool PreciseIssue()
        {
            return base.PreciseIssue();
        }

        #endregion
    }
}