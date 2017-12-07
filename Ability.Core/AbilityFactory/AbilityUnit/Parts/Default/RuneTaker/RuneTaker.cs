// <copyright file="RuneTaker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.RuneTaker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.Menus.AbilityMenu;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    public class RuneTaker : IRuneTaker
    {
        #region Fields

        private Vector3 lastOrderPosition;

        private RunePosition<BountyRune> latestBounty;

        private IOrderedEnumerable<RunePosition<BountyRune>> orderedBounties;

        #endregion

        #region Constructors and Destructors

        public RuneTaker(IAbilityUnit unit, IAbilityMapData abilityMapData, bool autoRunToTake)
        {
            this.Unit = unit;
            this.AbilityMapData = abilityMapData;
            this.AutoRunToTake = autoRunToTake;
            this.Menu = new AbilitySubMenu("RuneTaker");
        }

        public RuneTaker(IAbilityUnit unit, IAbilityMapData abilityMapData, bool autoRunToTake, AbilitySubMenu menu)
        {
            this.Unit = unit;
            this.AbilityMapData = abilityMapData;
            this.AutoRunToTake = autoRunToTake;
            this.Menu = menu;
        }

        #endregion

        #region Public Properties

        public IAbilityMapData AbilityMapData { get; }

        public bool AutoRunToTake { get; set; }

        public RunePosition<BountyRune> ClosestBounty { get; private set; }

        public bool Enabled { get; set; }

        public uint Id { get; set; }

        public AbilitySubMenu Menu { get; }

        public Sleeper Sleeper { get; } = new Sleeper();

        public bool TakeBountyOnStart { get; set; }

        public virtual IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual void ConnectToMenu(AbilityMenu menu, bool addAutoRun, bool addTakeBounty)
        {
            this.Menu.AddToMenu(menu);
            var enableItem = new AbilityMenuItem<bool>("Enable", true, "Enable automatic rune taking when in range");
            enableItem.AddToMenu(this.Menu);
            enableItem.NewValueProvider.Subscribe(new DataObserver<bool>(b => this.Enabled = b));
            this.Enabled = enableItem.Value;

            if (addAutoRun)
            {
                var autoRunForRune = new AbilityMenuItem<bool>(
                    "AutoRunForBounty",
                    false,
                    "Run to closest bounty rune spot when its the time");
                autoRunForRune.AddToMenu(this.Menu);
                autoRunForRune.NewValueProvider.Subscribe(new DataObserver<bool>(b => this.AutoRunToTake = b));
                this.AutoRunToTake = autoRunForRune.Value;
            }

            if (addTakeBounty)
            {
                var takeBountyOnStart = new AbilityMenuItem<bool>(
                    "TakeBountyOnStart",
                    false,
                    "Run to bounty rune spot on game start");
                takeBountyOnStart.AddToMenu(this.Menu);
                takeBountyOnStart.NewValueProvider.Subscribe(new DataObserver<bool>(b => this.TakeBountyOnStart = b));
                this.TakeBountyOnStart = takeBountyOnStart.Value;
            }
        }

        public void Dispose()
        {
        }

        public virtual void EnqueueRunForRune(List<Vector3> path)
        {
            this.Unit.OrderQueue.EnqueueOrder(new RunForRune<BountyRune>(this.Unit, this.ClosestBounty, path));
        }

        public virtual void EnqueueRunForRune(RunePosition<BountyRune> rune)
        {
        }

        public virtual void EnqueueRunForRune(RunePosition<PowerUpRune> rune)
        {
        }

        public void Initialize()
        {
        }

        public bool Issue()
        {
            if (!this.Enabled)
            {
                return false;
            }

            if (this.Sleeper.Sleeping)
            {
                return false;
            }

            this.Sleeper.Sleep(300);

            if (this.TakeBountyRune())
            {
                return true;
            }

            if (this.TakePowerUpRune())
            {
                return true;
            }

            return false;
        }

        public bool PreciseIssue()
        {
            if (!this.Enabled)
            {
                return false;
            }

            return false; // throw new NotImplementedException();
        }

        public virtual bool ShouldRunForRune(RunePosition<BountyRune> rune)
        {
            return true;
        }

        public virtual bool ShouldRunForRune(RunePosition<PowerUpRune> rune)
        {
            return true;
        }

        public virtual bool ShouldTakeRune(RunePosition<BountyRune> rune)
        {
            return true;
        }

        public virtual bool ShouldTakeRune(RunePosition<PowerUpRune> rune)
        {
            return true;
        }

        #endregion

        #region Methods

        private bool TakeBountyRune()
        {
            if (this.Unit.Position.PredictedByLatency.Distance2D(this.lastOrderPosition) > 500
                || this.lastOrderPosition == Vector3.Zero)
            {
                var list = new List<RunePosition<BountyRune>>(this.AbilityMapData.BountyRuneSpawner.Positions);

                // if (this.latestBounty != null)
                // {
                // list.Remove(this.latestBounty);
                // }
                foreach (var runePosition in this.AbilityMapData.BountyRuneSpawner.Positions)
                {
                    if (!this.ShouldTakeRune(runePosition))
                    {
                        list.Remove(runePosition);
                    }
                }

                if (list.Any())
                {
                    this.orderedBounties =
                        list.OrderBy(position => position.Position.Distance2D(this.Unit.Position.PredictedByLatency));
                    this.ClosestBounty = this.orderedBounties.First();
                    this.lastOrderPosition = this.Unit.Position.PredictedByLatency;
                    this.latestBounty = null;
                }
            }

            foreach (var runePosition in this.orderedBounties)
            {
                if (runePosition.HasRune && runePosition.CurrentRune != null
                    && this.Unit.Position.PredictedByLatency.Distance2D(runePosition.Position) < 350)
                {
                    if (this.ShouldTakeRune(runePosition))
                    {
                        Console.WriteLine("pickup order sent");
                        this.Unit.OrderQueue.EnqueueOrder(new PickUpRune(this.Unit, runePosition.CurrentRune));
                        this.Sleeper.Sleep(1000);
                        return true;
                    }
                }
            }

            if (!this.AutoRunToTake && (Game.GameTime > 0 || !this.TakeBountyOnStart))
            {
                return false;
            }

            if (this.Unit.Fighting)
            {
                return false;
            }

            List<Vector3> path;
            var walkDuration = this.Unit.Pathfinder.PathDistance(this.ClosestBounty.Position, out path)
                               / this.Unit.SourceUnit.MovementSpeed;
            if (this.ClosestBounty.HasRune && this.ClosestBounty.CurrentRune != null
                && !this.ClosestBounty.CurrentRune.Disposed
                || this.ClosestBounty.NextSpawnTime - Game.GameTime < walkDuration + 5
                && this.ClosestBounty.NextSpawnTime - Game.GameTime >= walkDuration)
            {
                if (this.ShouldRunForRune(this.ClosestBounty))
                {
                    Console.WriteLine("run for rune order sent");
                    this.latestBounty = this.ClosestBounty;
                    this.EnqueueRunForRune(path);
                    this.Sleeper.Sleep(1000);
                    return true;
                }
            }

            return false;
        }

        private bool TakePowerUpRune()
        {
            foreach (var runePosition in this.AbilityMapData.PowerUpRuneSpawner.Positions)
            {
                if (runePosition.HasRune && runePosition.CurrentRune != null
                    && this.Unit.Position.PredictedByLatency.Distance2D(runePosition.Position) < 350)
                {
                    if (this.ShouldTakeRune(runePosition))
                    {
                        Console.WriteLine("pickup order sent");
                        this.Unit.OrderQueue.EnqueueOrder(new PickUpRune(this.Unit, runePosition.CurrentRune));
                        this.Sleeper.Sleep(1000);
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }
}