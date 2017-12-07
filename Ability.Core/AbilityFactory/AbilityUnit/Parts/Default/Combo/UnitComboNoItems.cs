// <copyright file="UnitComboNoItems.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo
{
    using System.Collections.Generic;

    public class UnitComboNoItems : IUnitCombo
    {
        #region Constructors and Destructors

        public UnitComboNoItems(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public List<ComboEntry> DisableEntries { get; set; } = new List<ComboEntry>();

        public List<ComboEntry> Entries { get; set; } = new List<ComboEntry>();

        public List<List<ComboEntry>> NukeCombos { get; set; }

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public bool CastAllSpellsOnTarget()
        {
            if (!this.Unit.TargetSelector.Target.DisableManager.WillGetDisabled)
            {
                foreach (var comboEntry in this.DisableEntries)
                {
                    if (comboEntry.Cast())
                    {
                        return true;
                    }
                }
            }

            foreach (var comboEntry in this.Entries)
            {
                if (comboEntry.Cast())
                {
                    return true;
                }
            }

            return false;
        }

        public bool DisableTarget()
        {
            if (!this.Unit.TargetSelector.Target.DisableManager.WillGetDisabled)
            {
                foreach (var comboEntry in this.DisableEntries)
                {
                    if (comboEntry.Cast())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Dispose()
        {
        }

        public virtual void Initialize()
        {
        }

        public bool NoTarget()
        {
            return false;
        }

        public bool NukeTarget()
        {
            return false;
        }

        #endregion
    }
}