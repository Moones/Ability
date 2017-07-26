// <copyright file="IUnitOrbwalker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;

    public interface IUnitOrbwalker : IOrderIssuer
    {
        #region Public Properties

        bool Enabled { get; set; }

        IAbilityUnit Target { get; }

        #endregion

        #region Public Methods and Operators

        bool AfterAttack();

        bool BeforeAttack();

        bool CantAttack();

        bool Meanwhile();

        bool NoTarget();

        #endregion
    }
}