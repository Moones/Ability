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
namespace Ability.Lycan.ChaseCombo
{
    using System.Linq;

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

            return this.Unit.UnitCombo.CastAllSpellsOnTarget();
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
            if (this.RunAround(this.Unit.Owner, this.Target)
                || this.Unit.Owner.ControllableUnits.Units.Any(x => this.RunAround(x.Value, this.Target)))
            {
                return true;
            }
            
            this.Bodyblock();
            return true;
        }

        public override void MoveBeforeAttack()
        {
            if (this.RunAround(this.Unit.Owner, this.Target)
                || this.Unit.Owner.ControllableUnits.Units.Any(x => this.RunAround(x.Value, this.Target)))
            {
                return;
            }

            this.Attack();
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

            if (this.RunAround(this.Unit.Owner, Game.MousePosition)
                || this.Unit.Owner.ControllableUnits.Units.Any(x => this.RunAround(x.Value, Game.MousePosition)))
            {
                return true;
            }

            this.Unit.SourceUnit.Move(Game.MousePosition);
            return true;
        }

        #endregion

        #region Methods

        private bool CastSpellsNoTarget()
        {
            return this.Unit.UnitCombo.NoTarget();
        }

        #endregion
    }
}