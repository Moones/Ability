using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Pathfinder
{
    using Ensage;

    using SharpDX;

    public interface IPathfinder : IAbilityUnitPart
    {
        float PathDistance(Vector3 position);
        float PathDistance(Vector3 position, List<Vector3> path);

        NavMeshPathfinding EnsagePathfinding { get; }

        float PathDistance(Vector3 position, out List<Vector3> path);
    }
}
