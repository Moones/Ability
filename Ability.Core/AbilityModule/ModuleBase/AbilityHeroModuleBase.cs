// <copyright file="AbilityHeroModuleBase.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityModule.ModuleBase
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityModule.Combo;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    // [InheritedExport(typeof(IAbilityHeroModule))]
    public abstract class AbilityHeroModuleBase : AbilityModuleBase, IAbilityHeroModule
    {
        #region Fields

        private IAbilityUnit localHero;

        #endregion

        #region Constructors and Destructors

        protected AbilityHeroModuleBase(
            string name,
            string shortDescription,
            bool generateMenu,
            bool enabledByDefault,
            bool loadOnGameStart,
            string heroName)
            : base(name, shortDescription, generateMenu, enabledByDefault, loadOnGameStart, heroName)
        {
            this.HeroName = heroName;
        }

        #endregion

        #region Public Properties

        public string HeroName { get; }

        public override IAbilityUnit LocalHero
        {
            get
            {
                return this.localHero;
            }

            set
            {
                this.localHero = value;
            }
        }

        #endregion

        #region Properties

        private List<OneKeyCombo> Combos { get; } = new List<OneKeyCombo>();

        #endregion

        #region Public Methods and Operators

        public void AddOrbwalker(IUnitOrbwalker orbwalker)
        {
            this.AbilityDataCollector.AddOrbwalker(orbwalker);
        }

        public void RemoveOrbwalker(IUnitOrbwalker orbwalker)
        {
            this.AbilityDataCollector.RemoveOrbwalker(orbwalker);
        }

        public OneKeyCombo NewCombo(
            string name,
            List<IUnitOrbwalker> orbwalkers,
            List<IOrderIssuer> orderIssuers,
            uint key,
            Action targetAssign,
            Action targetReset,
            bool toggle = false,
            string description = null)
        {
            var subMenu = new AbilitySubMenu(name);
            subMenu.AddToMenu(this.Menu);

            foreach (var orbwalker in orbwalkers)
            {
                orderIssuers.Add(orbwalker);
            }

            var combo = new OneKeyCombo(
                orderIssuers,
                subMenu,
                key,
                2000,
                targetAssign,
                targetReset,
                toggle,
                description);
            this.Combos.Add(combo);
            return combo;
        }

        public override void OnClose()
        {
            foreach (var oneKeyCombo in this.Combos)
            {
                oneKeyCombo.Dispose();
            }
        }

        #endregion
    }
}