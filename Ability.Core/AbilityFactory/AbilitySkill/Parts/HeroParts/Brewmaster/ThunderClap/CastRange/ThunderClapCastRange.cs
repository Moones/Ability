using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Brewmaster.ThunderClap.CastRange
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastRange;

    using Ensage.Common.Extensions;

    public class ThunderClapCastRange : CastRange
    {
        public ThunderClapCastRange(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override void UpdateValue()
        {
            this.BaseValue = this.Skill.SourceAbility.GetAbilityData("radius", this.Skill.Level.Current);
        }
    }
}
