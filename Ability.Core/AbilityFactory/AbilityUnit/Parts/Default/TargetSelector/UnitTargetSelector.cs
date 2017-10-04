// <copyright file="UnitTargetSelector.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TargetSelector
{
    using System;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Position;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class UnitTargetSelector : IUnitTargetSelector
    {
        #region Fields

        private int lastZeroHealthId;

        private IDisposable positionUnsubscriber;

        private IAbilityUnit target;

        #endregion

        #region Constructors and Destructors

        public UnitTargetSelector(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public float LastDistanceToTarget { get; set; }

        public float MaxTargetDistance { get; set; }

        public IAbilityUnit Target
        {
            get
            {
                return this.target;
            }

            set
            {
                this.target?.Health.ZeroHealth.Unsubscribe(this.lastZeroHealthId);
                this.positionUnsubscriber?.Dispose();
                var targett = value;
                if (targett != null)
                {
                    this.target = value;
                    this.TargetIsSet = true;
                    this.lastZeroHealthId = this.target.Health.ZeroHealth.Subscribe(this.TargetDied);
                    this.positionUnsubscriber =
                        this.Target.Position.Subscribe(
                            new DataObserver<IPosition>(position => { this.UpdateDistance(); }));
                    this.TargetChanged.Notify();
                }
                else
                {
                    this.TargetIsSet = false;
                    this.Unit.Fighting = false;
                    //Console.WriteLine(this.Unit.PrettyName + " fighting false");
                }

                //this.TargetChanged.Notify();
            }
        }

        public Notifier TargetChanged { get; } = new Notifier();

        public Notifier TargetDistanceChanged { get; } = new Notifier();

        public bool TargetIsSet { get; set; }

        public Notifier TargetStartAttacking { get; } = new Notifier();

        public Notifier TargetStartMoving { get; } = new Notifier();

        public Notifier FightingNotifier { get; } = new Notifier();

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public virtual IAbilityUnit GetTarget()
        {
            var mouseDistance = 9999999f;
            var mousePosition = Game.MousePosition;
            IAbilityUnit result = null;
            //Console.WriteLine("looking for target");
            foreach (var teamOtherTeam in this.Unit.Team.OtherTeams)
            {
                if (teamOtherTeam?.UnitManager == null || !teamOtherTeam.UnitManager.Units.Any())
                {
                    continue;
                }

                foreach (var unitManagerUnit in teamOtherTeam.UnitManager.Units)
                {
                    if (!unitManagerUnit.Value.SourceUnit.IsValid || !unitManagerUnit.Value.SourceUnit.IsAlive
                        || !unitManagerUnit.Value.Visibility.Visible || unitManagerUnit.Value.Health.Current <= 0)
                    {
                        continue;
                    }

                    //Console.WriteLine("unit " + unitManagerUnit.Value.Name);
                    var distance = unitManagerUnit.Value.Position.Current.Distance2D(mousePosition);
                    if (distance < mouseDistance)
                    {
                        mouseDistance = distance;
                        result = unitManagerUnit.Value;
                    }
                }
            }

            //if (result != null)
            //{
            //    Console.WriteLine("setting target " + result.Name);
            //}
            this.Target = result;
            return result;
        }

        public IAbilityUnit[] GetTargets()
        {
            return new IAbilityUnit[0];
        }

        public void Initialize()
        {
            this.Unit.Position.Subscribe(
                new DataObserver<IPosition>(
                    position =>
                        {
                            if (this.TargetIsSet)
                            {
                                this.UpdateDistance();
                            }
                        }));
        }

        public void ResetTarget()
        {
            this.target?.Health.ZeroHealth.Unsubscribe(this.lastZeroHealthId);
            this.positionUnsubscriber?.Dispose();
            this.Target = null;
        }

        #endregion

        #region Methods

        private void TargetDied()
        {
            this.GetTarget();
        }

        private void UpdateDistance()
        {
            if (!this.TargetIsSet || this.Target == null)
            {
                return;
            }

            this.LastDistanceToTarget =
                this.Unit.Position.PredictedByLatency.Distance2D(this.Target.Position.PredictedByLatency);
            if (this.LastDistanceToTarget < 1500 && !this.Unit.Fighting)
            {
                this.Unit.Fighting = true;
                //Console.WriteLine(this.Unit.PrettyName + " fighting true");
            }
            else if (this.Unit.Fighting && this.LastDistanceToTarget > 1500)
            {
                this.Unit.Fighting = false;
                //Console.WriteLine(this.Unit.PrettyName + " fighting false");
            }

            this.TargetDistanceChanged.Notify();
        }

        #endregion
    }
}