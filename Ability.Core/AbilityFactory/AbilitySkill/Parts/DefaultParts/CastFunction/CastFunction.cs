// <copyright file="CastFunction.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;

    using Ensage;
    using Ensage.Common.Extensions;

    public abstract class CastFunctionBase : ICastFunction
    {
        #region Fields

        private Func<bool> canCast;

        private Func<bool> cast;

        #endregion

        #region Constructors and Destructors

        protected CastFunctionBase(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        public IAbilitySkill Skill { get; set; }

        #endregion

        #region Public Methods and Operators

        public bool CanCast()
        {
            return this.Skill.CanCast() && this.canCast();
        }

        public abstract bool Cast(IAbilityUnit target);

        public virtual bool Cast()
        {
            if (!this.CanCast() || this.Skill.CastData.Queued)
            {
                return false;
            }

            if (this.Skill.AbilityInfo.IsDisable)
            {
                this.Skill.Owner.TargetSelector.Target.DisableManager.CastingDisable(this.Skill.HitDelay.Get());
            }

            this.Skill.Owner.OrderQueue.EnqueueOrder(new CastSkill(OrderType.DealDamageToEnemy, this.Skill, this.cast));
            return true;
        }

        public abstract bool Cast(IAbilityUnit[] targets);

        public void Dispose()
        {
        }

        public void Initialize()
        {
            if (this.Skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.NoTarget))
            {
                this.cast = () => this.Skill.SourceAbility.UseAbility();
            }
            else if (this.Skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.UnitTarget))
            {
                if (this.Skill.SourceAbility.TargetTeamType.HasFlag(TargetTeamType.Enemy))
                {
                    this.cast =
                        () => this.Skill.SourceAbility.UseAbility(this.Skill.Owner.TargetSelector.Target.SourceUnit);
                }
                else
                {
                    this.cast = () => this.Skill.SourceAbility.UseAbility(this.Skill.Owner.SourceUnit);
                }
            }

            if (this.Skill.SourceAbility.TargetTeamType.HasFlag(TargetTeamType.Enemy))
            {
                if (this.Skill.SourceAbility.SpellPierceImmunityType == SpellPierceImmunityType.EnemiesNo)
                {
                    this.canCast =
                        () =>
                            this.Skill.Owner.TargetSelector.TargetIsSet
                            && this.Skill.Owner.TargetSelector.Target.SourceUnit.IsAlive
                            && this.Skill.Owner.TargetSelector.Target.SourceUnit.IsVisible
                            && !this.Skill.Owner.TargetSelector.Target.Modifiers.MagicImmune
                            && this.Skill.CastRange.IsTargetInRange;
                }
                else
                {
                    this.canCast =
                        () =>
                            this.Skill.Owner.TargetSelector.TargetIsSet
                            && this.Skill.Owner.TargetSelector.Target.SourceUnit.IsAlive
                            && this.Skill.Owner.TargetSelector.Target.SourceUnit.IsVisible
                            && this.Skill.CastRange.IsTargetInRange;
                }
            }
            else
            {
                this.canCast = () => true;
            }
        }

        #endregion
    }
}