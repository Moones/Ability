// <copyright file="BearRuneTaker.cs" company="EnsageSharp">
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
namespace Ability.LoneDruid.RuneTaker
{
    using System.Collections.Generic;

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.RuneTaker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    public class BearRuneTaker : RuneTaker
    {
        #region Fields

        public IAbilitySkill Return;

        private IAbilityUnit unit;

        #endregion

        #region Constructors and Destructors

        public BearRuneTaker(IAbilityUnit unit, IAbilityMapData abilityMapData)
            : base(unit, abilityMapData, true, new AbilitySubMenu("BearRuneTaker"))
        {
            this.TakeBountyOnStart = true;
        }

        #endregion

        #region Public Properties

        public override IAbilityUnit Unit
        {
            get
            {
                return this.unit;
            }

            set
            {
                this.unit = value;
                if (this.unit != null)
                {
                    this.Return = (this.unit.SkillBook as SpiritBearSkillBook).Return;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        public override void EnqueueRunForRune(List<Vector3> path)
        {
            this.Unit.OrderQueue.EnqueueOrder(new BearRuneForRune(this.Unit, this.ClosestBounty, path));
            this.Return?.CastFunction.Cast();
        }

        public override bool ShouldTakeRune(RunePosition<BountyRune> rune)
        {
            var time = Game.GameTime;
            if (time < 0)
            {
                if (rune.Team == this.Unit.Team.Name)
                {
                    return false;
                }
            }

            if (rune != null && rune.HasRune && this.Unit.Owner.Position.Current.Distance2D(rune.Position) < 300)
            {
                return false;
            }

            return true;
        }

        public override bool ShouldTakeRune(RunePosition<PowerUpRune> rune)
        {
            if (rune.HasRune && rune.CurrentRune.SourceRune.RuneType == RuneType.Illusion)
            {
                return false;
            }

            return false;
        }

        #endregion
    }
}