using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.KeyTrigger
{
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;

    using Ensage.Common.Menu;

    public interface IUnitKeyTrigger : IAbilityUnitPart
    {
        bool Activated { get; }

        void AddKeyBind(AbilityMenuItem<KeyBind> keyBind);

        void RemoveKeyBind(AbilityMenuItem<KeyBind> keyBind);
    }
}
