using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.HitDelay
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;

    public class HitDelay : IHitDelay
    {
        public HitDelay(IAbilitySkill skill)
        {
            this.Skill = skill;
        }
        public void Dispose()
        {
        }
        

        public void Initialize()
        {
        }

        public float Get(IAbilityUnit target)
        {
            return (float)(Game.Ping + this.Skill.CastData.CastPoint * 1000f);
        }

        public float Get()
        {
            return (float)(Game.Ping + this.Skill.CastData.CastPoint * 1000f);
        }

        public IAbilitySkill Skill { get; set; }
    }
}
