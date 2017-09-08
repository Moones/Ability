using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.UnitParts.Necronomicon.ManaBurn.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.UnitParts.Necronomicon.ManaBurn.DamageCalculator;

    public class ManaBurnCastingFunction : DefaultCastingFunction
    {
        public ManaBurnCastingFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        private ManaBurnDamageCalculator damageCalculator;

        public override void Initialize()
        {
            base.Initialize();

            this.damageCalculator = this.Skill.DamageCalculator as ManaBurnDamageCalculator;
        }

        public override bool CanCast()
        {
            Console.WriteLine(this.Skill.Owner.TargetSelector.Target.Mana.Current + 70 > this.damageCalculator.ManaBurnValue);
            return base.CanCast()
                   && this.Skill.Owner.TargetSelector.Target.Mana.Current + 70 > this.damageCalculator.ManaBurnValue;
        }
    }
}
