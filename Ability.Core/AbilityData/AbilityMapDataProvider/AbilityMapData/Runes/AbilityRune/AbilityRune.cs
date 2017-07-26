// <copyright file="AbilityRune.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune
{
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public abstract class AbilityRune : IAbilityRune
    {
        #region Constructors and Destructors

        protected AbilityRune(Rune sourceRune)
        {
            this.SourceRune = sourceRune;
            this.Name = this.SourceRune.Name;
            this.Handle = this.SourceRune.Handle;
            this.TypeName = Game.Localize(this.SourceRune.RuneType.ToString());
            this.PickUpRange = 200;
        }

        #endregion

        #region Public Properties

        public double Handle { get; }

        public string Name { get; }

        public float PickUpRange { get; }

        public Notifier RuneDisposed { get; } = new Notifier();

        public Rune SourceRune { get; }

        public string TypeName { get; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.RuneDisposed.Notify();
            this.RuneDisposed.Dispose();
        }

        #endregion
    }
}