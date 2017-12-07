// <copyright file="DamageManipulation.cs" company="EnsageSharp">
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
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    using SharpDX;

    internal class DamageManipulation : Body
    {
        #region Fields

        private ICollection<DamageManipulationEntry> entries = new List<DamageManipulationEntry>();

        private DamageManipulationEntry entry;

        private IAbilityUnit localHero;

        #endregion

        #region Constructors and Destructors

        internal DamageManipulation(Vector2 size, Vector2 position, IAbilityManager abilityManager)
            : base(size, position)
        {
            // foreach (var unitManagerUnit in abilityManager.LocalTeam.UnitManager.Units)
            // {
            // this.entries.Add(new DamageManipulationEntry(unitManagerUnit.Value));
            // }
            foreach (var abilityManagerTeam in abilityManager.Teams)
            {
                abilityManagerTeam.UnitManager.UnitAdded.Subscribe(
                    new DataObserver<IAbilityUnit>(
                        unit =>
                            {
                                this.entries.Add(new DamageManipulationEntry(unit) { Size = this.Size });
                                this.UpdatePosition();
                            }));
            }

            // this.entry = new DamageManipulationEntry(this.localHero);
            // this.entry.Size = this.Size;
            // this.UpdatePosition();
        }

        #endregion

        #region Public Methods and Operators

        public override void DrawElements()
        {
            foreach (var damageManipulationEntry in this.entries)
            {
                damageManipulationEntry.Draw();
            }
        }

        public override sealed void UpdatePosition()
        {
            var basePos = this.Position;
            foreach (var damageManipulationEntry in this.entries)
            {
                damageManipulationEntry.Position = basePos;
                basePos += new Vector2(damageManipulationEntry.Size.X, 0);
                if (basePos.X - this.Position.X + damageManipulationEntry.Size.X > this.Size.X)
                {
                    basePos = new Vector2(this.Position.X, basePos.Y + (float)(damageManipulationEntry.Size.Y * 1.1));
                }
            }

            // if (this.entry == null)
            // {
            // return;
            // }

            // this.entry.Position = this.Position;
        }

        #endregion
    }
}