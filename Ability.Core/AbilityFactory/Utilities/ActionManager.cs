// <copyright file="ActionManager.cs" company="EnsageSharp">
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

    /// <summary>The update manager.</summary>
    public class ActionManager : IDisposable
    {
        #region Fields

        private Dictionary<int, Action> actions = new Dictionary<int, Action>();

        private int count;

        #endregion

        #region Constructors and Destructors

        public ActionManager()
        {
        }

        #endregion

        #region Public Properties

        public IReadOnlyDictionary<int, Action> Actions => this.actions;

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.actions.Clear();
        }

        public void InvokeActions()
        {
            foreach (var action in this.actions)
            {
                action.Value.Invoke();
            }
        }

        public int Subscribe(Action action)
        {
            this.count++;
            var tempActions = new Dictionary<int, Action>(this.actions) { { this.count, action } };
            this.actions = tempActions;
            return this.count;
        }

        public void Unsubscribe(int id)
        {
            var tempActions = new Dictionary<int, Action>(this.actions);
            tempActions.Remove(id);
            this.actions = tempActions;
        }

        #endregion
    }
}