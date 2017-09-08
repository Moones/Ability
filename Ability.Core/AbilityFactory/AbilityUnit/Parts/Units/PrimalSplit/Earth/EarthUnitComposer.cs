using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.PrimalSplit.Earth
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.PrimalSplit.Earth.UnitCombo;

    [Export(typeof(IAbilityUnitComposer))]
    [AbilityUnitMetadata("npc_dota_brewmaster_earth_1", "npc_dota_brewmaster_earth_2", "npc_dota_brewmaster_earth_3")]
    internal class EarthUnitComposer : AbilityUnitComposer
    {
        internal EarthUnitComposer()
        {
            this.AssignControllablePart<IUnitCombo>(unit => new EarthCombo(unit));
        }
    }
}
