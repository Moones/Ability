// <copyright file="IAbilityRune.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public interface IAbilityRune : IDisposable
    {
        #region Public Properties

        double Handle { get; }

        string Name { get; }

        float PickUpRange { get; }

        Notifier RuneDisposed { get; }

        Rune SourceRune { get; }

        bool Disposed { get; set; }

        string TypeName { get; }

        #endregion
    }
}