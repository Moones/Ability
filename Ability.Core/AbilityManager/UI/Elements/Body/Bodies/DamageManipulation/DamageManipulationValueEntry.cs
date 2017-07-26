// <copyright file="DamageManipulationValueEntry.cs" company="EnsageSharp">
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

    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    public class DamageManipulationValueEntry : DrawObject
    {
        #region Fields

        private DrawText nameText;

        private Vector2 position;

        private Vector2 size;

        private DrawText valueText;

        #endregion

        #region Constructors and Destructors

        public DamageManipulationValueEntry(string name, Func<string> getValue)
        {
            this.Name = name;
            this.GetValue = getValue;

            this.nameText = new DrawText { Color = Color.White, Text = name };
            this.valueText = new DrawText { Color = Color.White, Text = getValue() };
        }

        #endregion

        #region Public Properties

        public Func<string> GetValue { get; set; }

        public string Name { get; set; }

        public override Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                this.nameText.Position = this.position;
                this.valueText.Position = this.position + new Vector2((float)(this.nameText.Size.X * 1.1), 0);
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
                this.nameText.TextSize = value;
                this.valueText.TextSize = value;
                this.size = new Vector2(
                    (float)(this.nameText.Size.X * 1.3 + this.valueText.Size.X),
                    this.nameText.Size.Y);
            }
        }

        #endregion

        #region Public Methods and Operators

        public override void Draw()
        {
            this.nameText.Draw();
            this.valueText.Text = this.GetValue();
            this.valueText.Draw();
        }

        #endregion
    }
}