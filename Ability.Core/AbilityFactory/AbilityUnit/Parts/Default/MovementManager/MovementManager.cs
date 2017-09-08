using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.MovementManager
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;

    using SharpDX;

    public class MovementManager : IMovementManager
    {
        public MovementManager(IAbilityUnit unit)
        {
            this.Unit = unit;
            this.Bodyblocker = new UnitBodyblocker(unit);
        }

        public void Dispose()
        {
        }

        public UnitBodyblocker Bodyblocker { get; set; }

        public IAbilityUnit Unit { get; set; }

        public void Initialize()
        {
        }

        public void Move(Vector3 position)
        {
            if (this.Unit.TargetSelector.TargetIsSet)
            {
                if (this.RunAround(this.Unit.Owner, this.Unit.TargetSelector.Target)
                    || this.Unit.Owner.ControllableUnits.Units.Any(
                        x => this.RunAround(x.Value, this.Unit.TargetSelector.Target)))
                {
                    return;
                }

                this.Bodyblock();
            }

            this.Unit.SourceUnit.Move(Game.MousePosition);
            return;
        }

        public void MoveToTarget()
        {
            if (this.Unit.TargetSelector.TargetIsSet)
            {
                if (this.RunAround(this.Unit.Owner, this.Unit.TargetSelector.Target)
                    || this.Unit.Owner.ControllableUnits.Units.Any(
                        x => this.RunAround(x.Value, this.Unit.TargetSelector.Target)))
                {
                    return;
                }

                this.Bodyblock();
            }

            this.Unit.SourceUnit.Move(Game.MousePosition);
        }

        public void Bodyblock()
        {
            this.Bodyblocker.Bodyblock();
        }

        public bool RunAround(IAbilityUnit unit, IAbilityUnit targetUnit)
        {
            var targetPosition = unit.Position.PredictedByLatency;

            if (targetUnit.Position.Current.Distance2D(targetPosition) < 200)
            {
                return false;
            }

            return this.RunAround(unit, targetUnit.Position.PredictedByLatency);
        }

        public bool RunAround(IAbilityUnit unit, Vector3 target)
        {
            if (this.Unit.SourceUnit.MovementSpeed <= unit.SourceUnit.MovementSpeed)
            {
                return false;
            }

            var unitPosition = this.Unit.Position.PredictedByLatency;
            var targetPosition = unit.Position.PredictedByLatency;

            if (target.Distance2D(targetPosition) < 200)
            {
                this.Unit.SourceUnit.Move(unit.SourceUnit.InFront(250));
                return true;
            }

            if (this.Unit.Position.PredictedByLatency.Distance2D(target)
                < unit.Position.PredictedByLatency.Distance2D(target)
                || this.Unit.Position.PredictedByLatency.Distance2D(unit.Position.PredictedByLatency) > 250)
            {
                return false;
            }

            var infront = unit.SourceUnit.InFront(500);

            // infront =
            // this.Target.SourceUnit.InFront(
            // (this.Unit.Position.Current.Distance2D(infront) / this.Unit.SourceUnit.MovementSpeed)
            // * this.Target.SourceUnit.MovementSpeed);
            var backWardsdirection = (targetPosition - infront).Normalized();
            var projectionInfo = Vector3Extensions.ProjectOn(targetPosition, unitPosition, infront);
            var projectionInfo2 = Vector3Extensions.ProjectOn(
                unitPosition,
                targetPosition + backWardsdirection * (unitPosition.Distance2D(targetPosition) + 200),
                infront);
            var isCloserToFront = unitPosition.Distance2D(infront) + this.Unit.SourceUnit.HullRadius
                                  + unit.SourceUnit.HullRadius + 100 < targetPosition.Distance2D(infront);
            var distanceFromSegment2 = unitPosition.Distance2D(
                Vector2Extensions.ToVector3(projectionInfo2.SegmentPoint));
            var canBlock = (projectionInfo2.IsOnSegment || distanceFromSegment2 < unit.SourceUnit.HullRadius / 2)
                           && isCloserToFront;
            if (!canBlock
                && (projectionInfo.IsOnSegment
                    || targetPosition.Distance2D(Vector2Extensions.ToVector3(projectionInfo.SegmentPoint))
                    < unit.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 20))
            {
                var direction = (infront - targetPosition).Normalized();
                var direction1 = (infront - targetPosition).Perpendicular().Normalized();
                var direction2 = (targetPosition - infront).Perpendicular().Normalized();

                // Console.WriteLine(direction1 + " " + direction2);
                var position = Pathfinding.ExtendUntilWall(
                    targetPosition,
                    direction1,
                    Math.Max(unit.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 100, distanceFromSegment2),
                    this.Unit.Pathfinder.EnsagePathfinding);

                var position2 = Pathfinding.ExtendUntilWall(
                    targetPosition,
                    direction2,
                    Math.Max(unit.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 100, distanceFromSegment2),
                    this.Unit.Pathfinder.EnsagePathfinding);

                var distance = unitPosition.Distance2D(position);
                var distance2 = unitPosition.Distance2D(position2);
                if (distance2 < distance)
                {
                    distance = distance2;
                    position = position2;
                }

                if (!isCloserToFront)
                {
                    if (distanceFromSegment2 < unit.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 50)
                    {
                        infront = Pathfinding.ExtendUntilWall(position, direction, distance + 500, this.Unit.Pathfinder.EnsagePathfinding);
                    }
                    else
                    {
                        infront = Pathfinding.ExtendUntilWall(unitPosition, direction, 500, this.Unit.Pathfinder.EnsagePathfinding);
                    }
                }

                // else
                // {
                // infront = Pathfinding.ExtendUntilWall(unitPosition, direction, 300, this.Pathfinder);
                // }
            }
            else
            {
                return false;
            }

            this.Unit.SourceUnit.Move(infront);
            return true;
        }
    }
}
