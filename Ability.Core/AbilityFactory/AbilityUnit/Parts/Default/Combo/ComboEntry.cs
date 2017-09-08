using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ItemManager;

    public class ComboEntry
    {
        public ComboEntry(IAbilitySkill skill)
        {
            this.Cast = skill.CastFunction.Cast;
        }

        public ComboEntry(ItemObserver item)
        {
            this.Cast = () => item.Equipped && item.Item.CastFunction.Cast();
        }

        public Func<bool> Cast { get; }
    }
}
