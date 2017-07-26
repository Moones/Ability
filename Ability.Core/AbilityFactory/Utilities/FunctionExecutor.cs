// <copyright file="FunctionExecutor.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.Utilities
{
    using System;

    public class FunctionExecutor<T> : IDisposable
    {
        #region Fields

        private FunctionManager<T> functionManager;

        private int id;

        #endregion

        #region Constructors and Destructors

        public FunctionExecutor(Func<T, bool> function)
        {
            this.Function = function;
        }

        #endregion

        #region Public Properties

        public Func<T, bool> Function { get; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.functionManager.Unsubscribe(this.id);
        }

        public void Subscribe(FunctionManager<T> functionManager)
        {
            this.id = functionManager.Subscribe(this.Function);
            this.functionManager = functionManager;
        }

        #endregion
    }
}