// <copyright file="WolfOrbwalker.cs" company="EnsageSharp">
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
    using System;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Lycan.ControllableUnits;

    using Ensage;

    public class WolfOrbwalker : UnitOrbwalkerBase
    {
        #region Fields

        private IAbilityUnit unit1;

        #endregion

        #region Constructors and Destructors

        public WolfOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
            this.IssueSleep = 220;
            this.Unit = unit;
            this.ControllableUnits = this.Unit.Owner.ControllableUnits as LycanControllableUnits;
        }

        #endregion

        #region Public Properties

        public LycanControllableUnits ControllableUnits { get; set; }

        public IAbilityUnit LocalHero { get; set; }

        #endregion

        #region Public Methods and Operators

        public override bool Attack()
        {
            if (this.ControllableUnits.IsClosestWolf(this.Unit))
            {
                return this.Bodyblock();
            }

            return base.Attack();
        }

        public override bool IssueMeanwhileActions()
        {
            if (!this.TargetValid)
            {
                return this.NoTarget();
            }


            if (this.Unit.TargetSelector.LastDistanceToTarget
                    < 800)
            {
                return this.Attack();
            }

            return this.Move();
        }

        public override bool Move()
        {
            //Console.WriteLine(this.Unit.SourceUnit.NetworkActivity);
            // Console.WriteLine(this.Unit.Owner == null);
            if (this.RunAround(this.Unit.Owner, this.Target))
            {
                return true;
            }

            return this.Bodyblocker.Bodyblock() || this.MoveToMouse();
        }

        public override void MoveBeforeAttack()
        {
            this.Move();
        }

        public override bool AfterAttack()
        {
            return base.AfterAttack();
        }

        public override bool NoTarget()
        {
            if (this.RunAround(this.Unit.Owner, Game.MousePosition))
            {
                return true;
            }

            this.Unit.SourceUnit.Move(Game.MousePosition);
            return true;
        }

        #endregion
    }
}