// <copyright file="Brewmaster.cs" company="EnsageSharp">
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
namespace Ability.Brewmaster
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    
    using Ability.Brewmaster.ChaseCombo;
    using Ability.Brewmaster.RetreatCombo;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.RuneTaker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.ControllableUnits;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityModule.Combo;
    using Ability.Core.AbilityModule.Metadata;
    using Ability.Core.AbilityModule.ModuleBase;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    using Ensage;

    [Export(typeof(IAbilityHeroModule))]
    [AbilityHeroModuleMetadata((uint)HeroId.npc_dota_hero_brewmaster)]
    internal class Brewmaster : AbilityHeroModuleBase
    {
        #region Constructors and Destructors

        public Brewmaster()
            : base("Brewmaster", "Module utilizing hero Brewmaster", true, true, true, "npc_dota_hero_brewmaster")
        {
        }

        #endregion

        #region Public Properties

        public OneKeyCombo ChaseCombo { get; set; }

        public BrewmasterOrbwalker BrewmasterOrbwalker { get; set; }

        public OneKeyCombo RetreatCombo { get; set; }

        #endregion

        #region Public Methods and Operators

        public override void OnLoad()
        {
            this.BrewmasterOrbwalker = new BrewmasterOrbwalker(this.LocalHero);
            var brewRuneTaker = new RuneTaker(this.LocalHero, this.MapData, false);
            if (Game.ShortLevelName == "start")
            {
                this.LocalHero.AddOrderIssuer(brewRuneTaker);
            }

            this.AddOrbwalker(this.BrewmasterOrbwalker);

            var orbwalkers = new List<IUnitOrbwalker> { this.BrewmasterOrbwalker };
            var orderIssuers = new List<IOrderIssuer>();

            var targetReset = new Action(() => { this.LocalHero.TargetSelector.ResetTarget(); });

            var targetAssign = new Action(() => { this.LocalHero.TargetSelector.GetTarget(); });

            this.LocalHero.TargetSelector.TargetChanged.Subscribe(
                () =>
                    {
                        foreach (var controllableUnitsUnit in this.LocalHero.ControllableUnits.Units)
                        {
                            controllableUnitsUnit.Value.TargetSelector.Target = this.LocalHero.TargetSelector.Target;
                        }
                    });

            this.ChaseCombo = this.NewCombo(
                "ChaseCombo",
                orbwalkers,
                orderIssuers,
                'G',
                targetAssign,
                targetReset,
                false,
                "In this combo Brewmaster will chase enemy");

            //this.RetreatCombo = this.NewCombo(
            //    "RetreatCombo",
            //    new List<IUnitOrbwalker> { new BrewmasterRetreatOrbwalker(this.LocalHero) },
            //    new List<IOrderIssuer>(),
            //    'H',
            //    () => { this.Bear?.TargetSelector.GetTarget(); },
            //    () => { this.Bear?.TargetSelector.ResetTarget(); },
            //    false,
            //    "lone will run to mouse, bear will attack/bodyblock target or run if low hp");

            this.LocalHero.ControllableUnits.AddedUnit.Subscribe(new DataObserver<IAbilityUnit>(unit => this.UnitAdded(unit)));
            this.LocalHero.ControllableUnits.RemovedUnit.Subscribe(new DataObserver<IAbilityUnit>(unit => this.UnitRemoved(unit)));

            var runeTakerMenu = new AbilitySubMenu("RuneTaker");
            runeTakerMenu.AddToMenu(this.Menu);

            brewRuneTaker.ConnectToMenu(runeTakerMenu, false, false);
            
            foreach (var controllableUnitsUnit in this.LocalHero.ControllableUnits.Units)
            {
                this.UnitAdded(controllableUnitsUnit.Value);
            }
        }

        private void UnitAdded(IAbilityUnit unit)
        {
            //unit.TargetSelector.Target = this
            if (unit.PrettyName == "Earth")
            {
                unit.AddPart<IUnitOrbwalker>(abilityUnit => new EarthOrbwalker(unit));
            }
            else if (unit.UnitCombo != null)
            {
                unit.AddPart<IUnitOrbwalker>(abilityUnit => new ControllableUnitSpellsOrbwalker(unit));
            }
            else
            {
                unit.AddPart<IUnitOrbwalker>(abilityUnit => new ControllableUnitOrbwalker(unit));
            }

            this.AddOrbwalker(unit.Orbwalker);

            this.ChaseCombo.AddOrderIssuer(unit.Orbwalker);
            //this.RetreatCombo.AddOrderIssuer(unit.Orbwalker);

            if (this.ChaseCombo.Key.Value.Active)
            {
                unit.AddOrderIssuer(unit.Orbwalker);
                unit.Orbwalker.Enabled = true;
            }

            unit.Fighting = this.LocalHero.Fighting;
            unit.TargetSelector.Target = this.LocalHero.TargetSelector.Target;

        }

        private void UnitRemoved(IAbilityUnit unit)
        {
            this.RemoveOrbwalker(unit.Orbwalker);

            this.ChaseCombo.RemoveOrderIssuer(unit.Orbwalker);
            //this.RetreatCombo.RemoveOrderIssuer(unit.Orbwalker);
            //this.BodyBlockCombo.RemoveOrderIssuer(unit.Orbwalker);
        }

        #endregion
    }
}