// <copyright file="AbilityUnitMeepo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Types.Meepo
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Abilities;
    using Ensage.Heroes;

    public class AbilityUnitMeepo : AbilityUnit
    {
        #region Fields

        private readonly Dictionary<double, AbilityUnitMeepo> otherMeepos = new Dictionary<double, AbilityUnitMeepo>();

        #endregion

        #region Constructors and Destructors

        internal AbilityUnitMeepo(Meepo meepo)
            : base(meepo)
        {
            this.SourceMeepo = meepo;
            this.DividedWeStand =
                this.SourceMeepo.Spellbook.Spells.FirstOrDefault(x => x is DividedWeStand) as DividedWeStand;
            this.MeepoId = this.DividedWeStand.UnitIndex + 1;
        }

        #endregion

        #region Public Properties

        public DividedWeStand DividedWeStand { get; set; }

        public int MeepoId { get; }

        public IReadOnlyDictionary<double, AbilityUnitMeepo> OtherMeepos => this.otherMeepos;

        public Meepo SourceMeepo { get; }

        #endregion

        #region Public Methods and Operators

        public override void Initialize()
        {
        }

        #endregion
    }
}