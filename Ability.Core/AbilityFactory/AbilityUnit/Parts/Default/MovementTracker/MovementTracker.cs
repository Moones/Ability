// <copyright file="MovementTracker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.MovementTracker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Position;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common.Extensions;

    using SharpDX;

    public class MovementTracker : IMovementTracker
    {
        #region Fields

        private Vector3 firstPosition;

        private bool firstPositionSet;

        private float lastIdleTime;

        private float lastStayTime;

        #endregion

        #region Constructors and Destructors

        public MovementTracker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public Vector3 AverageDirection()
        {
            return Vector3.Zero;
        }

        public void Dispose()
        {
        }

        public float IdleTime()
        {
            return GlobalVariables.Time - this.lastIdleTime;
        }

        public void Initialize()
        {
            this.Unit.Position.Subscribe(new DataObserver<IPosition>(position => { this.Update(); }));
        }

        public float StayTime()
        {
            return GlobalVariables.Time - this.lastStayTime;
        }

        public float StraightTime()
        {
            return 0;
        }

        public void Update()
        {
            if (!this.firstPositionSet)
            {
                this.firstPosition = this.Unit.Position.Current;
                this.lastStayTime = GlobalVariables.Time;
                this.lastIdleTime = GlobalVariables.Time;
                return;
            }

            var distance = this.firstPosition.Distance2D(this.Unit.Position.Current);
            if (distance > this.Unit.SourceUnit.HullRadius * 2)
            {
                this.firstPosition = this.Unit.Position.Current;
                this.lastStayTime = GlobalVariables.Time;
                this.lastIdleTime = GlobalVariables.Time;
                return;
            }

            if (distance > 0)
            {
                this.lastIdleTime = GlobalVariables.Time;
            }
        }

        #endregion
    }
}