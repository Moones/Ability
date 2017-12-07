// <copyright file="LycanModule.cs" company="EnsageSharp">
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
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Lycan.ControllableUnits;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityModule.Combo;
    using Ability.Core.AbilityModule.Metadata;
    using Ability.Core.AbilityModule.ModuleBase;
    using Ability.Lycan.ChaseCombo;
    using Ability.Lycan.RetreatCombo;

    using Ensage;

    [Export(typeof(IAbilityHeroModule))]
    [AbilityHeroModuleMetadata((uint)HeroId.npc_dota_hero_lycan)]
    internal class LycanModule : AbilityHeroModuleBase
    {
        #region Fields

        private LycanControllableUnits controllableUnits;

        #endregion

        #region Constructors and Destructors

        public LycanModule()
            : base("Lycan", "Module utilizing hero Lycan", true, true, true, "npc_dota_hero_lycan")
        {
        }

        #endregion

        #region Public Properties

        public OneKeyCombo ChaseCombo { get; set; }

        public LycanOrbwalker LycanOrbwalker { get; set; }

        public OneKeyCombo RetreatCombo { get; set; }

        #endregion

        #region Public Methods and Operators

        public override void OnLoad()
        {
            this.LycanOrbwalker = new LycanOrbwalker(this.LocalHero);

            // var loneRuneTaker = new RuneTaker(this.LocalHero, this.MapData, false);
            // this.LocalHero.AddOrderIssuer(loneRuneTaker);
            this.AddOrbwalker(this.LycanOrbwalker);

            var orbwalkers = new List<IUnitOrbwalker> { this.LycanOrbwalker };
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
                "everyone chases enemy, 1 wolf will bodyblock");

            this.RetreatCombo = this.NewCombo(
                "RetreatCombo",
                new List<IUnitOrbwalker> { new LycanRetreatOrbwalker(this.LocalHero) },
                new List<IOrderIssuer>(),
                'H',
                targetAssign,
                targetReset,
                false,
                "lycan will run to mouse, wolves will attack/bodyblock target");

            this.controllableUnits = this.LocalHero.ControllableUnits as LycanControllableUnits;

            this.controllableUnits.AddedUnit.Subscribe(new DataObserver<IAbilityUnit>(unit => this.UnitAdded(unit)));
            this.controllableUnits.RemovedUnit.Subscribe(new DataObserver<IAbilityUnit>(unit => this.UnitRemoved(unit)));

            foreach (var controllableUnitsUnit in this.controllableUnits.Units)
            {
                this.UnitAdded(controllableUnitsUnit.Value);
            }
        }

        #endregion

        #region Methods

        private void UnitAdded(IAbilityUnit unit)
        {
            // unit.TargetSelector.Target = this
            if (unit.Name == "npc_dota_lycan_wolf1" || unit.Name == "npc_dota_lycan_wolf2"
                || unit.Name == "npc_dota_lycan_wolf3" || unit.Name == "npc_dota_lycan_wolf4")
            {
                unit.AddPart<IUnitOrbwalker>(abilityUnit => new WolfOrbwalker(unit));
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

            unit.TargetSelector.Target = this.LocalHero.TargetSelector.Target;
            unit.Fighting = this.LocalHero.Fighting;

            this.ChaseCombo.AddOrderIssuer(unit.Orbwalker);
            this.RetreatCombo.AddOrderIssuer(unit.Orbwalker);

            if (this.RetreatCombo.Key.Value.Active || this.ChaseCombo.Key.Value.Active)
            {
                unit.AddOrderIssuer(unit.Orbwalker);
                unit.Orbwalker.Enabled = true;
            }
        }

        private void UnitRemoved(IAbilityUnit unit)
        {
            this.RemoveOrbwalker(unit.Orbwalker);

            this.ChaseCombo.RemoveOrderIssuer(unit.Orbwalker);
            this.RetreatCombo.RemoveOrderIssuer(unit.Orbwalker);
        }

        #endregion
    }
}