// <copyright file="EffectApplierWorker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;

    /// <summary>The modifier effect applier worker.</summary>
    internal class EffectApplierWorker : IEffectApplierWorker
    {
        #region Fields

        private IAbilityUnit abilityUnit;

        private Action<IAbilityUnit> applyEffectAction;

        private Func<Action<IAbilityUnit>> applyEffectActionGetter;

        private Action<IAbilityUnit> applyEffectActionUpdate;

        private Action<IAbilityUnit> removeEffectAction;

        private Func<Action<IAbilityUnit>> removeEffectActionGetter;

        private Action<IAbilityUnit> updateEffectAction;

        private Func<Action<IAbilityUnit>> updateEffectActionGetter;

        private bool useCustomUpdate;

        #endregion

        #region Constructors and Destructors

        public EffectApplierWorker(bool updateWithLevel)
        {
            this.UpdateWithLevel = updateWithLevel;
        }

        internal EffectApplierWorker(
            bool updateWithLevel,
            Func<Action<IAbilityUnit>> applyEffectActionGetter,
            Func<Action<IAbilityUnit>> removeEffectActionGetter,
            Func<Action<IAbilityUnit>> updateEffectActionGetter,
            Func<Action<IAbilityUnit>> applyEffectActionUpdateGetter = null)
        {
            this.UpdateWithLevel = updateWithLevel;
            this.ApplyEffectActionGetter = applyEffectActionGetter;
            this.RemoveEffectActionGetter = removeEffectActionGetter;
            this.UpdateEffectActionGetter = updateEffectActionGetter;
            this.ApplyEffectActionUpdateGetter = applyEffectActionUpdateGetter;
            this.useCustomUpdate = applyEffectActionGetter != null;
        }

        #endregion

        #region Public Properties

        public Func<Action<IAbilityUnit>> ApplyEffectActionGetter
        {
            get
            {
                return this.applyEffectActionGetter;
            }

            set
            {
                this.applyEffectActionGetter = value;
                this.applyEffectAction = this.applyEffectActionGetter.Invoke();
            }
        }

        public Func<Action<IAbilityUnit>> ApplyEffectActionUpdateGetter { get; }

        public bool EffectWasApplied { get; private set; }

        public Func<Action<IAbilityUnit>> RemoveEffectActionGetter
        {
            get
            {
                return this.removeEffectActionGetter;
            }

            set
            {
                this.removeEffectActionGetter = value;
                this.removeEffectAction = this.removeEffectActionGetter.Invoke();
            }
        }

        public Func<Action<IAbilityUnit>> UpdateEffectActionGetter
        {
            get
            {
                return this.updateEffectActionGetter;
            }

            set
            {
                this.updateEffectActionGetter = value;
                this.updateEffectAction = this.updateEffectActionGetter?.Invoke();
            }
        }

        public bool UpdateWithLevel { get; }

        #endregion

        #region Public Methods and Operators

        public void ApplyEffect(IAbilityUnit affectedUnit)
        {
            this.applyEffectAction.Invoke(affectedUnit);
            this.abilityUnit = affectedUnit;
            this.EffectWasApplied = true;
        }

        public void RemoveEffect()
        {
            this.removeEffectAction.Invoke(this.abilityUnit);
            this.EffectWasApplied = false;
        }

        public void UpdateEffect()
        {
            this.updateEffectAction.Invoke(this.abilityUnit);
        }

        #endregion
    }
}