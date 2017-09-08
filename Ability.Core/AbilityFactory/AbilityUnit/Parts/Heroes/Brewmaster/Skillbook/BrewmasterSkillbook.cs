using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Brewmaster.Skillbook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    public class BrewmasterSkillbook : SkillBook<IAbilitySkill>
    {
        public BrewmasterSkillbook(IAbilityUnit unit)
            : base(unit)
        {
        }

        public IAbilitySkill ThunderClap { get; private set; }
        public IAbilitySkill DrunkenHaze { get; private set; }

        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);
            
            if (skill.SourceAbility.Id == AbilityId.brewmaster_thunder_clap)
            {
                this.ThunderClap = skill;
            }
            else if (skill.SourceAbility.Id == AbilityId.brewmaster_drunken_haze)
            {
                this.DrunkenHaze = skill;
            }
        }
    }
}
