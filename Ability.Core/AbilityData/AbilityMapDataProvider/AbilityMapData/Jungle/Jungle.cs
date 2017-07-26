// <copyright file="Jungle.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Jungle
{
    using System.Collections.Generic;

    using SharpDX;

    public abstract class Jungle
    {
        #region Fields

        private readonly List<JungleCamp> ancientCamps = new List<JungleCamp>();

        private readonly List<Vector3> entrances = new List<Vector3>();

        private readonly List<JungleCamp> hardCamps = new List<JungleCamp>();

        private readonly List<JungleCamp> mediumCamps = new List<JungleCamp>();

        #endregion

        #region Public Properties

        public IReadOnlyCollection<JungleCamp> AncientCamps => this.ancientCamps;

        public JungleCamp EasyCamp { get; }

        public IReadOnlyCollection<Vector3> Entrances => this.entrances;

        public IReadOnlyCollection<JungleCamp> HardCamps => this.hardCamps;

        public IReadOnlyCollection<JungleCamp> MediumCamps => this.mediumCamps;

        #endregion
    }
}