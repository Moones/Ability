// <copyright file="AbilityModuleBase.cs" company="EnsageSharp">
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
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityData.AbilityDataCollector;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityFactory;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.MenuManager.Menus.AbilityMenu;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;

    using Ensage.Common.Menu;

    public abstract class AbilityModuleBase :  IAbilityModule
    {
        #region Fields

        private Lazy<IAbilityDataCollector> abilityDataCollectorLazy;

        private Lazy<IAbilityMapData> abilityMapDataLazy;

        private Lazy<IExternalPartManager> externalPartManagerLazy;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AbilityModuleBase" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="shortDescription">The short description.</param>
        /// <param name="generateMenu">The generate menu.</param>
        /// <param name="enabledByDefault">The enabled By Default.</param>
        /// <param name="loadOnGameStart">The load on game start.</param>
        protected AbilityModuleBase(
            string name,
            string shortDescription,
            bool generateMenu,
            bool enabledByDefault,
            bool loadOnGameStart,
            string textureName = null)
        {
            this.ModuleName = name;
            this.Description = shortDescription;
            this.LoadOnGameStart = loadOnGameStart;
            this.GenerateMenu = generateMenu;

            if (this.GenerateMenu)
            {
                this.Menu = new AbilityMenu(name, textureName);
                this.Menu.AddDescription(shortDescription);

                // this.EnableSwitchMenuItem = new AbilityMenuItem<bool>("Enabled", enabledByDefault);
                // this.EnableSwitchMenuItem.AddToMenu(this.Menu);
            }
        }

        #endregion

        #region Public Properties

        public IAbilityDataCollector AbilityDataCollector { get; set; }

        /// <summary>Gets the description.</summary>
        public string Description { get; }

        public AbilityMenuItem<bool> EnableSwitchMenuItem { get; }

        public IExternalPartManager ExternalPartManager { get; set; }

        /// <summary>Gets a value indicating whether generate menu.</summary>
        public bool GenerateMenu { get; }

        /// <summary>Gets a value indicating whether load on game start.</summary>
        public bool LoadOnGameStart { get; }

        /// <summary>Gets or sets the local hero.</summary>
        public virtual IAbilityUnit LocalHero { get; set; }

        /// <summary>Gets or sets the map data.</summary>
        public IAbilityMapData MapData { get; set; }

        public AbilityMenu Menu { get; }

        public string ModuleName { get; }

        #endregion

        #region Properties

        [Import(typeof(IAbilityDataCollector))]
        private Lazy<IAbilityDataCollector> AbilityDataCollectorLazy
        {
            get
            {
                return this.abilityDataCollectorLazy;
            }

            set
            {
                this.abilityDataCollectorLazy = value;
                this.AbilityDataCollector = this.abilityDataCollectorLazy.Value;
            }
        }

        /// <summary>Gets or sets the ability map data lazy.</summary>
        [Import(typeof(IAbilityMapData))]
        private Lazy<IAbilityMapData> AbilityMapDataLazy
        {
            get
            {
                return this.abilityMapDataLazy;
            }

            set
            {
                this.abilityMapDataLazy = value;
                this.MapData = this.AbilityMapDataLazy.Value;
            }
        }

        /// <summary>Gets or sets the external part manager lazy.</summary>
        [Import(typeof(IExternalPartManager))]
        private Lazy<IExternalPartManager> ExternalPartManagerLazy
        {
            get
            {
                return this.externalPartManagerLazy;
            }

            set
            {
                this.externalPartManagerLazy = value;
                this.ExternalPartManager = this.externalPartManagerLazy.Value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The menu.</summary>
        /// <returns>The <see cref="Menu" />.</returns>
        public Menu GetMenu()
        {
            return this.Menu.Menu;
        }

        /// <summary>The on close.</summary>
        public abstract void OnClose();

        /// <summary>The on load.</summary>
        public abstract void OnLoad();

        #endregion
    }
}