using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.MovementTracker
{
    using SharpDX;

    public interface IMovementTracker : IAbilityUnitPart
    {
        float IdleTime();
        float StayTime();

        float StraightTime();

        Vector3 AverageDirection();
    }
}
