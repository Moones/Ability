// <copyright file="ValueHolder.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation
{
    using System;

    internal class ValueHolder<T> : IValueHolder<T>
    {
        #region Constructors and Destructors

        internal ValueHolder(T value, bool willExpire = false, Func<float> expireTime = null)
        {
            this.Value = value;
            this.WillExpire = willExpire;
            this.ExpireTime = expireTime;
        }

        internal ValueHolder(
            Func<IAbilityUnit, float, T> getValue,
            bool willExpire = false,
            Func<float> expireTime = null)
        {
            this.GetSpecialValue = getValue;
            this.WillExpire = willExpire;
            this.ExpireTime = expireTime;
        }

        #endregion

        #region Public Properties

        public Func<float> ExpireTime { get; }

        public Func<IAbilityUnit, float, T> GetSpecialValue { get; set; }

        public T Value { get; set; }

        public bool WillExpire { get; }

        #endregion
    }
}