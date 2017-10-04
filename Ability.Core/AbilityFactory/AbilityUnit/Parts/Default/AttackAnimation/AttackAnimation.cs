// <copyright file="AttackAnimation.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimation
{
    using System;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class AttackAnimation : IAttackAnimation
    {
        #region Fields

        private float baseAttackPoint;

        private bool overpowerModifier;

        #endregion

        #region Constructors and Destructors

        public AttackAnimation(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public virtual float GetAttackPoint()
        {
            return (float)(this.baseAttackPoint / (1.0 + (this.GetAttackSpeed() - 100.0) / 100.0));
        }

        public virtual float GetAttackRate()
        {
            return 1f / this.Unit.SourceUnit.AttacksPerSecond;
        }

        public float GetAttackSpeed()
        {
            if (this.overpowerModifier)
            {
                return 600;
            }

            var attackSpeed = Math.Max(20, this.Unit.SourceUnit.AttackSpeedValue);
            return Math.Min(attackSpeed, 600);
        }

        public void Initialize()
        {
            try
            {
                this.baseAttackPoint =
                    Game.FindKeyValues(
                        this.Unit.Name + "/AttackAnimationPoint",
                        this.Unit.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;
            }
            catch (KeyValuesNotFoundException)
            {
                this.baseAttackPoint = 1;
            }

            if (this.Unit.SkillBook.Spells.Any(x => x.Value.SourceAbility.Id == AbilityId.ursa_overpower))
            {
                this.GotOverpower();
            }
            else
            {
                this.Unit.SkillBook.SkillAdded.Subscribe(
                    new DataObserver<IAbilitySkill>(
                        skill =>
                            {
                                if (skill.SourceAbility.Id == AbilityId.ursa_overpower)
                                {
                                    this.GotOverpower();
                                }
                            }));
            }
        }

        #endregion

        #region Methods

        private void GotOverpower()
        {
            this.overpowerModifier = this.Unit.SourceUnit.HasModifier("modifier_ursa_overpower");
            this.Unit.Modifiers.ModifierAdded.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (modifier.Name == "modifier_ursa_overpower")
                            {
                                this.overpowerModifier = true;
                            }
                        }));

            this.Unit.Modifiers.ModifierRemoved.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (modifier.Name == "modifier_ursa_overpower")
                            {
                                this.overpowerModifier = false;
                            }
                        }));
        }

        #endregion
    }
}