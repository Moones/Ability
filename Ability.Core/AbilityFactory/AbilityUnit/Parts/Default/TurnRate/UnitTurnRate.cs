// <copyright file="UnitTurnRate.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TurnRate
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    using SharpDX;

    public class UnitTurnRate : IUnitTurnRate
    {
        #region Fields

        private float turnRate;

        private float value;

        #endregion

        #region Constructors and Destructors

        public UnitTurnRate(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public bool BatriderStickNapalm { get; set; }

        public bool MedusaStoneGaze { get; set; }

        public IAbilityUnit Unit { get; set; }

        public float Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public double GetTurnTime(Vector3 position)
        {
            var num =
                Math.Abs(
                    Math.Atan2(
                        position.Y - this.Unit.Position.PredictedByLatency.Y,
                        position.X - this.Unit.Position.PredictedByLatency.X) - this.Unit.SourceUnit.RotationRad);
            if (num > Math.PI)
            {
                num = 2.0 * Math.PI - num;
            }

            var angle = (float)num;
            if (this.Unit.SourceUnit.ClassId == ClassId.CDOTA_Unit_Hero_Wisp)
            {
                return 0;
            }

            if (angle <= 0.5f)
            {
                return 0;
            }

            return 0.03f / this.Value * angle;
        }

        public double GetTurnTime(IAbilityUnit target)
        {
            return this.GetTurnTime(target.Position.PredictedByLatency);
        }

        public void Initialize()
        {
            try
            {
                this.turnRate =
                    Game.FindKeyValues(
                        $"{this.Unit.Name}/MovementTurnRate",
                        this.Unit.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;
            }
            catch (KeyValuesNotFoundException)
            {
                this.turnRate = 0.5f;
            }

            this.UpdateValue();

            this.Unit.Modifiers.ModifierAdded.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (modifier.Name == "modifier_medusa_stone_gaze_slow")
                            {
                                this.MedusaStoneGaze = true;
                                this.UpdateValue();
                            }
                            else if (modifier.Name == "modifier_batrider_sticky_napalm")
                            {
                                this.BatriderStickNapalm = true;
                                this.UpdateValue();
                            }
                        }));

            this.Unit.Modifiers.ModifierRemoved.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (modifier.Name == "modifier_medusa_stone_gaze_slow")
                            {
                                this.MedusaStoneGaze = false;
                                this.UpdateValue();
                            }
                            else if (modifier.Name == "modifier_batrider_sticky_napalm")
                            {
                                this.BatriderStickNapalm = false;
                                this.UpdateValue();
                            }
                        }));
        }

        #endregion

        #region Methods

        private void UpdateValue()
        {
            var tempValue = this.turnRate;

            if (this.MedusaStoneGaze)
            {
                tempValue *= 0.65f;
            }

            if (this.BatriderStickNapalm)
            {
                tempValue *= 0.3f;
            }

            this.Value = tempValue;
        }

        #endregion
    }
}