using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ComboEntry
{
    public interface IComboEntry : IAbilitySkillPart
    {
        ComboEntryType Type { get; }

        float ExecutionDelay { get; set; }

        bool Execute();
    }
}
