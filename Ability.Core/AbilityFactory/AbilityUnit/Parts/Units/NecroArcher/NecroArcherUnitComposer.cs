using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.NecroArcher
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.NecroArcher.UnitCombo;

    [Export(typeof(IAbilityUnitComposer))]
    [AbilityUnitMetadata("npc_dota_necronomicon_archer_1", "npc_dota_necronomicon_archer_2", "npc_dota_necronomicon_archer_3")]
    internal class NecroArcherUnitComposer : AbilityUnitComposer
    {
        internal NecroArcherUnitComposer()
        {
            this.AssignControllablePart<IUnitCombo>(unit => new NecroArcherUnitCombo(unit));
        }
    }
}
