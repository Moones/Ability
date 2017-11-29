// <copyright file="LoneDruidModule.cs" company="EnsageSharp">
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
namespace Ability.Invoker
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityModule.Metadata;
    using Ability.Core.AbilityModule.ModuleBase;

    using Ensage;

    [Export(typeof(IAbilityHeroModule))]
    [AbilityHeroModuleMetadata((uint)HeroId.npc_dota_hero_invoker)]
    internal class LoneDruidModule : AbilityHeroModuleBase
    {
        #region Constructors and Destructors

        public LoneDruidModule()
            : base("Invoker", "Module utilizing hero Invoker", true, true, true, "npc_dota_hero_invoker")
        {
        }

        #endregion

       #region Public Methods and Operators

        public override void OnLoad()
        {
            this.NewKey("OneKeyCombo", 'G', this.OneyKeyComboActivate, this.OneyKeyComboDeactivate);
        }

        private void OneyKeyComboActivate()
        {
            

        }

        private void OneyKeyComboDeactivate()
        {

        }



        private void UnitAdded(IAbilityUnit unit)
        {
           
        }

        private void UnitRemoved(IAbilityUnit unit)
        {
        }
        
        #endregion
    }
}