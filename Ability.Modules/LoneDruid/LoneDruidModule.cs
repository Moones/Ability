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
namespace Ability.Lycan
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Bodyblocker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.RuneTaker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.ControllableUnits;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityModule.Combo;
    using Ability.Core.AbilityModule.Metadata;
    using Ability.Core.AbilityModule.ModuleBase;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;
    using Ability.LoneDruid.ChaseCombo;
    using Ability.LoneDruid.RuneTaker;

    using Ensage;

    using Ability.Lycan.BodyblockCombo;
    using Ability.Lycan.ChaseCombo;
    using Ability.Lycan.RetreatCombo;

    [Export(typeof(IAbilityHeroModule))]
    [AbilityHeroModuleMetadata((uint)HeroId.npc_dota_hero_lone_druid)]
    internal class LoneDruidModule : AbilityHeroModuleBase
    {
        #region Constructors and Destructors

        public LoneDruidModule()
            : base("LoneDruid", "Module utilizing hero LoneDruid", true, true, true, "npc_dota_hero_lone_druid")
        {
        }

        #endregion

        #region Public Properties

        public IAbilityUnit Bear { get; set; }

        public UnitBodyblocker BearBodyblocker { get; set; }

        public BearOrbwalker BearOrbwalker { get; set; }

        public BearRetreatOrbwalker BearRetreatOrbwalker { get; set; }

        public OneKeyCombo BodyBlockCombo { get; set; }

        public OneKeyCombo ChaseCombo { get; set; }

        public LoneDruidOrbwalker LoneDruidOrbwalker { get; set; }

        public OneKeyCombo RetreatCombo { get; set; }

        #endregion

        #region Public Methods and Operators

        public override void OnLoad()
        {
            this.LoneDruidOrbwalker = new LoneDruidOrbwalker(this.LocalHero);
            var loneRuneTaker = new RuneTaker(this.LocalHero, this.MapData, false);
            if (Game.ShortLevelName == "start")
            {
                this.LocalHero.AddOrderIssuer(loneRuneTaker);
            }

            this.AddOrbwalker(this.LoneDruidOrbwalker);

            var orbwalkers = new List<IUnitOrbwalker> { this.LoneDruidOrbwalker };
            var orderIssuers = new List<IOrderIssuer>();

            var targetReset = new Action(() => { this.LocalHero.TargetSelector.ResetTarget(); });

            var targetAssign = new Action(() => { this.LocalHero.TargetSelector.GetTarget(); });

            this.LocalHero.TargetSelector.TargetChanged.Subscribe(
                () =>
                    {
                        foreach (var localHeroControllableUnit in this.LocalHero.ControllableUnits.Units)
                        {
                            localHeroControllableUnit.Value.TargetSelector.Target = this.LocalHero.TargetSelector.Target;
                        }

                        if (this.Bear == null)
                        {
                            return;
                        }

                        this.Bear.TargetSelector.Target = this.LocalHero.TargetSelector.Target;
                    });

            this.BodyBlockCombo = this.NewCombo(
                "BodyBlockCombo",
                orbwalkers,
                orderIssuers,
                'B',
                targetAssign,
                targetReset,
                false,
                "bear will bodyblock, lone druid will attack");

            this.ChaseCombo = this.NewCombo(
                "ChaseCombo",
                orbwalkers,
                orderIssuers,
                'G',
                targetAssign,
                targetReset,
                false,
                "bear will chase enemy, lone will attack and move to mouse and keep range if not in true form");

            this.BearRetreatOrbwalker = new BearRetreatOrbwalker { LocalHero = this.LocalHero };

            this.RetreatCombo = this.NewCombo(
                "RetreatCombo",
                new List<IUnitOrbwalker> { new LoneDruidRetreatOrbwalker(this.LocalHero) },
                new List<IOrderIssuer>(),
                'H',
                () =>
                    {
                        this.Bear?.TargetSelector.GetTarget();
                        foreach (var localHeroControllableUnit in this.LocalHero.ControllableUnits.Units)
                        {
                            localHeroControllableUnit.Value.TargetSelector.GetTarget();
                        }
                    },
                () =>
                    {
                        this.Bear?.TargetSelector.ResetTarget();
                        foreach (var localHeroControllableUnit in this.LocalHero.ControllableUnits.Units)
                        {
                            localHeroControllableUnit.Value.TargetSelector.ResetTarget();
                        }
                    },
                false,
                "lone will run to mouse, bear will attack/bodyblock target or run if low hp");

            this.BearRetreatOrbwalker.LowHp.AddToMenu(this.RetreatCombo.SubMenu);

            this.controllableUnits = this.LocalHero.ControllableUnits as LoneDruidControllableUnits;
            this.controllableUnits.BearAdded.Subscribe(this.BearAdded);

            this.controllableUnits.AddedUnit.Subscribe(new DataObserver<IAbilityUnit>(unit => this.UnitAdded(unit)));
            this.controllableUnits.RemovedUnit.Subscribe(new DataObserver<IAbilityUnit>(unit => this.UnitRemoved(unit)));

            var runeTakerMenu = new AbilitySubMenu("RuneTaker");
            runeTakerMenu.AddToMenu(this.Menu);

            this.BearRuneTaker = new BearRuneTaker(null, this.MapData);
            this.BearRuneTaker.ConnectToMenu(runeTakerMenu, true, true);

            loneRuneTaker.ConnectToMenu(runeTakerMenu, false, false);


            if (this.controllableUnits.Bear != null)
            {
                this.BearAdded();
            }

            foreach (var controllableUnitsUnit in this.controllableUnits.Units)
            {
                this.UnitAdded(controllableUnitsUnit.Value);
            }
        }

        private LoneDruidControllableUnits controllableUnits;

        private BearRuneTaker BearRuneTaker;



        private void UnitAdded(IAbilityUnit unit)
        {
           //unit.TargetSelector.Target = this
            if (unit.UnitCombo != null)
            {
                unit.AddPart<IUnitOrbwalker>(abilityUnit => new ControllableUnitSpellsOrbwalker(unit));
            }
            else
            {
                unit.AddPart<IUnitOrbwalker>(abilityUnit => new ControllableUnitOrbwalker(unit));
            }

            this.AddOrbwalker(unit.Orbwalker);
            
            //unit.TargetSelector.Target = this.LocalHero.TargetSelector.Target;
            //unit.Fighting = this.LocalHero.Fighting;

            this.BodyBlockCombo.AddOrderIssuer(unit.Orbwalker);
            this.ChaseCombo.AddOrderIssuer(unit.Orbwalker);
            this.RetreatCombo.AddOrderIssuer(unit.Orbwalker);

            if (this.RetreatCombo.Key.Value.Active || this.ChaseCombo.Key.Value.Active || this.BodyBlockCombo.Key.Value.Active)
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
            this.RetreatCombo.RemoveOrderIssuer(unit.Orbwalker);
            this.BodyBlockCombo.RemoveOrderIssuer(unit.Orbwalker);
        }

        private void BearAdded()
        {
            if (this.Bear != null)
            {
                this.controllableUnits.Bear.TargetSelector.Target = this.Bear.TargetSelector.Target;
                this.controllableUnits.Bear.Fighting = this.LocalHero.Fighting;
            }

            this.Bear = this.controllableUnits.Bear;
            this.controllableUnits.Bear.Fighting = this.LocalHero.Fighting;

            if (this.BearBodyblocker == null)
            {
                this.BearBodyblocker = new BearBodyblocker(this.Bear);
                this.BearOrbwalker = new BearOrbwalker { LocalHero = this.LocalHero, Unit = this.Bear };
                this.BearRetreatOrbwalker.Unit = this.Bear;

                this.AddOrbwalker(this.BearOrbwalker);
                this.AddOrbwalker(this.BearRetreatOrbwalker);

                this.BodyBlockCombo.AddOrderIssuer(this.BearBodyblocker);
                this.ChaseCombo.AddOrderIssuer(this.BearOrbwalker);
                this.RetreatCombo.AddOrderIssuer(this.BearRetreatOrbwalker);
                if (Game.ShortLevelName == "start")
                {
                    this.BearRuneTaker.Unit = this.Bear;
                    this.Bear.AddOrderIssuer(this.BearRuneTaker);
                }
            }
            else
            {
                this.BearBodyblocker.Unit = this.Bear;
                this.BearOrbwalker.Unit = this.Bear;
                this.BearRetreatOrbwalker.Unit = this.Bear;
                this.BearRuneTaker.Unit = this.Bear;

                if (this.BearBodyblocker.Enabled)
                {
                    this.Bear.AddOrderIssuer(this.BearBodyblocker);
                }

                if (this.BearOrbwalker.Enabled)
                {
                    this.Bear.AddOrderIssuer(this.BearOrbwalker);
                }

                if (this.BearRetreatOrbwalker.Enabled)
                {
                    this.Bear.AddOrderIssuer(this.BearRetreatOrbwalker);
                }

                if (Game.ShortLevelName == "start")
                {
                    this.Bear.AddOrderIssuer(this.BearRuneTaker);
                }
            }

            this.LoneDruidOrbwalker.Bear = this.Bear;
        }
        
        #endregion
    }
}