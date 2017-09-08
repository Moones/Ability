using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.PrimalSplit.Storm
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.PrimalSplit.Storm.UnitCombo;

    [Export(typeof(IAbilityUnitComposer))]
    [AbilityUnitMetadata("npc_dota_brewmaster_storm_1", "npc_dota_brewmaster_storm_2", "npc_dota_brewmaster_storm_3")]
    internal class StormUnitComposer : AbilityUnitComposer
    {

        internal StormUnitComposer()
        {
            this.AssignControllablePart<IUnitCombo>(unit => new StormCombo(unit));
        }
    }
}
