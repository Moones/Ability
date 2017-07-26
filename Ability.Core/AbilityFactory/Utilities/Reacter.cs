// <copyright file="Reacter.cs" company="EnsageSharp">
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

    public class Reacter : IDisposable
    {
        #region Fields

        private int id;

        private Notifier notifier;

        #endregion

        #region Constructors and Destructors

        public Reacter(Action reaction, bool reactOnce = false)
        {
            this.Reaction = reaction;
            this.ReactOnce = reactOnce;
        }

        #endregion

        #region Public Properties

        public Action Reaction { get; }

        public bool ReactOnce { get; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.notifier?.Unsubscribe(this.id);
        }

        public void Subscribe(Notifier notifier)
        {
            this.id = notifier.Subscribe(this.React);
            this.notifier = notifier;
        }

        #endregion

        #region Methods

        private void React()
        {
            this.Reaction.Invoke();
            if (this.ReactOnce)
            {
                this.Dispose();
            }
        }

        #endregion
    }
}