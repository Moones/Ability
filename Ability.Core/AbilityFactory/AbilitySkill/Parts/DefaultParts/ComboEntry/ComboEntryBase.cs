using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ComboEntry
{
    public abstract class ComboEntryBase : IComboEntry
    {
        protected ComboEntryBase(IAbilitySkill skill, ComboEntryType type)
        {
            this.Skill = skill;
            this.Type = type;
        }

        public virtual void Dispose()
        {
        }

        public IAbilitySkill Skill { get; set; }

        public virtual void Initialize()
        {
        }

        public ComboEntryType Type { get; }

        public float ExecutionDelay { get; set; }

        public abstract bool Execute();
    }
}
