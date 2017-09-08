using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Types.LaneCreep
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.LaneCreep.CreepWave;

    using Ensage;

    public class LaneCreep : AbilityUnit
    {
        public LaneCreep(Unit unit)
            : base(unit)
        {
        }

        public ICreepWave CreepWave { get; set; }
    }
}
