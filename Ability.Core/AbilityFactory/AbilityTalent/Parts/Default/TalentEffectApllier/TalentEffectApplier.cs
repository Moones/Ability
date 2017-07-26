// <copyright file="TalentEffectApplier.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityTalent.Parts.Default.TalentEffectApllier
{
    using Ability.Core.AbilityFactory.Utilities;

    public abstract class TalentEffectApplier : ITalentEffectApplier
    {
        #region Fields

        private uint lastLevel;

        private ActionExecutor levelUpdater;

        #endregion

        #region Constructors and Destructors

        protected TalentEffectApplier(IAbilityTalent talent)
        {
            this.Talent = talent;
        }

        #endregion

        #region Public Properties

        public bool EffectWasApplied { get; set; }

        public IAbilityTalent Talent { get; set; }

        #endregion

        #region Public Methods and Operators

        public abstract void ApplyEffect();

        public void Dispose()
        {
            if (!this.EffectWasApplied)
            {
                this.levelUpdater.Dispose();
            }
        }

        public void Initialize()
        {
            this.levelUpdater = new ActionExecutor(this.Update);
            this.levelUpdater.Subscribe(this.Talent.Owner.DataReceiver.Updates);
        }

        #endregion

        #region Methods

        private void Update()
        {
            if (this.EffectWasApplied || this.lastLevel == this.Talent.SourceAbility.Level)
            {
                return;
            }

            this.lastLevel = this.Talent.SourceAbility.Level;
            if (this.lastLevel > 0)
            {
                this.ApplyEffect();
                this.EffectWasApplied = true;
                this.levelUpdater.Dispose();
            }
        }

        #endregion
    }
}