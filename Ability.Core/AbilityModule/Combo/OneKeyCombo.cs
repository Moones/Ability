// <copyright file="OneKeyCombo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityModule.Combo
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    using Ensage.Common.Menu;

    public class OneKeyCombo : IDisposable
    {
        #region Constructors and Destructors

        public OneKeyCombo(
            List<IOrderIssuer> orderIssuers,
            AbilitySubMenu subMenu,
            uint key,
            float maxTargetDistance,
            Action targetAssign,
            Action targetReset,
            bool toggle = false,
            string description = null)
        {
            this.OrderIssuers = orderIssuers;
            this.SubMenu = subMenu;
            this.MaxTargetDistance = maxTargetDistance;

            var name = "KeyTo" + (toggle ? "Toggle" : "Hold");

            this.Key = new AbilityMenuItem<KeyBind>(
                name,
                new KeyBind(key, toggle ? KeyBindType.Toggle : KeyBindType.Press));

            if (description != null)
            {
                this.SubMenu.AddDescription(description);
            }

            this.Key.NewValueProvider.Subscribe(
                new DataObserver<KeyBind>(
                    bind =>
                        {
                            if (bind.Active)
                            {
                                targetAssign();
                            }
                            else
                            {
                                targetReset();
                            }

                            foreach (var orderIssuer in this.OrderIssuers)
                            {
                                if (bind.Active)
                                {
                                    orderIssuer.Unit.AddOrderIssuer(orderIssuer);
                                }
                                else
                                {
                                    orderIssuer.Unit.RemoveOrderIssuer(orderIssuer);
                                }

                                orderIssuer.Enabled = bind.Active;
                            }
                        }));

            this.Key.AddToMenu(subMenu);
        }

        #endregion

        #region Public Properties

        public AbilityMenuItem<KeyBind> Key { get; }

        public float MaxTargetDistance { get; set; } = 2000;

        public IReadOnlyCollection<IOrderIssuer> OrderIssuers { get; set; }

        public AbilitySubMenu SubMenu { get; }

        public IAbilityUnit Target { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AddOrderIssuer(IOrderIssuer orderIssuer)
        {
            var newList = new List<IOrderIssuer> { orderIssuer };
            newList.AddRange(this.OrderIssuers);
            this.OrderIssuers = newList;
        }

        public void Dispose()
        {
            this.Key.Dispose();
        }

        public void RemoveOrderIssuer(IOrderIssuer orderIssuer)
        {
            var newList = new List<IOrderIssuer>(this.OrderIssuers);
            newList.Remove(orderIssuer);
            this.OrderIssuers = newList;
        }

        #endregion
    }
}