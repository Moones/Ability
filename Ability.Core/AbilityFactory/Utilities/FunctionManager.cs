// <copyright file="FunctionManager.cs" company="EnsageSharp">
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
    using System.Collections.Generic;
    using System.Linq;

    public class FunctionManager<T> : IDisposable
    {
        #region Fields

        private int count;

        private Dictionary<int, Func<T, bool>> functions = new Dictionary<int, Func<T, bool>>();

        #endregion

        #region Constructors and Destructors

        public FunctionManager()
        {
        }

        #endregion

        #region Public Properties

        public IReadOnlyDictionary<int, Func<T, bool>> Functions => this.functions;

        #endregion

        #region Public Methods and Operators

        public bool AnyFunctionPasses(T value)
        {
            return this.functions.Any(x => x.Value.Invoke(value));
        }

        public void Dispose()
        {
            this.functions.Clear();
        }

        public int Subscribe(Func<T, bool> function)
        {
            this.count++;
            this.functions.Add(this.count, function);
            return this.count;
        }

        public void Unsubscribe(int id)
        {
            this.functions.Remove(id);
        }

        #endregion
    }

    public class FunctionManager : IDisposable
    {
        #region Fields

        private int count;

        private Dictionary<int, Func<bool>> functions = new Dictionary<int, Func<bool>>();

        #endregion

        #region Constructors and Destructors

        public FunctionManager()
        {
        }

        #endregion

        #region Public Properties

        public IReadOnlyDictionary<int, Func<bool>> Functions => this.functions;

        #endregion

        #region Public Methods and Operators

        public bool AnyFunctionPasses()
        {
            return this.functions.Any(x => x.Value.Invoke());
        }

        public void Dispose()
        {
            this.functions.Clear();
        }

        public int Subscribe(Func<bool> function)
        {
            this.count++;
            this.functions.Add(this.count, function);
            return this.count;
        }

        public void Unsubscribe(int id)
        {
            this.functions.Remove(id);
        }

        #endregion
    }
}