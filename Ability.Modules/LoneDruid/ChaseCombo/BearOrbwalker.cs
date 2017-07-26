// <copyright file="BearOrbwalker.cs" company="EnsageSharp">
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
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;

    using Ensage;
    using Ensage.Common.Extensions;

    public class BearOrbwalker : UnitOrbwalkerBase
    {
        #region Fields

        private IAbilityUnit unit1;

        #endregion

        #region Constructors and Destructors

        public BearOrbwalker()
        {
            this.IssueSleep = 150;
        }

        #endregion

        #region Public Properties

        public IAbilityUnit LocalHero { get; set; }

        public SpiritBearSkillBook SkillBook { get; set; }

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

        #endregion

        #region Public Methods and Operators

        public override bool Attack()
        {
            if (!this.LocalHero.SkillBook.HasAghanim && !this.LocalHero.Modifiers.ConsumedAghanim
                && this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency) > 1100)
            {
                return this.Bodyblocker.Bodyblock();
            }

            return base.Attack();
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

            if (this.Unit.TargetSelector.LastDistanceToTarget - 700 > this.LocalHero.TargetSelector.LastDistanceToTarget
                && this.LocalHero.TargetSelector.LastDistanceToTarget
                < this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency))
            {
                if (this.SkillBook.Return.CastFunction.Cast())
                {
                    return true;
                }
            }

            // Console.WriteLine(this.Bodyblocker.Bodyblocking);
            if (this.Unit.ItemManager.PhaseBoots.Equipped && !this.Bodyblocker.Bodyblocking
                && this.Unit.ItemManager.PhaseBoots.Item.CastFunction.Cast())
            {
                return true;
            }

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

            return false;
        }

        public override void Initialize()
        {
        }

        public override bool IsTargetValid()
        {
            return this.Unit.TargetSelector.TargetIsSet && this.Target.SourceUnit.IsAlive
                   && this.LocalHero.TargetSelector.LastDistanceToTarget < this.MaxTargetDistance;
        }

        public override bool Move()
        {
            if (!this.RunAround(this.LocalHero, this.Target))
            {
                this.Bodyblock();
            }

            return true;
        }

        public override void MoveBeforeAttack()
        {
            if (!this.RunAround(this.LocalHero, this.Target))
            {
                this.Attack();
            }
        }

        public override bool NoTarget()
        {
            // if (this.Unit.TargetSelector.TargetIsSet
            // && this.Unit.TargetSelector.LastDistanceToTarget - 700
            // > this.LocalHero.TargetSelector.LastDistanceToTarget
            // && this.LocalHero.TargetSelector.LastDistanceToTarget
            // < this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency))
            // {
            // if (this.SkillBook.Return.CanCast())
            // {
            // this.SkillBook.Return.CastFunction.Cast();
            // return true;
            // }
            // }
            if (this.CastSpellsNoTarget())
            {
                return true;
            }

            if (!this.RunAround(this.LocalHero, Game.MousePosition))
            {
                this.Unit.SourceUnit.Move(Game.MousePosition);
            }

            return true;
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