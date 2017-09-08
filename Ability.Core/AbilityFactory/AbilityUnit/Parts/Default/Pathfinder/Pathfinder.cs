using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Pathfinder
{
    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    public class Pathfinder : IPathfinder
    {
        public Pathfinder(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

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

        public NavMeshPathfinding EnsagePathfinding { get; } = new NavMeshPathfinding();
    }
}
