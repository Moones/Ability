// <copyright file="Pathfinder.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Pathfinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    public class Pathfinder : IPathfinder
    {
        #region Constructors and Destructors

        public Pathfinder(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public NavMeshPathfinding EnsagePathfinding { get; } = new NavMeshPathfinding();

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public void Initialize()
        {
        }

        public float PathDistance(Vector3 position)
        {
            bool completed;
            var path = this.EnsagePathfinding.CalculateStaticLongPath(
                this.Unit.Position.PredictedByLatency,
                position,
                20000,
                true,
                out completed);
            if (!completed)
            {
                Console.WriteLine("Path not completed");
                return 0;
            }

            var totalDistance = 0f;
            var lastPosition = this.Unit.Position.PredictedByLatency;
            foreach (var position1 in path)
            {
                totalDistance += lastPosition.Distance2D(position1);
                lastPosition = position1;
            }

            return totalDistance;
        }

        public float PathDistance(Vector3 position, List<Vector3> path)
        {
            var totalDistance = 0f;
            var lastPosition = this.Unit.Position.PredictedByLatency;
            foreach (var position1 in path)
            {
                totalDistance += lastPosition.Distance2D(position1);
                lastPosition = position1;
            }

            return totalDistance;
        }

        public float PathDistance(Vector3 position, out List<Vector3> path)
        {
            bool completed;
            var path1 = this.EnsagePathfinding.CalculateStaticLongPath(
                this.Unit.Position.PredictedByLatency,
                position,
                20000,
                true,
                out completed);
            var position1s = path1.ToList();
            if (!completed)
            {
                Console.WriteLine("Path not completed");
                path = position1s;
                return 0;
            }

            var totalDistance = 0f;
            var lastPosition = this.Unit.Position.PredictedByLatency;
            foreach (var position1 in position1s)
            {
                totalDistance += lastPosition.Distance2D(position1);
                lastPosition = position1;
            }

            path = position1s;
            return totalDistance;
        }

        #endregion
    }
}