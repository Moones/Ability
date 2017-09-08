using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Brewmaster.HurlBoulder.CastFunction
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction.Generic;
    public class HurlBoulderCastFunction : DefaultCastingFunction
    {
        public HurlBoulderCastFunction(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool CanCast()
        {
            //Console.WriteLine(this.Skill.CastRange.IsTargetInRange);
            return base.CanCast();
        }
    }
}
