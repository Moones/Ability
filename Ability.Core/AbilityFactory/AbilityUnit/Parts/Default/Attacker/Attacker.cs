// <copyright file="Attacker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Attacker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public class Attacker : IAttacker
    {
        #region Fields

        private Attack attackOrder;

        private int attackReady;

        private int targetChanged;

        private int targetDistanceChanged;

        #endregion

        #region Public Properties

        public Notifier AttackOrderSent { get; } = new Notifier();

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Activate()
        {
            if (this.Unit.TargetSelector.TargetIsSet
                && this.Unit.AttackRange.IsInAttackRange(this.Unit.TargetSelector.Target)
                && this.Unit.Modifiers.AbleToIssueAttack && this.Unit.TargetSelector.Target.Modifiers.Attackable
                && this.Unit.AttackAnimationTracker.AttackReady)
            {
                this.Unit.OrderQueue.EnqueueOrder(this.attackOrder);
            }

            this.targetChanged = this.Unit.TargetSelector.TargetChanged.Subscribe(() => this.attackOrder.Cancel());
            this.targetDistanceChanged = this.Unit.TargetSelector.TargetDistanceChanged.Subscribe(
                () =>
                    {
                        if (this.Unit.AttackAnimationTracker.AttackReady && this.Unit.Modifiers.AbleToIssueAttack
                            && this.Unit.TargetSelector.Target.Modifiers.Attackable)
                        {
                            this.Unit.OrderQueue.EnqueueOrder(this.attackOrder);
                        }
                    });

            this.attackReady = this.Unit.AttackAnimationTracker.AttackReadyNotifier.Subscribe(
                () =>
                    {
                        if (this.Unit.AttackRange.TargetIsInRange && this.Unit.Modifiers.AbleToIssueAttack
                            && this.Unit.TargetSelector.Target.Modifiers.Attackable)
                        {
                        }
                    });
        }

        public void Attack(IAbilityUnit target)
        {
            this.Attack(target.SourceUnit);
        }

        public void Attack(Unit unit)
        {
            this.AttackOrderSent.Notify();
            this.Unit.SourceUnit.Attack(unit);
        }

        public void Deactivate()
        {
            this.attackOrder.Cancel();

            this.Unit.TargetSelector.TargetChanged.Unsubscribe(this.targetChanged);
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {
            this.attackOrder = new Attack(this.Unit);
        }

        #endregion

        #region Methods

        private void Attack()
        {
            this.Unit.OrderQueue.EnqueueOrder(this.attackOrder);
        }

        #endregion
    }
}