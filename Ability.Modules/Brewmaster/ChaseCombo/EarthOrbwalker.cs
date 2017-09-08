using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Brewmaster.ChaseCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    public class EarthOrbwalker : BrewmasterOrbwalker
    {
        public EarthOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
            this.IssueSleep = 150;
        }
    }
}
