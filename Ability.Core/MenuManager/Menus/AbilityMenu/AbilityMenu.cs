// <copyright file="AbilityMenu.cs" company="EnsageSharp">
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
namespace Ability.Core.MenuManager.Menus.AbilityMenu
{
    using System.Collections.Generic;

    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    using Ensage.Common.Menu;

    public class AbilityMenu : IAbilityMenu
    {
        #region Fields

        private readonly Dictionary<string, IAbilityMenuItem> menuItems = new Dictionary<string, IAbilityMenuItem>();

        private readonly Dictionary<string, AbilitySubMenu> submenus = new Dictionary<string, AbilitySubMenu>();

        #endregion

        #region Constructors and Destructors

        public AbilityMenu(string name, string textureName = null)
        {
            this.Name = name;
            this.Menu = textureName != null
                            ? new Menu(name, Constants.AssemblyName + name, false, textureName, true)
                            : new Menu(name, Constants.AssemblyName + name);
        }

        #endregion

        #region Public Properties

        public IReadOnlyDictionary<string, IAbilityMenuItem> MenuItems => this.menuItems;

        public string Name { get; }

        public IReadOnlyDictionary<string, AbilitySubMenu> Submenus => this.submenus;

        #endregion

        #region Properties

        internal Menu Menu { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AddDescription(string description)
        {
            this.Menu.AddItem(
                new MenuItem(this.Menu.Name + "Description", "Description (hover mouse)").SetTooltip(description));
        }

        #endregion
    }
}