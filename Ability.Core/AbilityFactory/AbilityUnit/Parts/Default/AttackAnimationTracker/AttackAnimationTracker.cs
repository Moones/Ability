// <copyright file="AttackAnimationTracker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public class AttackAnimationTracker : IAttackAnimationTracker
    {
        #region Constructors and Destructors

        public AttackAnimationTracker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public float CancelAnimationTime { get; set; }

        public float NextAttackTime { get; set; }

        public float LastAttackStartTime { get; set; }

        public Notifier AttackReadyNotifier { get; set; } = new Notifier();

        public FunctionManager<bool> OnAttackCancel { get; } = new FunctionManager<bool>();

        public FunctionManager<bool> OnAttackReady { get; } = new FunctionManager<bool>();

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual void AttackStarted()
        {
            this.LastAttackStartTime = GlobalVariables.Time * 1000;

            this.CancelAnimationTime = this.Unit.AttackAnimation.GetAttackPoint() * 1000f + this.LastAttackStartTime;
            this.NextAttackTime = this.Unit.AttackAnimation.GetAttackRate() * 1000f + this.LastAttackStartTime - 1;
        }
        

        public void Dispose()
        {
        }

        public void Initialize()
        {
            //this.Unit.DataReceiver.Drawings.Subscribe(
            //    () =>
            //        {
            //            if (this.Unit.TargetSelector.TargetIsSet)
            //            {
            //                var time = GlobalVariables.Time * 1000 + Game.Ping;
            //                var nextAttack = time - this.Unit.AttackAnimationTracker.NextAttackTime
            //                                 + this.Unit.TurnRate.GetTurnTime(this.Unit.TargetSelector.Target) * 1000;
            //                if (nextAttack >= 0)
            //                {
            //                    this.AttackReadyNotifier.Notify();
            //                    this.AttackReady = true;
            //                }
            //                else
            //                {
            //                    this.AttackReady = false;
            //                }
            //            }
            //        });
        }

        public bool AttackReady { get; set; }

        #endregion
    }
}