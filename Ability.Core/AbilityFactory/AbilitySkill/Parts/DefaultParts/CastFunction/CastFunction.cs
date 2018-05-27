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
        

        private Func<IAbilityUnit, bool> validTarget;

        #endregion

        #region Constructors and Destructors

        protected CastFunctionBase(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        public Func<IAbilityUnit, bool> CastFunc { get; set; }

        public IAbilityUnit LastTarget { get; set; }

        public IAbilitySkill Skill { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual bool CanCast()
        {
            return this.Skill.CanCast();
        }

        public virtual bool Cast(IAbilityUnit target)
        {
            if (!this.Skill.CanCast() || !target.SourceUnit.IsVisible
                || !this.Skill.CastRange.TargetInRange(target)
                || this.Skill.CastData.Queued || !this.TargetIsValid(target))
            {
                //Console.WriteLine("Cant cast on target cancast:");
                return false;
            }

            //Console.WriteLine("casting on target");
            return this.CastFunc(target);
        }

        public virtual bool Cast()
        {
            if (!this.CanCast() || this.Skill.CastData.Queued
                || !this.TargetIsValid(this.Skill.Owner.TargetSelector.Target))
            {
                return false;
            }

            return this.CastFunc(this.Skill.Owner.TargetSelector.Target);
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
                    this.CastFunc = (target) =>
                        {
                            if (!target.DisableManager.CanDisable(0))
                            {
                                return false;
                            }

                            target.DisableManager.CastingDisable(
                                this.Skill.HitDelay.Get());
                            this.Skill.Owner.OrderQueue.EnqueueOrder(
                                new CastSkill(
                                    OrderType.DealDamageToEnemy,
                                    this.Skill,
                                    () => this.Skill.SourceAbility.UseAbility(), target));
                            return true;
                        };
                }
                else
                {
                    this.CastFunc = (target) =>
                        {
                            this.Skill.Owner.OrderQueue.EnqueueOrder(
                                new CastSkill(
                                    OrderType.DealDamageToEnemy,
                                    this.Skill,
                                    () => this.Skill.SourceAbility.UseAbility(), target));
                            return true;
                        };
                }

                this.validTarget =
                    (target) =>
                        target != null && target.SourceUnit.IsValid && target.SourceUnit.IsAlive
                        && target.SourceUnit.IsVisible && !target.Modifiers.MagicImmune && !target.Modifiers.Invul;
            }
            else if (this.Skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.UnitTarget))
            {
                if (this.Skill.SourceAbility.TargetTeamType.HasFlag(TargetTeamType.Enemy))
                {
                    if (this.Skill.AbilityInfo.IsDisable)
                    {
                        this.CastFunc = (target) =>
                        {
                            if (!target.DisableManager.CanDisable(0))
                            {
                                //Console.WriteLine("targetDisabled");
                                return false;
                                }

                                target.DisableManager.CastingDisable(
                                    this.Skill.HitDelay.Get());

                            //Console.WriteLine("enqueued2");
                            this.Skill.Owner.OrderQueue.EnqueueOrder(
                                    new CastSkill(
                                        OrderType.DealDamageToEnemy,
                                        this.Skill,
                                        () =>
                                            this.Skill.SourceAbility.UseAbility(
                                                target.SourceUnit), target));
                                return true;
                            };
                    }
                    else
                    {
                        this.CastFunc = (target) =>
                        {
                            //Console.WriteLine("enqueued1");
                            this.Skill.Owner.OrderQueue.EnqueueOrder(
                                    new CastSkill(
                                        OrderType.DealDamageToEnemy,
                                        this.Skill,
                                        () =>
                                            this.Skill.SourceAbility.UseAbility(
                                                target.SourceUnit), target));
                                return true;
                            };
                    }
                }
                else
                {
                    this.CastFunc = (target) =>
                    {
                        //Console.WriteLine("enqueued3");
                        this.Skill.Owner.OrderQueue.EnqueueOrder(
                                new CastSkill(
                                    OrderType.DealDamageToEnemy,
                                    this.Skill,
                                    () => this.Skill.SourceAbility.UseAbility(this.Skill.Owner.SourceUnit), target));
                            return true;
                        };
                }

                this.validTarget =
                    (target) =>
                        target != null && target.SourceUnit.IsValid && target.SourceUnit.IsAlive
                        && target.SourceUnit.IsVisible && !target.Modifiers.MagicImmune && !target.Modifiers.Invul
                        && this.Skill.CastRange.TargetInRange(this.Skill.Owner.TargetSelector.Target);
            }
            else if (this.Skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.Point)
                     || this.Skill.SourceAbility.IsAbilityBehavior(AbilityBehavior.AreaOfEffect))
            {
                this.validTarget =
                    (target) =>
                        target != null && target.SourceUnit.IsValid && target.SourceUnit.IsAlive
                        && target.SourceUnit.IsVisible && !target.Modifiers.MagicImmune
                        && this.Skill.CastRange.TargetInRange(this.Skill.Owner.TargetSelector.Target);
                this.CastFunc = (target) =>
                    {
                        this.Skill.Owner.OrderQueue.EnqueueOrder(
                            new CastSkill(
                                OrderType.DealDamageToEnemy,
                                this.Skill,
                                () =>
                                    this.Skill.SourceAbility.UseAbility(
                                        target.Position.PredictedByLatency), target));
                        return true;
                    };
            }

            if (this.validTarget == null)
            {
                if (this.Skill.SourceAbility.TargetTeamType.HasFlag(TargetTeamType.Enemy))
                {
                    if (this.Skill.SourceAbility.SpellPierceImmunityType == SpellPierceImmunityType.EnemiesNo)
                    {
                        this.validTarget =
                            (target) =>
                                target.SourceUnit.IsAlive && target.SourceUnit.IsVisible
                                && !target.Modifiers.MagicImmune && !target.Modifiers.Invul;
                    }
                    else
                    {
                        this.validTarget =
                            (target) =>
                                target.SourceUnit.IsAlive && target.SourceUnit.IsVisible && !target.Modifiers.Invul;
                    }
                }
                else
                {
                    this.validTarget =
                        (target) =>
                            target.SourceUnit.IsAlive && target.SourceUnit.IsVisible && !target.Modifiers.MagicImmune
                            && !target.Modifiers.Invul;
                }
            }
        }

        public virtual bool TargetIsValid(IAbilityUnit target)
        {
            return this.validTarget(target);
        }

        #endregion
    }
}