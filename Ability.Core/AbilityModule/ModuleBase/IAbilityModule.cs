// <copyright file="IAbilityModule.cs" company="EnsageSharp">
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
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityService;
    using Ability.Core.MenuManager.Menus.AbilityMenu;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;

    /// <summary>The AbilityModule interface.</summary>
    public interface IAbilityModule : IAbilityService
    {
        #region Public Properties

        string Description { get; }

        AbilityMenuItem<bool> EnableSwitchMenuItem { get; }

        bool LoadOnGameStart { get; }

        IAbilityUnit LocalHero { get; set; }

        IAbilityMapData MapData { get; set; }

        AbilityMenu Menu { get; }

        string ModuleName { get; }

        #endregion
    }
}