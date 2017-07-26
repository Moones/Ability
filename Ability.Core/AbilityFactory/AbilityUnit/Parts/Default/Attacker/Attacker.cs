// <copyright file="Attacker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Attacker
{
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public class Attacker : IAttacker
    {
        #region Public Properties

        public Notifier AttackOrderSent { get; } = new Notifier();

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Attack(IAbilityUnit target)
        {
            this.Attack(target.SourceUnit);
        }

        public void Attack(Unit unit)
        {
            this.AttackOrderSent.Notify();
            this.Unit.SourceUnit.Attack(unit);
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {
        }

        #endregion
    }
}