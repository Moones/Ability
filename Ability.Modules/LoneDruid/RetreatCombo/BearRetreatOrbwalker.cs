// <copyright file="BearRetreatOrbwalker.cs" company="EnsageSharp">
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
namespace Ability.Lycan.RetreatCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    using Ability.Lycan.ChaseCombo;

    public class BearRetreatOrbwalker : BearOrbwalker
    {
        #region Fields

        private IAbilityUnit unit1;

        #endregion

        #region Constructors and Destructors

        public BearRetreatOrbwalker()
        {
            this.LowHp = new AbilityMenuItem<Slider>(
                "BearLowHp",
                new Slider(400, 200, 1000),
                "retreat with bear when he has low hp");
        }

        #endregion

        #region Public Properties

        public AbilityMenuItem<Slider> LowHp { get; }

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

        public override bool CastSpells()
        {
            if (!this.Target.DisableManager.WillGetDisabled)
            {
                if (this.Unit.UnitCombo.DisableTarget())
                {
                    return true;
                }
            }

            return false;
        }

        public override void Initialize()
        {
        }

        public override bool IssueMeanwhileActions()
        {
            if (this.Unit.Health.Current < this.LowHp.Value)
            {
                if (!this.RunAround(this.LocalHero, Game.MousePosition))
                {
                    this.Unit.SourceUnit.Move(Game.MousePosition);
                }

                return true;
            }

            if (this.TargetValid
                && this.Unit.TargetSelector.LastDistanceToTarget - 700
                > this.LocalHero.TargetSelector.LastDistanceToTarget
                && this.LocalHero.TargetSelector.LastDistanceToTarget
                < this.Unit.Position.PredictedByLatency.Distance2D(this.LocalHero.Position.PredictedByLatency))
            {
                if (this.SkillBook.Return.CastFunction.Cast())
                {
                    return true;
                }
            }

            return base.IssueMeanwhileActions();
        }

        public override bool PreciseIssue()
        {
            if (this.Unit.Health.Current < this.LowHp.Value)
            {
                return false;
            }

            return base.PreciseIssue();
        }

        #endregion
    }
}