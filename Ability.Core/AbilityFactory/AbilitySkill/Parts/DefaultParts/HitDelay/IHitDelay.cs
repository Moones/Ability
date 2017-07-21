using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.HitDelay
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts;
    public interface IHitDelay : IAbilitySkillPart
    {
        float Get(IAbilityUnit target);
        float Get();
    }
}
