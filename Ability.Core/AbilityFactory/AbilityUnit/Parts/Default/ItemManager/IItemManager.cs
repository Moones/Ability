using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ItemManager
{
    using Ability.Core.AbilityFactory.AbilitySkill;

    public interface IItemManager : IAbilityUnitPart
    {
        ItemObserver PhaseBoots { get; }
        ItemObserver AbyssalBlade { get; }
        ItemObserver Mjollnir { get; }
        ItemObserver TravelBoots { get; }

        IReadOnlyDictionary<double, IAbilitySkill> Items { get; }

        void ItemAdded(IAbilitySkill item);

        void ItemRemoved(IAbilitySkill item);
    }
}
