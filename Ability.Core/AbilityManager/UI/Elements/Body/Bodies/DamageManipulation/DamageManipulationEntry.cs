// <copyright file="DamageManipulationEntry.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager.UI.Elements.Body.Bodies.DamageManipulation
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    internal class DamageManipulationEntry : DrawObject
    {
        #region Fields

        private Vector2 position;

        private Vector2 size;

        private IAbilityUnit unit;

        private DrawText unitNameText;

        private ICollection<DamageManipulationValueEntry> values = new List<DamageManipulationValueEntry>();

        #endregion

        #region Constructors and Destructors

        internal DamageManipulationEntry(IAbilityUnit unit)
        {
            this.unit = unit;

            // if (this.unit == null)
            this.values.Add(
                new DamageManipulationValueEntry(
                    "DamageAmpli",
                    () => unit.DamageManipulation.DamageAmplification.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "DamageReduce",
                    () => unit.DamageManipulation.DamageReduction.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "DamageNegate",
                    () => unit.DamageManipulation.DamageNegation.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry("DamageBlock", () => unit.DamageManipulation.DamageBlock.ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "MagDmgAbsorb",
                    () => unit.DamageManipulation.MagicalDamageAbsorb.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "ManaShield",
                    () => unit.DamageManipulation.ManaShield.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "IceBlast",
                    () =>
                        unit.DamageManipulation.Aa?.GetSpecialValue(unit, unit.Health.Current).ToString()
                        ?? 0.ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "MagicDmgShield",
                    () => unit.DamageManipulation.MagicalDamageShield.ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "PureDamageShield",
                    () => unit.DamageManipulation.PureDamageShield.ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "PhysicalDamageShield",
                    () => unit.DamageManipulation.PhysicalDamageShield.ToString()));

            this.unitNameText = new DrawText
                                    {
                                       Color = Color.GreenYellow, Shadow = true, Text = Game.Localize(unit.Name) 
                                    };
        }

        #endregion

        #region Public Properties

        public override Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                var basePos = this.position;
                this.unitNameText.Position = basePos;
                basePos += new Vector2(0, this.unitNameText.Size.Y);
                foreach (var damageManipulationValueEntry in this.values)
                {
                    damageManipulationValueEntry.Position = basePos;
                    basePos += new Vector2(0, damageManipulationValueEntry.Size.Y);
                }
            }
        }

        public override Vector2 Size
        {
            get
            {
                return this.size;
            }

            set
            {
                var textSize = value / 5;
                var entrySize = new Vector2(textSize.X / this.values.Count, textSize.Y / this.values.Count);
                this.size = new Vector2(0);
                foreach (var damageManipulationValueEntry in this.values)
                {
                    damageManipulationValueEntry.Size = entrySize * (float)0.9;
                    this.size = new Vector2(
                        Math.Max(this.size.X, damageManipulationValueEntry.Size.X),
                        this.size.Y + damageManipulationValueEntry.Size.Y);
                }

                this.unitNameText.TextSize = entrySize;
                this.size += new Vector2(0, this.unitNameText.Size.Y);
            }
        }

        #endregion

        #region Public Methods and Operators

        public override void Draw()
        {
            this.unitNameText.Draw();
            foreach (var damageManipulationValueEntry in this.values)
            {
                damageManipulationValueEntry.Draw();
            }
        }

        #endregion
    }
}