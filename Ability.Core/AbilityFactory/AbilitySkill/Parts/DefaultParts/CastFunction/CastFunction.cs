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

        private Func<IAbilityUnit, bool> validTarget;

        public Func<bool> CastFunc { get; set; }

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

        public virtual bool CanCast()
        {
            return this.Skill.CanCast() && this.canCast();
        }

        public virtual bool TargetIsValid(IAbilityUnit target)
        {
            return this.validTarget(target);
        }

        public IAbilityUnit LastTarget { get; set; }

        public virtual bool Cast(IAbilityUnit target)
        {
            if (!this.CanCast() || this.Skill.CastData.Queued
                || !this.TargetIsValid(this.Skill.Owner.TargetSelector.Target))
            {
                return false;
            }

            return this.CastFunc();
        }

        public virtual bool Cast()
        {
            if (!this.CanCast() || this.Skill.CastData.Queued
                || !this.TargetIsValid(this.Skill.Owner.TargetSelector.Target))
            {
                return false;
            }
            
            return this.CastFunc();
        }

        public abstract bool Cast(IAbilityUnit[] targets);

        public void Dispose()
        {
        }

        public virtual void Initialize()
        {
            if (this.Skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.NoTarget))
            {
                if (this.Skill.AbilityInfo.IsDisable)
                {
                    this.CastFunc = () =>
                        {
                            if (!this.Skill.Owner.TargetSelector.Target.DisableManager.CanDisable(0))
                            {
                                return false;
                            }

                            this.Skill.Owner.TargetSelector.Target.DisableManager.CastingDisable(
                                this.Skill.HitDelay.Get());
                            this.Skill.Owner.OrderQueue.EnqueueOrder(
                                new CastSkill(
                                    OrderType.DealDamageToEnemy,
                                    this.Skill,
                                    () => this.Skill.SourceAbility.UseAbility()));
                            return true;
                        };
                }
                else
                {
                    this.CastFunc =
                        () =>
                            {
                                this.Skill.Owner.OrderQueue.EnqueueOrder(
                                    new CastSkill(
                                        OrderType.DealDamageToEnemy,
                                        this.Skill,
                                        () => this.Skill.SourceAbility.UseAbility()));
                                return true;
                            };
                }

                this.canCast =
                    () =>
                        this.Skill.Owner.TargetSelector.TargetIsSet
                        && this.Skill.Owner.TargetSelector.Target.SourceUnit.IsVisible;
            }
            else if (this.Skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.UnitTarget))
            {
                if (this.Skill.SourceAbility.TargetTeamType.HasFlag(TargetTeamType.Enemy))
                {
                    if (this.Skill.AbilityInfo.IsDisable)
                    {
                        this.CastFunc = () =>
                            {
                                if (!this.Skill.Owner.TargetSelector.Target.DisableManager.CanDisable(0))
                                {
                                    return false;
                                }

                                this.Skill.Owner.TargetSelector.Target.DisableManager.CastingDisable(
                                    this.Skill.HitDelay.Get());
                                this.Skill.Owner.OrderQueue.EnqueueOrder(
                                    new CastSkill(
                                        OrderType.DealDamageToEnemy,
                                        this.Skill,
                                        () =>
                                            this.Skill.SourceAbility.UseAbility(
                                                this.Skill.Owner.TargetSelector.Target.SourceUnit)));
                                return true;
                            };
                    }
                    else
                    {
                        this.CastFunc = () =>
                            {
                                this.Skill.Owner.OrderQueue.EnqueueOrder(
                                    new CastSkill(
                                        OrderType.DealDamageToEnemy,
                                        this.Skill,
                                        () =>
                                            this.Skill.SourceAbility.UseAbility(
                                                this.Skill.Owner.TargetSelector.Target.SourceUnit)));
                                return true;
                            };

                    }
                }
                else
                {
                    this.CastFunc =
                        () =>
                            {
                                this.Skill.Owner.OrderQueue.EnqueueOrder(
                                    new CastSkill(
                                        OrderType.DealDamageToEnemy,
                                        this.Skill,
                                        () => this.Skill.SourceAbility.UseAbility(this.Skill.Owner.SourceUnit)));
                                return true;
                            };
                }


                this.canCast =
                    () =>
                        this.Skill.Owner.TargetSelector.TargetIsSet
                        && this.Skill.Owner.TargetSelector.Target.SourceUnit.IsVisible
                        && this.Skill.CastRange.TargetInRange(this.Skill.Owner.TargetSelector.Target);
            }


            if (this.Skill.SourceAbility.TargetTeamType.HasFlag(TargetTeamType.Enemy))
            {
                if (this.Skill.SourceAbility.SpellPierceImmunityType == SpellPierceImmunityType.EnemiesNo)
                {
                    this.validTarget =
                        (target) =>
                            target.SourceUnit.IsAlive
                            && !target.Modifiers.MagicImmune
                            && !target.Modifiers.Invul;
                }
                else
                {
                    this.validTarget =
                        (target) =>
                            target.SourceUnit.IsAlive
                            && !target.Modifiers.Invul;
                }
            }
            else
            {
                this.validTarget =
                    (target) =>
                        target.SourceUnit.IsAlive
                        && !target.Modifiers.MagicImmune
                        && !target.Modifiers.Invul;
            }
        }

        #endregion
    }
}