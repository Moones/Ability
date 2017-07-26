// <copyright file="AttackAnimationTracker.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker
{
    using Ability.Core.AbilityFactory.Utilities;

    public class AttackAnimationTracker : IAttackAnimationTracker
    {
        #region Constructors and Destructors

        public AttackAnimationTracker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public float CancelAnimationTime { get; set; }

        public float NextAttackTime { get; set; }

        public FunctionManager<bool> OnAttackCancel { get; } = new FunctionManager<bool>();

        public FunctionManager<bool> OnAttackReady { get; } = new FunctionManager<bool>();

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AttackStarted()
        {
            var time = GlobalVariables.Time * 1000 - 0.01f;
            this.CancelAnimationTime = this.Unit.AttackAnimation.GetAttackPoint() * 1000f + time;
            this.NextAttackTime = this.Unit.AttackAnimation.GetAttackRate() * 1000f + time;
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