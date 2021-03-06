﻿// <copyright file="ActionExecutor.cs" company="EnsageSharp">
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

    public class ActionExecutor : IDisposable
    {
        #region Fields

        private ActionManager actionManager;

        private int id;

        private Dictionary<int, ActionManager> idDictionary = new Dictionary<int, ActionManager>();

        #endregion

        #region Constructors and Destructors

        public ActionExecutor(Action action)
        {
            this.Action = action;
        }

        #endregion

        #region Public Properties

        public Action Action { get; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            foreach (var manager in this.idDictionary)
            {
                manager.Value.Unsubscribe(manager.Key);
            }
        }

        public void Subscribe(ActionManager actionManager)
        {
            this.idDictionary.Add(actionManager.Subscribe(this.Action), actionManager);
        }

        #endregion
    }
}