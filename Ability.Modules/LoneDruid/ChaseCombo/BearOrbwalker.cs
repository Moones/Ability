namespace LoneDruid.ChaseCombo
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;

    using Ensage;
    using Ensage.Common.Extensions;

    public class BearOrbwalker : UnitOrbwalkerBase
    {
        private IAbilityUnit unit1;

        public BearOrbwalker()
        {
            this.IssueSleep = 150;
        }


        public override IAbilityUnit Unit
        {
            get
            {
                return this.unit1;
            }

            set
            {
                this.unit1 = value;
                this.Bodyblocker.Unit = this.unit1;
                this.SkillBook = this.unit1.SkillBook as SpiritBearSkillBook;
            }
        }

        public IAbilityUnit LocalHero { get; set; }

        public SpiritBearSkillBook SkillBook { get; set; }

        public override void Initialize()
        {
        }

        public override void MoveBeforeAttack()
        {
            if (!this.RunAround(this.LocalHero, this.Target))
            {
                this.Attack();
            }
        }

        public override bool IsTargetValid()
        {
            return this.Unit.TargetSelector.TargetIsSet && this.Target.SourceUnit.IsAlive
                               && this.LocalHero.TargetSelector.LastDistanceToTarget < this.MaxTargetDistance;
        }

        public override bool NoTarget()
        {
            //if (this.Unit.TargetSelector.TargetIsSet
            //    && this.Unit.TargetSelector.LastDistanceToTarget - 700
            //    > this.LocalHero.TargetSelector.LastDistanceToTarget
            //    && this.LocalHero.TargetSelector.LastDistanceToTarget
            //    < this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency))
            //{
            //    if (this.SkillBook.Return.CanCast())
            //    {
            //        this.SkillBook.Return.CastFunction.Cast();
            //        return true;
            //    }
            //}
            if (!this.RunAround(this.LocalHero, Game.MousePosition))
            {
                this.Unit.SourceUnit.Move(Game.MousePosition);
            }

            return true;
        }

        public override bool Move()
        {
            if (!this.RunAround(this.LocalHero, this.Target))
            {
                this.Bodyblock();
            }

            return true;
        }

        public override bool Attack()
        {
            if (!this.LocalHero.SkillBook.HasAghanim && !this.LocalHero.Modifiers.ConsumedAghanim
                && this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency) > 1100)
            {
                return this.Bodyblocker.Bodyblock();
            }

            return base.Attack();
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


            if (this.Unit.TargetSelector.LastDistanceToTarget - 700 > this.LocalHero.TargetSelector.LastDistanceToTarget
                && this.LocalHero.TargetSelector.LastDistanceToTarget
                < this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency))
            {
                if (this.SkillBook.Return.CanCast())
                {
                    return this.SkillBook.Return.CastFunction.Cast();
                }
            }

            //Console.WriteLine(this.Bodyblocker.Bodyblocking);
            if (this.Unit.ItemManager.PhaseBoots.Equipped && !this.Bodyblocker.Bodyblocking
                && this.Unit.ItemManager.PhaseBoots.Item.CanCast())
            {
                return this.Unit.ItemManager.PhaseBoots.Item.CastFunction.Cast();
            }

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
    }
}
