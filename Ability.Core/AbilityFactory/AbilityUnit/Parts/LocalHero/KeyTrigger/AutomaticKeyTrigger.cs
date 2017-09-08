using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.KeyTrigger
{
    public class AutomaticKeyTrigger : UnitKeyTriggerBase
    {
        public AutomaticKeyTrigger(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override void Activate()
        {
            base.Activate();


        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}
