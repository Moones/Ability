// <copyright file="LoneDruidOrbwalker.cs" company="EnsageSharp">
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
namespace LoneDruid.ChaseCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.AttackRange;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.SkillBook;

    using Ensage;
    using Ensage.Common.Extensions;

    public class LoneDruidOrbwalker : UnitOrbwalkerBase
    {
        #region Constructors and Destructors

        public LoneDruidOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
            this.AttackRange = unit.AttackRange as LoneDruidAttackRange;
            this.SkillBook = unit.SkillBook as LoneDruidSkillBook;
        }

        #endregion

        #region Public Properties

        public LoneDruidAttackRange AttackRange { get; }

        public IAbilityUnit Bear { get; set; }

        public LoneDruidSkillBook SkillBook { get; }

        #endregion

        #region Public Methods and Operators

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

        public override bool BeforeAttack()
        {
            if (this.CastSpells())
            {
                return false;
            }

            return base.BeforeAttack();
        }

        public bool CastDisable()
        {
            if (this.Unit.ItemManager.AbyssalBlade.Equipped
                && this.Unit.TargetSelector.Target.DisableManager.CanDisable(0)
                && this.Unit.ItemManager.AbyssalBlade.Item.CastFunction.Cast())
            {
                return true;
            }

            return false;
        }

        public override bool CastSpells()
        {
            // Console.WriteLine(
            // this.Unit.TargetSelector.TargetIsSet + " " + this.Unit.TargetSelector.LastDistanceToTarget + " "
            // + this.SkillBook.Rabid.CastData.EnoughMana + " " + this.SkillBook.Rabid.CastData.IsOnCooldown);
            if (!this.Unit.TargetSelector.TargetIsSet)
            {
                return this.CastSpellsNoTarget();
            }

            // Console.WriteLine(this.Target.Modifiers.Immobile);
            if (!this.Target.DisableManager.WillGetDisabled)
            {
                if (this.CastDisable())
                {
                    return true;
                }
            }

            if (this.Unit.ItemManager.Mjollnir.Equipped
                && this.Unit.TargetSelector.LastDistanceToTarget
                < this.Unit.TargetSelector.Target.AttackRange.Value + 100
                && this.Unit.ItemManager.Mjollnir.Item.CastFunction.Cast())
            {
                return true;
            }

            if (this.Unit.TargetSelector.LastDistanceToTarget < 1500 && this.SkillBook.Rabid.CastFunction.Cast())
            {
                return true;
            }

            if (this.Unit.ItemManager.PhaseBoots.Equipped
                && this.Unit.TargetSelector.LastDistanceToTarget > this.Unit.AttackRange.Value
                && this.Unit.ItemManager.PhaseBoots.Item.CastFunction.Cast())
            {
                return true;
            }

            if ((this.Unit.AttackRange.IsInAttackRange(this.Unit.TargetSelector.Target)
                 || this.Bear.AttackRange.IsInAttackRange(this.Unit.TargetSelector.Target))
                && (this.Unit.TargetSelector.Target.SourceUnit.IsAttacking()
                    || !this.Unit.TargetSelector.Target.SourceUnit.CanMove()
                    || this.Unit.TargetSelector.Target.SourceUnit.MovementSpeed < this.Unit.SourceUnit.MovementSpeed)
                && this.SkillBook.BattleCry.CastFunction.Cast())
            {
                return true;
            }

            return false;
        }

        public override void Initialize()
        {
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

        public override bool NoTarget()
        {
            this.Unit.TargetSelector.GetTarget();

            if (this.CastSpellsNoTarget())
            {
                return true;
            }

            return base.NoTarget();
        }

        #endregion

        #region Methods

        private bool CastSpellsNoTarget()
        {
            if (this.Unit.ItemManager.PhaseBoots.Equipped && this.Unit.ItemManager.PhaseBoots.Item.CastFunction.Cast())
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}