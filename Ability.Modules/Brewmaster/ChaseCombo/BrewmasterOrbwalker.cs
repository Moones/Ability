// <copyright file="BrewmasterOrbwalker.cs" company="EnsageSharp">
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
namespace Ability.Brewmaster.ChaseCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.AttackRange;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.SkillBook;

    using Ensage;
    using Ensage.Common.Extensions;

    public class BrewmasterOrbwalker : UnitOrbwalkerBase
    {
        private bool enabled;

        #region Constructors and Destructors

        public BrewmasterOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods and Operators

        public override bool AfterAttack()
        {
            if (this.CastSpells())
            {
                return true;
            }

            if (!this.Target.SourceUnit.CanMove()
                && this.Unit.TargetSelector.LastDistanceToTarget
                > this.Target.Position.PredictedByLatency.Distance2D(Game.MousePosition))
            {
                return this.Attack();
            }

            return this.Move();
        }

        public override bool BeforeAttack()
        {
            if (this.CastSpells())
            {
                return false;
            }

            return base.BeforeAttack();
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
            return this.Unit.UnitCombo.CastAllSpellsOnTarget();
        }

        public override void Initialize()
        {
            base.Initialize();
            //this.Unit.Fighting = true;
            //if (this.Unit.TargetSelector.TargetIsSet && this.Bear != null && this.Bear.TargetSelector.LastDistanceToTarget < 1000)
            //{
            //    this.Bear.Fighting = true;
            //}
        }

        public override void Dispose()
        {
            base.Dispose();
            //this.Unit.Fighting = false;
            //if (this.Bear != null)
            //{
            //    this.Bear.Fighting = false;
            //}
        }

        public override bool Meanwhile()
        {
            if (this.CastSpells())
            {
                return true;
            }
            
            if (!this.Target.SourceUnit.CanMove()
                && this.Unit.TargetSelector.LastDistanceToTarget
                > this.Target.Position.PredictedByLatency.Distance2D(Game.MousePosition))
            {
                return this.Attack();
            }

            return this.Move();
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
            return this.Unit.UnitCombo.NoTarget();
        }

        #endregion
    }
}