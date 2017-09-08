using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.UnitParts.Necronomicon.ManaBurn.DamageCalculator
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator;

    using Ensage.Common.Extensions;

    public class ManaBurnDamageCalculator : SkillDamageCalculator
    {
        public ManaBurnDamageCalculator(IAbilitySkill skill)
            : base(skill)
        {
        }

        public float ManaBurnValue { get; set; }

        public override void Initialize()
        {
            base.Initialize();

            this.ManaBurnValue = this.Skill.SourceAbility.GetAbilityData("burn_amount");

            this.RawDamageWorkerAssign =
                unit =>
                    new ManaBurnDamageCalculatorWorker(
                        this.Skill,
                        unit,
                        this.DamageWorkerAssign(unit),
                        this.ManaBurnValue);
        }
    }
}
