// <copyright file="OrderQueueMenu.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue
{
    using System.ComponentModel.Composition;

    using Ability.Core.MenuManager.GetValue;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    [Export(typeof(IUnitMenu))]
    internal class OrderQueueMenu : IUnitMenu
    {
        #region Fields

        private MenuItem enableDraw;

        #endregion

        #region Constructors and Destructors

        internal OrderQueueMenu()
        {
            this.Menu = new Menu("OrderQueue", Constants.AssemblyName + "OrderQueue");
            this.enableDraw = this.Menu.AddItem(new MenuItem(this.Menu.Name + "DrawOrder", "DrawOrder").SetValue(true));
        }

        #endregion

        #region Public Properties

        public Menu Menu { get; set; }

        #endregion

        #region Public Methods and Operators

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

        #endregion
    }
}