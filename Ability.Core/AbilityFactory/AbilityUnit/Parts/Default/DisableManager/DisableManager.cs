// <copyright file="DisableManager.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DisableManager
{
    using Ability.Core.Utilities;

    using Ensage;

    public class DisableManager : IDisableManager
    {
        #region Fields

        private Sleeper sleeper = new Sleeper();

        #endregion

        #region Constructors and Destructors

        public DisableManager(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public IAbilityUnit Unit { get; set; }

        public bool WillGetDisabled => this.sleeper.Sleeping;

        #endregion

        #region Public Methods and Operators

        public bool CanDisable(float delay)
        {
            return !this.sleeper.Sleeping
                   && (!this.Unit.Modifiers.Immobile || this.Unit.Modifiers.ImmobileDuration <= delay)
                   && this.Unit.SourceUnit.NetworkActivity != NetworkActivity.Idle
                   && this.Unit.SourceUnit.NetworkActivity != NetworkActivity.IdleImpatient
                   && this.Unit.SourceUnit.NetworkActivity != NetworkActivity.IdleImpatientSwordTap
                   && this.Unit.SourceUnit.NetworkActivity != NetworkActivity.IdleRare
                   && this.Unit.SourceUnit.NetworkActivity != NetworkActivity.IdleSleeping
                   && this.Unit.SourceUnit.NetworkActivity != NetworkActivity.IdleSleepingEnd
                   && this.Unit.SourceUnit.NetworkActivity != NetworkActivity.RoquelaireLandIdle
                   && this.Unit.SourceUnit.NetworkActivity != NetworkActivity.SwimIdle
                   && this.Unit.SourceUnit.NetworkActivity != NetworkActivity.WaitIdle;
        }

        public void CastingDisable(float delay)
        {
            this.sleeper.Sleep(delay);
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {
        }

        #endregion
    }
}