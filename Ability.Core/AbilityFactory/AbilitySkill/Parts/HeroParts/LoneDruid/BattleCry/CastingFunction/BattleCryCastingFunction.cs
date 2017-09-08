using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.BattleCry.CastingFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.ControllableUnits;

    using Ensage.Common.Extensions;

    public class BattleCryCastingFunction : DefaultCastingFunction
    {
        public BattleCryCastingFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        private LoneDruidControllableUnits controllableUnits;



        public override void Initialize()
        {
            base.Initialize();

            this.controllableUnits = this.Skill.Owner.ControllableUnits as LoneDruidControllableUnits;
        }

        public override bool CanCast()
        {
            return base.CanCast()
                   && (this.Skill.Owner.AttackRange.IsInAttackRange(this.Skill.Owner.TargetSelector.Target)
                       || (this.controllableUnits.Bear != null
                           && this.controllableUnits.Bear.AttackRange.IsInAttackRange(
                               this.Skill.Owner.TargetSelector.Target)))
                   && (this.Skill.Owner.TargetSelector.Target.SourceUnit.IsAttacking()
                       || !this.Skill.Owner.TargetSelector.Target.SourceUnit.CanMove()
                       || this.Skill.Owner.TargetSelector.Target.SourceUnit.MovementSpeed
                       < this.Skill.Owner.SourceUnit.MovementSpeed);
        }
    }
}
