namespace LoneDruid.ChaseCombo
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.AttackRange;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.SkillBook;

    using Ensage;
    using Ensage.Common.Extensions;

    public class LoneDruidOrbwalker : UnitOrbwalkerBase
    {
        public LoneDruidOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
            this.AttackRange = unit.AttackRange as LoneDruidAttackRange;
            this.SkillBook = unit.SkillBook as LoneDruidSkillBook;
        }

        public LoneDruidAttackRange AttackRange { get; }

        public LoneDruidSkillBook SkillBook { get; }

        public override bool BeforeAttack()
        {
            if (this.CastSpells())
            {
                return false;
            }

            return base.BeforeAttack();
        }

        public override bool NoTarget()
        {
            this.Unit.TargetSelector.GetTarget();
            return base.NoTarget();
        }

        public override bool AfterAttack()
        {
            if (this.CastSpells())
            {
                return true;
            }

            if (this.AttackRange.TrueForm)
            {
                if (!this.Target.SourceUnit.CanMove()
                    && this.Unit.TargetSelector.LastDistanceToTarget
                    > this.Target.Position.PredictedByLatency.Distance2D(Game.MousePosition))
                {
                    return this.Attack();
                }

                return this.Move();
            }

            return this.KeepRange();
        }

        public override bool Meanwhile()
        {
            if (this.CastSpells())
            {
                return true;
            }

            if (this.AttackRange.TrueForm)
            {
                if (!this.Target.SourceUnit.CanMove()
                    && this.Unit.TargetSelector.LastDistanceToTarget
                    > this.Target.Position.PredictedByLatency.Distance2D(Game.MousePosition))
                {
                    return this.Attack();
                }

                return this.Move();
            }

            return this.KeepRange();
        }

        public override bool CastSpells()
        {
            //Console.WriteLine(
            //    this.Unit.TargetSelector.TargetIsSet + " " + this.Unit.TargetSelector.LastDistanceToTarget + " "
            //    + this.SkillBook.Rabid.CastData.EnoughMana + " " + this.SkillBook.Rabid.CastData.IsOnCooldown);
            if (!this.Unit.TargetSelector.TargetIsSet)
            {
                return this.CastSpellsNoTarget();
            }

            //Console.WriteLine(this.Target.Modifiers.Immobile);
            if (!this.Target.DisableManager.WillGetDisabled && !this.Target.Modifiers.Immobile
                && this.Target.SourceUnit.NetworkActivity != NetworkActivity.Idle
                && this.Target.SourceUnit.NetworkActivity != NetworkActivity.IdleImpatient
                && this.Target.SourceUnit.NetworkActivity != NetworkActivity.IdleImpatientSwordTap
                && this.Target.SourceUnit.NetworkActivity != NetworkActivity.IdleRare
                && this.Target.SourceUnit.NetworkActivity != NetworkActivity.IdleSleeping
                && this.Target.SourceUnit.NetworkActivity != NetworkActivity.IdleSleepingEnd
                && this.Target.SourceUnit.NetworkActivity != NetworkActivity.RoquelaireLandIdle
                && this.Target.SourceUnit.NetworkActivity != NetworkActivity.SwimIdle
                && this.Target.SourceUnit.NetworkActivity != NetworkActivity.WaitIdle)
            {
                if (this.CastDisable())
                {
                    return true;
                }
            }

            if (this.Unit.ItemManager.Mjollnir.Equipped && this.Unit.ItemManager.Mjollnir.Item.CanCast()
                && this.Unit.TargetSelector.LastDistanceToTarget
                < this.Unit.TargetSelector.Target.AttackRange.Value + 100)
            {
                return this.Unit.ItemManager.Mjollnir.Item.CastFunction.Cast(this.Unit);
            }

            if (this.Unit.TargetSelector.LastDistanceToTarget < 1500
                && this.SkillBook.Rabid.CanCast())
            {
                return this.SkillBook.Rabid.CastFunction.Cast();
            }

            if (this.Unit.ItemManager.PhaseBoots.Equipped
                && this.Unit.TargetSelector.LastDistanceToTarget > this.Unit.AttackRange.Value
                && this.Unit.ItemManager.PhaseBoots.Item.CanCast())
            {
                return this.Unit.ItemManager.PhaseBoots.Item.CastFunction.Cast();
            }

            if (this.SkillBook.BattleCry.CanCast() && this.Unit.TargetSelector.LastDistanceToTarget < 700
                && (this.Unit.TargetSelector.Target.SourceUnit.IsAttacking()
                    || !this.Unit.TargetSelector.Target.SourceUnit.CanMove()
                    || this.Unit.TargetSelector.Target.SourceUnit.MovementSpeed < 200))
            {
                return this.SkillBook.BattleCry.CastFunction.Cast();
            }

            return false;
        }

        private bool CastDisable()
        {
            if (this.Unit.ItemManager.AbyssalBlade.Equipped
                && this.Unit.ItemManager.AbyssalBlade.Item.CastFunction.Cast())
            {
                return true;
            }

            return false;
        }

        private bool CastSpellsNoTarget()
        {
            if (this.Unit.ItemManager.PhaseBoots.Equipped && this.Unit.ItemManager.PhaseBoots.Item.CanCast())
            {
                return this.Unit.ItemManager.PhaseBoots.Item.CastFunction.Cast();
            }

            return false;
        }

        public override void Initialize()
        {
        }
    }
}
