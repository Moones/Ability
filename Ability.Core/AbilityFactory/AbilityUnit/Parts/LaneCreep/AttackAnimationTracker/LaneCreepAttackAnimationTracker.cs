// <copyright file="LaneCreepAttackAnimationTracker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LaneCreep.AttackAnimationTracker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Types.LaneCreep;

    using Ensage.Common.Extensions;

    public class LaneCreepAttackAnimationTracker : AttackAnimationTracker
    {
        #region Constructors and Destructors

        public LaneCreepAttackAnimationTracker(IAbilityUnit unit)
            : base(unit)
        {
            this.LaneCreep = this.Unit as LaneCreep;
        }

        #endregion

        #region Public Properties

        public LaneCreep LaneCreep { get; }

        public IAbilityUnit PossibleTarget { get; set; }

        #endregion

        #region Public Methods and Operators

        public override void AttackStarted()
        {
            base.AttackStarted();

            this.PossibleTarget =
                this.LaneCreep.CreepWave.EnemyCreeps.MinOrDefault(
                    x => this.LaneCreep.SourceUnit.FindRelativeAngle(x.Value.Position.Current)).Value;

            // this.PossibleTarget
        }

        #endregion
    }
}