// <copyright file="RuneSpawner.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common.Extensions;

    public class RuneSpawner<T>
        where T : IAbilityRune
    {
        #region Fields

        private readonly float firstSpawnTime;

        private readonly List<RunePosition<T>> positions = new List<RunePosition<T>>();

        private float lastSpawnTime;

        private float nextSpawnTime;

        #endregion

        #region Constructors and Destructors

        public RuneSpawner(List<RunePosition<T>> positions, float firstSpawnTime)
        {
            this.positions = positions;
            this.firstSpawnTime = firstSpawnTime;
        }

        #endregion

        #region Public Properties

        public DataProvider<T> NewRuneProvider { get; } = new DataProvider<T>();

        public float NextSpawnTime
        {
            get
            {
                return this.nextSpawnTime;
            }

            set
            {
                this.nextSpawnTime = value;
                foreach (var runePosition in this.Positions)
                {
                    runePosition.NextSpawnTime = this.nextSpawnTime;
                }
            }
        }

        public IReadOnlyCollection<RunePosition<T>> Positions => this.positions;

        #endregion

        #region Methods

        internal void Draw()
        {
            foreach (var runePosition in this.Positions)
            {
                runePosition.Draw();
            }
        }

        internal void NewRune(T rune)
        {
            var closest = this.positions.MinOrDefault(position => position.Position.Distance(rune.SourceRune.Position));
            closest.CurrentRune = rune;
            this.NewRuneProvider.Next(closest.CurrentRune);

            // foreach (var runePosition in this.Positions)
            // {
            // if (runePosition.Position.Distance(rune.SourceRune.Position) < 100)
            // {
            // runePosition.CurrentRune = rune;
            // break;
            // }
            // }
        }

        internal void UpdateTime(float gameTime)
        {
            if (gameTime > this.firstSpawnTime)
            {
                this.lastSpawnTime = this.firstSpawnTime;
                for (var i = this.firstSpawnTime; i <= gameTime; i += 120)
                {
                    this.lastSpawnTime = i;
                }

                this.NextSpawnTime = this.lastSpawnTime + 120;
            }
            else
            {
                this.NextSpawnTime = this.firstSpawnTime;
            }
        }

        #endregion
    }
}