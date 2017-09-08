using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.RuneTaker
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.MenuManager.Menus.AbilityMenu;

    /// <summary>The RuneTaker interface.</summary>
    public interface IRuneTaker : IOrderIssuer
    {
        IAbilityMapData AbilityMapData { get; }

        void ConnectToMenu(AbilityMenu menu, bool addAutoRun, bool addTakeBounty);
    }
}
