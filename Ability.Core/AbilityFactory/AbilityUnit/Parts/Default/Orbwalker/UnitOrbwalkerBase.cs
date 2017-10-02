// <copyright file="UnitOrbwalkerBase.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;

    using SharpDX;

    // [InheritedExport(typeof(IUnitOrbwalker))]
    public abstract class UnitOrbwalkerBase : IUnitOrbwalker
    {
        #region Fields

        private bool afterAttackExecuted;

        private bool beforeAttackExecuted;

        private Sleeper issueSleeper = new Sleeper();

        private IAbilityUnit target;

        private Reacter targetReset;

        private IAbilityUnit unit;

        private bool enabled;

        #endregion

        #region Constructors and Destructors

        protected UnitOrbwalkerBase(IAbilityUnit unit)
        {
            this.targetReset = new Reacter(
                () =>
                    {
                        this.TargetValid = this.IsTargetValid();
                        this.MeanWhile = false;
                        this.MoveToAttack = false;
                    });

            this.Bodyblocker = new UnitBodyblocker(unit);
            this.Unit = unit;
        }

        protected UnitOrbwalkerBase()
        {
            this.Bodyblocker = new UnitBodyblocker();
            this.targetReset = new Reacter(
                () =>
                    {
                        this.TargetValid = this.IsTargetValid();
                        this.MeanWhile = false;
                        this.MoveToAttack = false;
                    });
        }

        #endregion

        #region Public Properties

        public bool Attacking { get; set; }

        public UnitBodyblocker Bodyblocker { get; }

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                this.enabled = value;
                if (this.enabled)
                {
                    this.Initialize();
                }
                else
                {
                    this.Dispose();
                }
            }
        }

        public uint Id { get; set; }

        public float IssueSleep { get; set; } = 120;

        public float MaxTargetDistance { get; set; } = 1000;

        public bool MeanWhile { get; set; }

        public bool MoveToAttack { get; set; }

        public double NextAttack { get; set; }

        public IAbilityUnit Target => this.Unit.TargetSelector.Target;

        public bool TargetValid { get; set; }

        public float Time { get; set; }

        public virtual IAbilityUnit Unit
        {
            get
            {
                return this.unit;
            }

            set
            {
                this.unit = value;
                this.Bodyblocker.Unit = this.unit;
                this.targetReset.Dispose();
                this.targetReset.Subscribe(this.unit.TargetSelector.TargetChanged);

                // this.unit.AddOrderIssuer(this);
            }
        }

        #endregion

        #region Public Methods and Operators

        public virtual bool AfterAttack()
        {
            return this.Move();
        }

        public virtual bool Attack()
        {
            this.Unit.OrderQueue.EnqueueOrder(new Attack(this.Unit));
            return true;
        }

        public virtual bool BeforeAttack()
        {
            return this.Attack();
        }

        public bool Bodyblock()
        {
            return this.Bodyblocker.Bodyblock() || this.Attack();
        }

        public virtual bool CantAttack()
        {
            return this.MoveToMouse();
        }

        public virtual bool CastSpells()
        {
            return false;
        }

        public virtual void Dispose()
        {
            this.beforeAttackExecuted = false;
            this.afterAttackExecuted = false;
        }

        public virtual void Initialize()
        {
            this.beforeAttackExecuted = false;
            this.afterAttackExecuted = false;
        }

        public virtual bool Issue()
        {
            if (!this.Enabled || this.issueSleeper.Sleeping)
            {
                return false;
            }

            if (this.IssueMeanwhileActions())
            {
                this.issueSleeper.Sleep(this.IssueSleep);
                return true;
            }

            return false;
        }

        public virtual bool IssueMeanwhileActions()
        {
            if (!this.TargetValid)
            {
                return this.NoTarget();
            }

            //if (this.beforeAttackExecuted && !this.Attacking)
            //{
            //    return this.Attack();
            //}



            if (this.MoveToAttack)
            {
                if (this.CastSpells())
                {
                    return true;
                }

                this.MoveBeforeAttack();
                return true;
            }

            return this.MeanWhile && (this.CastSpells() || this.Meanwhile());
        }

        public virtual bool IsTargetValid()
        {
            return this.Unit.TargetSelector.TargetIsSet && this.Target.SourceUnit.IsValid && this.Target.SourceUnit.IsAlive
                   && this.Target.Visibility.Visible
                   && this.Unit.TargetSelector.LastDistanceToTarget < this.MaxTargetDistance + this.Unit.AttackRange.Value
                   && this.Target.Modifiers.Attackable;
        }

        public bool KeepRange()
        {
            var difference = this.Unit.AttackRange.Value - this.Unit.TargetSelector.LastDistanceToTarget
                             + this.Unit.SourceUnit.HullRadius + this.Target.SourceUnit.HullRadius;
            var mousDistance = Game.MousePosition.Distance2D(this.Target.Position.Current);
            if (difference > 0)
            {
                // var targetDistance = this.Unit.TargetSelector.LastDistanceToTarget;
                if (mousDistance + 350
                    < this.Unit.AttackRange.Value + this.Unit.SourceUnit.HullRadius + this.Target.SourceUnit.HullRadius)
                {
                    if (this.Target.SourceUnit.NetworkActivity == NetworkActivity.Move)
                    {
                        if (this.Target.Position.PredictedByLatency.Distance2D(this.Unit.Position.Current)
                            > this.Target.Position.Current.Distance2D(this.Unit.Position.Current))
                        {
                            return this.Move();
                        }
                        else
                        {
                            return
                                this.Unit.SourceUnit.Move(
                                    this.Target.Position.PredictedByLatency.Extend(
                                        Game.MousePosition,
                                        Math.Max(this.Unit.AttackRange.Value * 0.8f, mousDistance)));
                        }
                    }

                    return true;
                }
                else
                {
                    return this.Unit.SourceUnit.Move(Game.MousePosition);
                }
            }

            return this.Unit.SourceUnit.Move(Game.MousePosition);
        }

        public virtual bool Meanwhile()
        {
            return this.Move();
        }

        public virtual bool Move()
        {
            return this.MoveToMouse();
        }

        public virtual void MoveBeforeAttack()
        {
            this.Move();
        }

        public bool MoveToMouse()
        {
            return this.Unit.SourceUnit.Move(Game.MousePosition);
        }

        public virtual bool NoTarget()
        {
            return this.MoveToMouse();
        }

        public virtual bool PreciseIssue()
        {
            if (!this.Enabled)
            {
                return false;
            }

            this.TargetValid = this.IsTargetValid();

            if (!this.TargetValid)
            {
                this.MeanWhile = true;
                return false;
            }

            this.Time = GlobalVariables.Time * 1000 + Game.Ping;
            this.NextAttack = this.Time - this.Unit.AttackAnimationTracker.NextAttackTime
                              + this.Unit.TurnRate.GetTurnTime(this.Target) * 1000;

            // Console.WriteLine(time + " " + Game.Ping + " " + this.NextAttackTime + " " + (this.Unit.SourceUnit.GetTurnTime(this.Target.SourceUnit) * 1000));
            if (this.NextAttack < 0)
            {
                this.MoveToAttack = false;
                this.beforeAttackExecuted = false;
                if (this.afterAttackExecuted)
                {
                    this.MeanWhile = true;

                    // this.Unit.Target = this.Unit.TargetSelector.GetTarget();
                    return false;
                }

                this.afterAttackExecuted = true;
                return this.CastSpells() || this.AfterAttack();
            }
            else
            {
                // Console.WriteLine(
                // this.Unit.TurnRate.GetTurnTime(this.Target) + " " + this.Unit.AttackAnimation.GetAttackRate() + " "
                // + this.Unit.SourceUnit.AttackRate());
                this.afterAttackExecuted = false;
                if (this.beforeAttackExecuted)
                {
                    return false;
                }

                if (this.Unit.Modifiers.Disarmed || this.Unit.Modifiers.Immobile || this.Target.Modifiers.AttackImmune
                    || this.Target.Modifiers.Invul || !this.Target.Visibility.Visible)
                {
                    this.MeanWhile = true;
                    return false;
                }

                this.MeanWhile = false;
                if (this.CastSpells())
                {
                    this.MoveToAttack = false;
                    return true;
                }

                if (this.Unit.AttackRange.IsInAttackRange(this.Target) && this.BeforeAttack())
                {
                    this.MoveToAttack = false;
                    this.beforeAttackExecuted = true;
                    return true;
                }

                this.MoveToAttack = true;
                return false;
            }

            this.MeanWhile = false;
            return false;
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

        #endregion
    }
}