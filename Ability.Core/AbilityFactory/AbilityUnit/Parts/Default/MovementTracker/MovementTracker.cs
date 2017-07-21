using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.MovementTracker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Position;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    public class MovementTracker : IMovementTracker
    {
        public MovementTracker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        private bool firstPositionSet;

        private float lastStayTime;
        private float lastIdleTime;

        private Vector3 firstPosition;

        public void Initialize()
        {
            this.Unit.Position.Subscribe(
                new DataObserver<IPosition>(
                    position =>
                        {
                            this.Update();
                        }));
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

        public float IdleTime()
        {
            return GlobalVariables.Time - this.lastIdleTime;
        }

        public float StayTime()
        {
            return GlobalVariables.Time - this.lastStayTime;
        }

        public float StraightTime()
        {
            return 0;
        }

        public Vector3 AverageDirection()
        {
            return Vector3.Zero;
        }
    }
}
