using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LaneCreep.CreepWave
{
    public interface ICreepWave : IAbilityUnitPart
    {
        Dictionary<double, IAbilityUnit> Creeps { get; }
        
        Dictionary<double, IAbilityUnit> EnemyCreeps { get; }
    }
}
