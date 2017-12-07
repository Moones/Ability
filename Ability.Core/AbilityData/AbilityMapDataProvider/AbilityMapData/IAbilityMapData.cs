// <copyright file="IAbilityMapData.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Jungle;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;

    public interface IAbilityMapData
    {
        #region Public Properties

        /// <summary>Gets the bounty rune spawner.</summary>
        RuneSpawner<BountyRune> BountyRuneSpawner { get; }

        DireJungle DireJungle { get; }

        /// <summary>Gets the power up rune spawner.</summary>
        RuneSpawner<PowerUpRune> PowerUpRuneSpawner { get; }

        #endregion
    }
}