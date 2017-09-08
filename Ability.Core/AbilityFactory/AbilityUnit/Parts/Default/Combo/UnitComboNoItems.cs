using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo
{
    public class UnitComboNoItems : IUnitCombo
    {
        public UnitComboNoItems(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public virtual void Initialize()
        {
        }

        public List<ComboEntry> Entries { get; set; } = new List<ComboEntry>();

        public List<ComboEntry> DisableEntries { get; set; } = new List<ComboEntry>();

        public List<List<ComboEntry>> NukeCombos { get; set; }

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

        public bool NukeTarget()
        {
            return false;
        }

        public bool NoTarget()
        {
            return false;
        }
    }
}
