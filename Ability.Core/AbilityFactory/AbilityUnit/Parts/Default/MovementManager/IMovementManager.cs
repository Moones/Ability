using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.MovementManager
{
    using SharpDX;

    public interface IMovementManager : IAbilityUnitPart
    {
        void Move(Vector3 position);

        void MoveToTarget();

        void Bodyblock();
    }
}
