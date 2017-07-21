﻿namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;

    public class NoTarget : CastFunctionBase
    {
        public NoTarget(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool Cast(IAbilityUnit target)
        {
            this.Skill.Owner.OrderQueue.EnqueueOrder(
                new CastSkill(
                    OrderType.DealDamageToEnemy,
                    this.Skill,
                    () =>
                        {
                            this.Skill.SourceAbility.UseAbility();
                            return this.Skill.IsItem ? 250 : (float)(this.Skill.CastData.CastPoint * 250);
                        }));

            return true;
        }

        public override bool Cast(IAbilityUnit[] targets)
        {
            throw new NotImplementedException();
        }

        public override bool Cast()
        {
            this.Skill.Owner.OrderQueue.EnqueueOrder(
                new CastSkill(
                    OrderType.DealDamageToEnemy,
                    this.Skill,
                    () =>
                    {
                        this.Skill.SourceAbility.UseAbility();
                        return this.Skill.IsItem ? 250 : (float)(this.Skill.CastData.CastPoint * 250);
                    }));

            return true;
        }
    }
}
