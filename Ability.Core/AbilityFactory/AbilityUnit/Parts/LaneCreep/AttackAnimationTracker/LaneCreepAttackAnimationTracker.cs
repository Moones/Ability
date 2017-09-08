using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LaneCreep.AttackAnimationTracker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Types.LaneCreep;

    using Ensage.Common.Extensions;

    public class LaneCreepAttackAnimationTracker : AttackAnimationTracker
    {
        public LaneCreepAttackAnimationTracker(IAbilityUnit unit)
            : base(unit)
        {
            this.LaneCreep = this.Unit as LaneCreep;
        }

        public LaneCreep LaneCreep { get; }

        public IAbilityUnit PossibleTarget { get; set; }

        public override void AttackStarted()
        {
            base.AttackStarted();

            this.PossibleTarget =
                this.LaneCreep.CreepWave.EnemyCreeps.MinOrDefault(
                    x => this.LaneCreep.SourceUnit.FindRelativeAngle(x.Value.Position.Current)).Value;

            //this.PossibleTarget
        }
    }
}
