// <copyright file="LycanModifiers.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Lycan.Modifiers
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;

    using Ensage;

    public class LycanModifiers : Modifiers
    {
        #region Constructors and Destructors

        public LycanModifiers(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        public bool Shapeshift { get; private set; }

        #endregion

        #region Public Methods and Operators

        public override void AddModifier(Modifier modifier)
        {
            if (modifier.Name == "modifier_lycan_shapeshift")
            {
                this.Shapeshift = true;
            }

            base.AddModifier(modifier);
        }

        public override void RemoveModifier(Modifier modifier)
        {
            if (modifier.Name == "modifier_lycan_shapeshift")
            {
                this.Shapeshift = false;
            }

            base.RemoveModifier(modifier);
        }

        #endregion
    }
}