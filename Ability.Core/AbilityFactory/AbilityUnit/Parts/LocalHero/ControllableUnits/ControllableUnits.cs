// <copyright file="ControllableUnits.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.Utilities;

    public class ControllableUnits : IControllableUnits
    {
        #region Constructors and Destructors

        public ControllableUnits(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public DataProvider<IAbilityUnit> ControllableUnitAdded { get; } = new DataProvider<IAbilityUnit>();

        public DataProvider<IAbilityUnit> ControllableUnitRemoved { get; } = new DataProvider<IAbilityUnit>();

        public Dictionary<double, IAbilityUnit> Units { get; } = new Dictionary<double, IAbilityUnit>();

        public DataProvider<IAbilityUnit> AddedUnit { get; } = new DataProvider<IAbilityUnit>();

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
        }

        public DataProvider<IAbilityUnit> RemovedUnit { get; } = new DataProvider<IAbilityUnit>();

        public virtual void UnitAdded(IAbilityUnit unit)
        {
            Console.WriteLine("added controllable unit " + unit.Name);
            unit.Owner = this.Unit;
            this.Units.Add(unit.UnitHandle, unit);
            this.AddedUnit.Next(unit);
        }

        public virtual void UnitRemoved(IAbilityUnit unit)
        {
            this.Units.Remove(unit.UnitHandle);
            this.RemovedUnit.Next(unit);
        }

        #endregion
    }
}