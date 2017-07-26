// <copyright file="UnitBodyblocker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker
{
    using System;

    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;

    using SharpDX;

    public class UnitBodyblocker : IUnitBodyblocker
    {
        #region Fields

        private Sleeper issueSleeper = new Sleeper();

        private double lastRad;

        private bool moveOrderWasSent;

        private bool stopped;

        private Vector3 targetPosition;

        private bool targetWasMoving;

        private bool wasMoving;

        #endregion

        #region Constructors and Destructors

        public UnitBodyblocker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public UnitBodyblocker()
        {
        }

        #endregion

        #region Public Properties

        public bool Bodyblocking { get; set; }

        public bool Enabled { get; set; }

        public uint Id { get; set; }

        public float IssueSleep { get; set; } = 120;

        public float MaxTargetDistance { get; set; } = 2000;

        public IAbilityUnit Target => this.Unit.TargetSelector.Target;

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public bool Bodyblock()
        {
            var unitPosition = this.Unit.Position.PredictedByLatency;
            this.targetPosition = this.Target.Position.PredictedByLatency;
            var infront = this.Target.SourceUnit.InFront(1000);

            // infront =
            // this.Target.SourceUnit.InFront(
            // (this.Unit.Position.Current.Distance2D(infront) / this.Unit.SourceUnit.MovementSpeed)
            // * this.Target.SourceUnit.MovementSpeed);
            var backWardsdirection = (this.targetPosition - infront).Normalized();
            var projectionInfo = Vector3Extensions.ProjectOn(this.targetPosition, unitPosition, infront);
            var projectionInfo2 = Vector3Extensions.ProjectOn(
                unitPosition,
                this.targetPosition + backWardsdirection * (unitPosition.Distance2D(this.targetPosition) + 200),
                infront);
            var distanceFromSegment2 =
                this.Unit.Position.Current.Distance2D(Vector2Extensions.ToVector3(projectionInfo2.SegmentPoint));
            var isCloserToFront = projectionInfo2.SegmentPoint.Distance(infront) + this.Unit.SourceUnit.HullRadius
                                  + this.Target.SourceUnit.HullRadius + 50 < this.targetPosition.Distance2D(infront)
                                  && unitPosition.Distance2D(infront) < this.targetPosition.Distance2D(infront);
            var angle = this.Unit.SourceUnit.FindRelativeAngle(infront);

            // Console.WriteLine(angle + " " + Math.PI / 4);
            var goodposition = projectionInfo2.IsOnSegment && isCloserToFront;
            this.wasMoving = this.Unit.SourceUnit.NetworkActivity == NetworkActivity.Move;
            this.targetWasMoving = this.Target.SourceUnit.NetworkActivity == NetworkActivity.Move;
            this.lastRad = this.Target.SourceUnit.RotationRad;
            var idleTime = this.Target.MovementTracker.IdleTime();
            if (isCloserToFront && idleTime < 1)
            {
                if (this.Unit.TargetSelector.LastDistanceToTarget
                    > this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius
                    + this.Target.SourceUnit.MovementSpeed * (Game.Ping / 1000) * 2 + 200 || angle > Math.PI / 6
                    || !goodposition)
                {
                    // Console.WriteLine("1");
                    this.moveOrderWasSent = true;
                    this.Bodyblocking = true;
                    return
                        this.Unit.SourceUnit.Move(
                            this.Target.SourceUnit.InFront(
                                this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 150
                                + this.Target.SourceUnit.MovementSpeed * (Game.Ping / 1000) * 2));
                }
            }

            if (goodposition)
            {
                if ((this.wasMoving || this.moveOrderWasSent) && this.Target.SourceUnit.CanMove() && idleTime < 1)
                {
                    // Console.WriteLine("2");
                    this.moveOrderWasSent = false;
                    this.issueSleeper.Sleep(
                        this.IssueSleep
                        + (this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius * 2)
                        / this.Target.SourceUnit.MovementSpeed * 1000);
                    this.Bodyblocking = true;
                    return this.Stop();
                }

                if (!this.Target.SourceUnit.CanMove() || idleTime >= 1)
                {
                    // Console.WriteLine("3");
                    this.moveOrderWasSent = false;
                    return this.CantMove();
                }

                // Console.WriteLine("5");
                this.moveOrderWasSent = true;
                this.issueSleeper.Sleep(
                    this.IssueSleep
                    + (this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius)
                    / this.Target.SourceUnit.MovementSpeed * 1000);

                this.Bodyblocking = true;
                return
                    this.Unit.SourceUnit.Move(
                        this.Target.SourceUnit.InFront(
                            this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 200
                            + this.Target.SourceUnit.MovementSpeed * (Game.Ping / 1000) * 2));
                return this.Stop();

                // this.moveOrderWasSent = false;
                return false;
            }

            // Console.WriteLine("4");
            var canBlock = (this.wasMoving || this.moveOrderWasSent) && goodposition;
            if (!canBlock
                && (projectionInfo.IsOnSegment
                    || this.targetPosition.Distance2D(Vector2Extensions.ToVector3(projectionInfo.SegmentPoint))
                    < this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 20))
            {
                var direction = (infront - this.targetPosition).Normalized();
                var direction1 = (infront - this.targetPosition).Perpendicular().Normalized();
                var direction2 = (this.targetPosition - infront).Perpendicular().Normalized();

                // Console.WriteLine(direction1 + " " + direction2);
                var position = Pathfinding.ExtendUntilWall(
                    this.targetPosition,
                    direction1,
                    Math.Max(
                        this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 100,
                        distanceFromSegment2),
                    this.Unit.Pathfinder);

                var position2 = Pathfinding.ExtendUntilWall(
                    this.targetPosition,
                    direction2,
                    Math.Max(
                        this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 100,
                        distanceFromSegment2),
                    this.Unit.Pathfinder);

                var distance = unitPosition.Distance2D(position);
                var distance2 = unitPosition.Distance2D(position2);
                if (distance2 < distance)
                {
                    distance = distance2;
                    position = position2;
                }

                if (!isCloserToFront)
                {
                    if (distanceFromSegment2 < this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 50)
                    {
                        infront = Pathfinding.ExtendUntilWall(position, direction, distance + 500, this.Unit.Pathfinder);
                    }
                    else
                    {
                        infront = Pathfinding.ExtendUntilWall(unitPosition, direction, 500, this.Unit.Pathfinder);
                    }
                }

                // else
                // {
                // infront = Pathfinding.ExtendUntilWall(unitPosition, direction, 300, this.Pathfinder);
                // }
            }
            else
            {
                // if (!this.Target.SourceUnit.CanMove() || !this.targetWasMoving)
                // {
                // this.moveOrderWasSent = false;
                // return this.CantMove();
                // }
                infront =
                    this.Target.SourceUnit.InFront(
                        this.Target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius + 300
                        + this.Target.SourceUnit.MovementSpeed * (Game.Ping / 1000) * 2);

                // this.moveOrderWasSent = false;
                // return false;
            }

            this.moveOrderWasSent = true;
            this.Unit.SourceUnit.Move(infront);
            this.Bodyblocking = false;
            return true;
        }

        public virtual bool CantMove()
        {
            return this.Unit.SourceUnit.Attack(this.Target.SourceUnit);
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {
        }

        public bool Issue()
        {
            if (!this.Enabled || this.issueSleeper.Sleeping)
            {
                return false;
            }

            this.issueSleeper.Sleep(this.IssueSleep);

            if (!this.Unit.TargetSelector.TargetIsSet || !this.Target.Visibility.Visible
                || !this.Target.SourceUnit.IsAlive
                || this.Target.Position.Current.Distance2D(this.Unit.Position.Current) > this.MaxTargetDistance)
            {
                // this.Unit.TargetSelector.GetTarget();
                return this.NoTarget();
            }

            return this.Bodyblock();
        }

        public bool MoveToMouse()
        {
            return this.Unit.SourceUnit.Move(Game.MousePosition);
        }

        public virtual bool NoTarget()
        {
            return this.MoveToMouse();
        }

        public bool PreciseIssue()
        {
            if (!this.Enabled)
            {
                return false;
            }

            if (!this.Unit.TargetSelector.TargetIsSet || !this.Target.Visibility.Visible
                || !this.Target.SourceUnit.IsAlive
                || this.Target.Position.Current.Distance2D(this.Unit.Position.Current) > this.MaxTargetDistance)
            {
                // this.Unit.TargetSelector.GetTarget();
                return false;
            }

            if (Math.Abs(this.lastRad - this.Target.SourceUnit.RotationRad) > Math.PI / 6
                || this.targetPosition.Distance2D(this.Target.Position.PredictedByLatency) > 35
                && !this.moveOrderWasSent)
            {
                return this.Bodyblock();
            }

            return false;
        }

        public virtual bool Stop()
        {
            return this.Unit.SourceUnit.Stop();
        }

        #endregion
    }
}