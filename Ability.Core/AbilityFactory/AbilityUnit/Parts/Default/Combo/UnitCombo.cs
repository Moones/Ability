using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class UnitCombo : IUnitCombo, IOrderIssuer
    {
        public UnitCombo(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        private List<OrderedComboEntries> list;

        private IOrderedEnumerable<OrderedComboEntries> orderedEnumerable;

        public void Initialize()
        {
            this.DisableTargetEntries.DelayChanged.Subscribe(this.Update);
            this.WeakenTargetEntries.DelayChanged.Subscribe(this.Update);
            this.DamageTargetEntries.DelayChanged.Subscribe(this.Update);

            this.list = new List<OrderedComboEntries>
                            { this.DamageTargetEntries, this.WeakenTargetEntries, this.DamageTargetEntries };

            //this.Unit.DataReceiver.Drawings.Subscribe(this.OnDraw);
            //this.Unit.DataReceiver.Updates.Subscribe(this.OnUpdate);

            this.Unit.TargetSelector.TargetStartAttacking.Subscribe(
                () => this.TargetStartAttacking.AnyFunctionPasses());
            this.Unit.TargetSelector.TargetStartMoving.Subscribe(
                () => this.TargetStartMoving.AnyFunctionPasses());

            this.Update();
        }

        public virtual bool IsTargetValid()
        {
            return this.Unit.TargetSelector.TargetIsSet && this.Unit.TargetSelector.Target.SourceUnit.IsAlive
                   && this.Unit.TargetSelector.LastDistanceToTarget < this.Unit.TargetSelector.MaxTargetDistance;
        }

        public float Time { get; set; }

        public double NextAttack { get; set; }


        private bool afterAttackExecuted;

        private bool beforeAttackExecuted;
        public bool MoveToAttack { get; set; }
        public bool MeanWhile { get; set; }

        public bool OnDraw()
        {
            this.TargetIsValid = this.IsTargetValid();

            if (!this.TargetIsValid)
            {
                return false;
            }

            this.Attacking = this.Unit.SourceUnit.IsAttacking();

            this.Time = (GlobalVariables.Time) * 1000 + Game.Ping;
            this.NextAttack = this.Time - this.Unit.AttackAnimationTracker.NextAttackTime
                              + (this.Unit.TurnRate.GetTurnTime(this.Unit.TargetSelector.Target) * 1000);

            if (this.NextAttack < 0)
            {
                this.MoveToAttack = false;
                this.beforeAttackExecuted = false;
                if (this.Time >= this.Unit.AttackAnimationTracker.CancelAnimationTime)
                {
                    if (this.afterAttackExecuted)
                    {
                        this.MeanWhile = true;
                        //this.Unit.Target = this.Unit.TargetSelector.GetTarget();
                        return false;
                    }

                    this.afterAttackExecuted = true;
                    return this.AttackCancel.AnyFunctionPasses();
                }
            }
            else if (!this.Attacking)
            {
                //Console.WriteLine(
                //    this.Unit.TurnRate.GetTurnTime(this.Target) + " " + this.Unit.AttackAnimation.GetAttackRate() + " "
                //    + this.Unit.SourceUnit.AttackRate());
                this.afterAttackExecuted = false;
                if (this.beforeAttackExecuted)
                {
                    return false;
                }

                if (this.Unit.Modifiers.Disarmed || this.Unit.Modifiers.Immobile || this.Unit.TargetSelector.Target.Modifiers.AttackImmune
                    || this.Unit.TargetSelector.Target.Modifiers.Invul)
                {
                    this.MeanWhile = true;
                    return false;
                }

                this.MeanWhile = false;

                if (this.Unit.AttackRange.IsInAttackRange(this.Unit.TargetSelector.Target) && this.BeforeAttack.AnyFunctionPasses())
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

        public OrderedComboEntries GetCloserToTargetEntries { get; } = new OrderedComboEntries();

        public OrderedComboEntries DisableTargetEntries { get; } = new OrderedComboEntries();

        public OrderedComboEntries WeakenTargetEntries { get; } = new OrderedComboEntries();

        public OrderedComboEntries DamageTargetEntries { get; } = new OrderedComboEntries();

        public bool Attacking { get; set; }

        public bool Execute()
        {
            return this.GetCloserToTargetEntries.Execute() || this.orderedEnumerable.Any(orderedComboEntriese => orderedComboEntriese.Execute());
        }

        private Func<bool> executeFunc;

        private void Update()
        {
            this.orderedEnumerable = this.list.OrderBy(entries => entries.MinExecutionDelay);
        }

        public FunctionManager BeforeAttack { get; } = new FunctionManager();

        public FunctionManager AttackCancel { get; } = new FunctionManager();

        public FunctionManager BetweenAttacks { get; } = new FunctionManager();

        public FunctionManager TargetStartMoving { get; } = new FunctionManager();

        public FunctionManager TargetStartAttacking { get; } = new FunctionManager();

        public FunctionManager TargetStartCasting { get; } = new FunctionManager();

        public bool TargetIsValid { get; set; }

        public bool Enabled { get; set; }

        public uint Id { get; set; }

        public bool Issue()
        {
            throw new NotImplementedException();
        }

        public bool PreciseIssue()
        {
            throw new NotImplementedException();
        }
    }
}
