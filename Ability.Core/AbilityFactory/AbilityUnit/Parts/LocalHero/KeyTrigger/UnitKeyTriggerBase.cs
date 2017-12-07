// <copyright file="UnitKeyTriggerBase.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.KeyTrigger
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;

    using Ensage.Common.Menu;

    public class UnitKeyTriggerBase : IUnitKeyTrigger
    {
        #region Fields

        private DataObserver<KeyBind> keyBindObserver;

        private Dictionary<AbilityMenuItem<KeyBind>, IDisposable> keyBinds =
            new Dictionary<AbilityMenuItem<KeyBind>, IDisposable>();

        #endregion

        #region Constructors and Destructors

        public UnitKeyTriggerBase(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public bool Activated { get; private set; }

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual void Activate()
        {
            this.Activated = true;
        }

        public void AddKeyBind(AbilityMenuItem<KeyBind> keyBind)
        {
            this.keyBinds.Add(keyBind, keyBind.NewValueProvider.Subscribe(this.keyBindObserver));
            if (keyBind.Value.Active)
            {
                this.Activate();
            }
        }

        public virtual void Deactivate()
        {
            this.Activated = false;
        }

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
            this.keyBindObserver = new DataObserver<KeyBind>(
                bind =>
                    {
                        if (bind.Active)
                        {
                            this.Activate();
                        }
                        else
                        {
                            this.Deactivate();
                        }
                    });
        }

        public void RemoveKeyBind(AbilityMenuItem<KeyBind> keyBind)
        {
            this.keyBinds[keyBind].Dispose();
            this.keyBinds.Remove(keyBind);
            if (keyBind.Value.Active)
            {
                this.Deactivate();
            }
        }

        #endregion
    }
}