using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.UnitParts.Necronomicon.ManaBurn.DamageCalculator
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator;
    using Ability.Core.AbilityFactory.AbilityUnit;

    public class ManaBurnDamageCalculatorWorker : SkillRawDamageCalculatorWorker
    {
        public ManaBurnDamageCalculatorWorker(
            IAbilitySkill skill,
            IAbilityUnit target,
            ISkillManipulatedDamageCalculatorWorker manipulatedDamageWorker, float burnAmount)
            : base(skill, target, manipulatedDamageWorker)
        {
            this.burnAmount = burnAmount;
        }

        private float burnAmount;

        public override void UpdateRawDamage()
        {
            this.RawDamageValue = Math.Min(this.Target.Mana.Current, this.burnAmount);
        }
    }
}
