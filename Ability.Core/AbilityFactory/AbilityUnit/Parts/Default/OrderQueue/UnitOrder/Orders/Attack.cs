// <copyright file="Attack.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class Attack : UnitOrderBase
    {
        #region Fields

        private bool executedOnce;

        private Sleeper sleeper = new Sleeper();

        private IAbilityUnit target;

        #endregion

        #region Constructors and Destructors

        public Attack(IAbilityUnit unit)
            : base(OrderType.DealDamageToEnemy, unit, "Attack target" + unit.TargetSelector.Target.Name)
        {
            this.PrintInLog = false;
            this.ShouldExecuteFast = true;
        }

        #endregion

        #region Public Properties

        public bool Attacking { get; set; }

        public float Time { get; set; }

        #endregion

        #region Public Methods and Operators

        public override bool CanExecute()
        {
            if (this.Unit.Modifiers.Disarmed || this.Unit.Modifiers.Immobile || !this.Unit.TargetSelector.TargetIsSet
                || this.Unit.TargetSelector.Target.Modifiers.AttackImmune
                || this.Unit.TargetSelector.Target.Modifiers.Invul
                || !this.Unit.TargetSelector.Target.Visibility.Visible
                || this.Unit.TargetSelector.LastDistanceToTarget > this.Unit.AttackRange.Value + 1000
                || !this.Unit.TargetSelector.Target.SourceUnit.IsAlive)
            {
                Console.WriteLine("attack canceled");
                return false;
            }

            this.Time = GlobalVariables.Time * 1000 + Game.Ping * 0.99f;
            this.Attacking = this.Unit.SourceUnit.IsAttacking();
            if (this.Attacking && this.Time > this.Unit.AttackAnimationTracker.CancelAnimationTime)
            {
                return false;
            }

            return true;
        }

        public override void Enqueue()
        {
            this.target = this.Unit.TargetSelector.Target;
            base.Enqueue();
        }

        public override float Execute()
        {
            if (this.sleeper.Sleeping)
            {
                return 0;
            }

            // Console.WriteLine("Executing attack " + this.Unit.PrettyName + " target: " + this.Unit.TargetSelector.Target.PrettyName);
            this.sleeper.Sleep(300);
            this.executedOnce = true;
            this.attack();
            return 0;
        }

        public override float ExecuteFast()
        {
            if (this.executedOnce)
            {
                return 0;
            }

            if (this.sleeper.Sleeping)
            {
                return 0;
            }

            this.executedOnce = true;
            this.attack();
            return 0;
        }

        #endregion

        #region Methods

        private void attack()
        {
            // if (this.Unit.TargetSelector.Target == null)
            // {
            // return;
            // }
            this.Unit.SourceUnit.Attack(this.Unit.TargetSelector.Target.SourceUnit);
        }

        #endregion
    }
}