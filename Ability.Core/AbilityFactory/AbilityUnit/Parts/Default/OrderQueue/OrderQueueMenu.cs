using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue
{
    using System.ComponentModel.Composition;

    using Ability.Core.MenuManager.GetValue;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    [Export(typeof(IUnitMenu))]
    internal class OrderQueueMenu : IUnitMenu
    {
        private MenuItem enableDraw;
        internal OrderQueueMenu()
        {
            this.Menu = new Menu("OrderQueue", Constants.AssemblyName + "OrderQueue");
            this.enableDraw = this.Menu.AddItem(new MenuItem(this.Menu.Name + "DrawOrder", "DrawOrder").SetValue(true));
        }

        public Menu Menu { get; set; }

        public void AddToMenu(Menu parentMenu)
        {
            parentMenu.AddSubMenu(this.Menu);
        }

        public void ConnectToUnit(IAbilityUnit unit)
        {
            if (unit.OrderQueue != null)
            {
                unit.OrderQueue.DrawOrder = new GetValue<bool, bool>(this.enableDraw, b => b);
            }
        }
    }
}
