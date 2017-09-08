// <copyright file="AbilityMapData.cs" company="EnsageSharp">
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
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Jungle;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;

    using Ensage;

    using SharpDX;

    [Export(typeof(IAbilityMapData))]
    public class AbilityMapData : IAbilityMapData
    {
        #region Public Properties

        public RuneSpawner<BountyRune> BountyRuneSpawner { get; } =
            new RuneSpawner<BountyRune>(
                new List<RunePosition<BountyRune>>
                    {
                        new RunePosition<BountyRune>(new Vector3(-2827, 4144, 300), "Dire Jungle Bounty", Team.Dire),
                        new RunePosition<BountyRune>(new Vector3(3482, 289, 300), "Dire Secret Bounty", Team.Dire),
                        new RunePosition<BountyRune>(new Vector3(1287, -4132, 300), "Radiant Jungle Bounty", Team.Radiant),
                        new RunePosition<BountyRune>(new Vector3(-4352, 194, 300), "Radiant Secret Bounty", Team.Radiant)
                    },
                0);

        public RuneSpawner<PowerUpRune> PowerUpRuneSpawner { get; } =
            new RuneSpawner<PowerUpRune>(
                new List<RunePosition<PowerUpRune>>
                    {
                        new RunePosition<PowerUpRune>(new Vector3(-1762, 1214, 150), "Top PowerUp", Team.Neutral),
                        new RunePosition<PowerUpRune>(new Vector3(2601, -2015, 150), "Bot PowerUp", Team.Neutral)
                    },
                120);

        public DireJungle DireJungle { get; } = new DireJungle();

        #endregion
    }
}