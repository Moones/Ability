// <copyright file="UnitCombo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class UnitCombo : IUnitCombo, IOrderIssuer
    {
        #region Fields

        private bool afterAttackExecuted;

        private bool beforeAttackExecuted;

        private Func<bool> executeFunc;

        private List<OrderedComboEntries> list;

        private IOrderedEnumerable<OrderedComboEntries> orderedEnumerable;

        #endregion

        #region Constructors and Destructors

        public UnitCombo(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public FunctionManager AttackCancel { get; } = new FunctionManager();

        public bool Attacking { get; set; }

        public FunctionManager BeforeAttack { get; } = new FunctionManager();

        public FunctionManager BetweenAttacks { get; } = new FunctionManager();

        public OrderedComboEntries DamageTargetEntries { get; } = new OrderedComboEntries();

        public OrderedComboEntries DisableTargetEntries { get; } = new OrderedComboEntries();

        public bool Enabled { get; set; }

        public OrderedComboEntries GetCloserToTargetEntries { get; } = new OrderedComboEntries();

        public uint Id { get; set; }

        public bool MeanWhile { get; set; }

        public bool MoveToAttack { get; set; }

        public double NextAttack { get; set; }

        public bool TargetIsValid { get; set; }

        public FunctionManager TargetStartAttacking { get; } = new FunctionManager();

        public FunctionManager TargetStartCasting { get; } = new FunctionManager();

        public FunctionManager TargetStartMoving { get; } = new FunctionManager();

        public float Time { get; set; }

        public IAbilityUnit Unit { get; set; }

        public OrderedComboEntries WeakenTargetEntries { get; } = new OrderedComboEntries();

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public bool Execute()
        {
            return this.GetCloserToTargetEntries.Execute()
                   || this.orderedEnumerable.Any(orderedComboEntriese => orderedComboEntriese.Execute());
        }

        public void Initialize()
        {
            this.DisableTargetEntries.DelayChanged.Subscribe(this.Update);
            this.WeakenTargetEntries.DelayChanged.Subscribe(this.Update);
            this.DamageTargetEntries.DelayChanged.Subscribe(this.Update);

            this.list = new List<OrderedComboEntries>
                            {
                               this.DamageTargetEntries, this.WeakenTargetEntries, this.DamageTargetEntries 
                            };

            // this.Unit.DataReceiver.Drawings.Subscribe(this.OnDraw);
            // this.Unit.DataReceiver.Updates.Subscribe(this.OnUpdate);
            this.Unit.TargetSelector.TargetStartAttacking.Subscribe(() => this.TargetStartAttacking.AnyFunctionPasses());
            this.Unit.TargetSelector.TargetStartMoving.Subscribe(() => this.TargetStartMoving.AnyFunctionPasses());

            this.Update();
        }

        public bool Issue()
        {
            throw new NotImplementedException();
        }

        public virtual bool IsTargetValid()
        {
            return this.Unit.TargetSelector.TargetIsSet && this.Unit.TargetSelector.Target.SourceUnit.IsAlive
                   && this.Unit.TargetSelector.LastDistanceToTarget < this.Unit.TargetSelector.MaxTargetDistance;
        }

        public bool OnDraw()
        {
            this.TargetIsValid = this.IsTargetValid();

            if (!this.TargetIsValid)
            {
                return false;
            }

            this.Attacking = this.Unit.SourceUnit.IsAttacking();

            this.Time = GlobalVariables.Time * 1000 + Game.Ping;
            this.NextAttack = this.Time - this.Unit.AttackAnimationTracker.NextAttackTime
                              + this.Unit.TurnRate.GetTurnTime(this.Unit.TargetSelector.Target) * 1000;

            if (this.NextAttack < 0)
            {
                this.MoveToAttack = false;
                this.beforeAttackExecuted = false;
                if (this.Time >= this.Unit.AttackAnimationTracker.CancelAnimationTime)
                {
                    if (this.afterAttackExecuted)
                    {
                        this.MeanWhile = true;

                        // this.Unit.Target = this.Unit.TargetSelector.GetTarget();
                        return false;
                    }

                    this.afterAttackExecuted = true;
                    return this.AttackCancel.AnyFunctionPasses();
                }
            }
            else if (!this.Attacking)
            {
                // Console.WriteLine(
                // this.Unit.TurnRate.GetTurnTime(this.Target) + " " + this.Unit.AttackAnimation.GetAttackRate() + " "
                // + this.Unit.SourceUnit.AttackRate());
                this.afterAttackExecuted = false;
                if (this.beforeAttackExecuted)
                {
                    return false;
                }

                if (this.Unit.Modifiers.Disarmed || this.Unit.Modifiers.Immobile
                    || this.Unit.TargetSelector.Target.Modifiers.AttackImmune
                    || this.Unit.TargetSelector.Target.Modifiers.Invul)
                {
                    this.MeanWhile = true;
                    return false;
                }

                this.MeanWhile = false;

                if (this.Unit.AttackRange.IsInAttackRange(this.Unit.TargetSelector.Target)
                    && this.BeforeAttack.AnyFunctionPasses())
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

        public void OnUpdate()
        {
        }

        public bool PreciseIssue()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        private void Update()
        {
            this.orderedEnumerable = this.list.OrderBy(entries => entries.MinExecutionDelay);
        }

        #endregion
    }
}