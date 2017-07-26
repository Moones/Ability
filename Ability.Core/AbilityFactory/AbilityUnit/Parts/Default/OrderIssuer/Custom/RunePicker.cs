// <copyright file="RunePicker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer.Custom
{
    using System;

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common.Extensions;

    public class RunePicker : IOrderIssuer
    {
        #region Fields

        private DataObserver<BountyRune> newBountyRuneObserver = new DataObserver<BountyRune>();

        private DataObserver<PowerUpRune> newPowerUpRuneObserver = new DataObserver<PowerUpRune>();

        private PickUpRune pickUpRune;

        #endregion

        #region Constructors and Destructors

        public RunePicker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public bool Enabled { get; set; }

        public uint Id { get; set; }

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AutomaticRunePicking(bool enable)
        {
            if (enable)
            {
                this.pickUpRune = new PickUpRune(this.Unit);
                this.newBountyRuneObserver = new DataObserver<BountyRune>(
                    rune =>
                        {
                            var distance = VectorExtensions.Distance(
                                this.Unit.Position.PredictedByLatency,
                                rune.SourceRune.Position);
                            if (distance < 1000)
                            {
                                this.pickUpRune.AssignRune(rune);

                                // this.Unit.OrderQueue.EnqueueOrder(this.pickUpRune);
                            }
                        });

                this.newPowerUpRuneObserver = new DataObserver<PowerUpRune>(
                    rune =>
                        {
                            var distance = VectorExtensions.Distance(
                                this.Unit.Position.PredictedByLatency,
                                rune.SourceRune.Position);
                            if (distance < 1000)
                            {
                                this.pickUpRune.AssignRune(rune);

                                // this.Unit.OrderQueue.EnqueueOrder(this.pickUpRune);
                            }
                        });

                // this.newPowerUpRuneObserver.Subscribe(this.MapData.PowerUpRuneSpawner.NewRuneProvider);
                // Console.WriteLine(this.MapData);
                // this.newBountyRuneObserver.Subscribe(this.MapData.BountyRuneSpawner.NewRuneProvider);
            }
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {
        }

        public bool Issue()
        {
            throw new NotImplementedException();
        }

        public bool PreciseIssue()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}