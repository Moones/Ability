namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;

    public class UnitTarget : CastFunctionBase
    {
        public UnitTarget(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool Cast(IAbilityUnit target)
        {
            if (!this.Skill.CanCast())
            {
                return false;
            }


            this.enqueue(target);
            return true;
        }

        public override bool Cast(IAbilityUnit[] targets)
        {
            throw new NotImplementedException();
        }

        public override bool Cast()
        {
            if (!this.Skill.CanCast()
                || this.Skill.Owner.TargetSelector.LastDistanceToTarget > this.Skill.CastRange.Value)
            {
                return false;
            }

            this.enqueue(this.Skill.Owner.TargetSelector.Target);

            return true;
        }

        private void enqueue(IAbilityUnit target)
        {
            if (this.Skill.AbilityInfo.IsDisable)
            {
                target.DisableManager.CastingDisable(this.Skill.HitDelay.Get());
            }

            this.Skill.Owner.OrderQueue.EnqueueOrder(
                new CastSkill(
                    OrderType.DealDamageToEnemy,
                    this.Skill,
                    () =>
                    {
                        if (this.Skill.AbilityInfo.IsDisable)
                        {
                            target.DisableManager.CastingDisable(this.Skill.HitDelay.Get());
                        }

                        this.Skill.SourceAbility.UseAbility(target.SourceUnit);
                        return this.Skill.IsItem ? 250 : (float)(this.Skill.CastData.CastPoint * 250);
                    }));
        }
    }
}
