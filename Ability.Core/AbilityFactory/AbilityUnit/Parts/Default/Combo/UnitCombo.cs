// <copyright file="UnitCombo.cs" company="EnsageSharp">
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

    public class UnitCombo : IUnitCombo
    {
        #region Constructors and Destructors

        public UnitCombo(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public List<ComboEntry> DisableEntries { get; set; }

        public List<ComboEntry> Entries { get; set; }

        public List<List<ComboEntry>> NukeCombos { get; set; }

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual bool CastAllSpellsOnTarget()
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

            if (this.Unit.ItemManager.PhaseBoots.Equipped && this.Unit.ItemManager.PhaseBoots.Item.CastFunction.Cast())
            {
                return true;
            }

            return false;
        }

        public virtual bool DisableTarget()
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
            this.Entries = new List<ComboEntry>
                               {
                                   new ComboEntry(this.Unit.ItemManager.Mjollnir),
                                   new ComboEntry(this.Unit.ItemManager.MaskOfMadness),
                                   new ComboEntry(this.Unit.ItemManager.SolarCrest),
                                   new ComboEntry(this.Unit.ItemManager.MedallionOfCourage)
                               };

            this.DisableEntries = new List<ComboEntry>
                                      {
                                          new ComboEntry(this.Unit.ItemManager.AbyssalBlade),
                                          new ComboEntry(this.Unit.ItemManager.Orchid),
                                          new ComboEntry(this.Unit.ItemManager.Bloodthorn),
                                          new ComboEntry(this.Unit.ItemManager.DiffusalBlade)
                                      };
        }

        public virtual bool NoTarget()
        {
            if (this.Unit.ItemManager.PhaseBoots.Equipped && this.Unit.ItemManager.PhaseBoots.Item.CastFunction.Cast())
            {
                return true;
            }

            return false;
        }

        public virtual bool NukeTarget()
        {
            return false;
        }

        #endregion
    }
}