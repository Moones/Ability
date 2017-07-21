using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DisableManager
{
    public interface IDisableManager : IAbilityUnitPart
    {
        void CastingDisable(float delay);

        bool WillGetDisabled { get; }

        bool CanDisable(float delay);
    }
}
