// <copyright file="AbilityTalent.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityTalent
{
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public class AbilityTalent : IAbilityTalent
    {
        #region Fields

        private uint lastLevel;

        private ActionExecutor levelChecker;

        #endregion

        #region Constructors and Destructors

        public AbilityTalent(Ability sourceAbility, IAbilityUnit owner)
        {
            this.SourceAbility = sourceAbility;
            this.Owner = owner;

            this.levelChecker = new ActionExecutor(
                () =>
                    {
                        if (this.lastLevel > 0 || this.lastLevel == this.SourceAbility.Level)
                        {
                            return;
                        }

                        this.lastLevel = this.SourceAbility.Level;
                        if (this.lastLevel > 0)
                        {
                            this.TalentLeveledNotifier.Notify();
                            this.levelChecker.Dispose();
                        }
                    });

            // this.levelChecker.Subscribe(this.Owner.DataReceiver.Updates);
        }

        #endregion

        #region Public Properties

        public IAbilityUnit Owner { get; set; }

        public Ability SourceAbility { get; set; }

        public Notifier TalentLeveledNotifier { get; } = new Notifier();

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            if (this.lastLevel == 0)
            {
                this.levelChecker.Dispose();
            }

            this.TalentLeveledNotifier.Dispose();
        }

        #endregion
    }
}