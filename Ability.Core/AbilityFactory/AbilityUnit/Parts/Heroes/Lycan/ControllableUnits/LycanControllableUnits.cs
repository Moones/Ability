// <copyright file="LycanControllableUnits.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Lycan.ControllableUnits
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits;
    using Ability.Core.Utilities;

    using Ensage.Common.Extensions;

    public class LycanControllableUnits : ControllableUnits
    {
        #region Fields

        private readonly Dictionary<double, IAbilityUnit> wolves = new Dictionary<double, IAbilityUnit>();

        private uint closestWolfHandle;

        private Sleeper closestWolfSleeper = new Sleeper();

        #endregion

        #region Constructors and Destructors

        public LycanControllableUnits(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        public IReadOnlyDictionary<double, IAbilityUnit> Wolves => this.wolves;

        #endregion

        #region Public Methods and Operators

        public override void Initialize()
        {
            base.Initialize();
        }

        public bool IsClosestWolf(IAbilityUnit wolf)
        {
            if (!this.Unit.TargetSelector.TargetIsSet || this.Unit.TargetSelector.Target == null)
                if (this.closestWolfSleeper.Sleeping)
                {
                    return wolf.UnitHandle.Equals(this.closestWolfHandle);
                }

            var closestWolf =
                this.wolves.MinOrDefault(
                    x =>
                        x.Value.Position.PredictedByLatency.Distance2D(
                            this.Unit.TargetSelector.Target.Position.PredictedByLatency)).Value;

            if (closestWolf == null)
            {
                return false;
            }

            this.closestWolfHandle = closestWolf.UnitHandle;
            this.closestWolfSleeper.Sleep(500);

            return closestWolf.UnitHandle.Equals(wolf.UnitHandle);
        }

        public override void UnitAdded(IAbilityUnit unit)
        {
            if (unit.Name == "npc_dota_lycan_wolf1" || unit.Name == "npc_dota_lycan_wolf2"
                || unit.Name == "npc_dota_lycan_wolf3" || unit.Name == "npc_dota_lycan_wolf4")
            {
                this.wolves.Add(unit.UnitHandle, unit);
            }

            base.UnitAdded(unit);
        }

        public override void UnitRemoved(IAbilityUnit unit)
        {
            if (unit.Name == "npc_dota_lycan_wolf1" || unit.Name == "npc_dota_lycan_wolf2"
                || unit.Name == "npc_dota_lycan_wolf3" || unit.Name == "npc_dota_lycan_wolf4")
            {
                this.wolves.Remove(unit.UnitHandle);
            }

            base.UnitRemoved(unit);
        }

        #endregion
    }
}