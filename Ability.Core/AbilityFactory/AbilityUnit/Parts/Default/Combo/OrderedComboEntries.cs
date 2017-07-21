using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo
{
    using System.Collections;
    using System.Collections.Specialized;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ComboEntry;
    using Ability.Core.AbilityFactory.Utilities;

    public class OrderedComboEntries
    {
        private readonly Dictionary<uint, IComboEntry> dictionary = new Dictionary<uint, IComboEntry>();

        private IOrderedEnumerable<KeyValuePair<uint, IComboEntry>> orderedEntries;

        private float minExecutionDelay;

        public Notifier DelayChanged { get; } = new Notifier();

        public float MinExecutionDelay
        {
            get
            {
                return this.minExecutionDelay;
            }

            set
            {
                if (value != this.minExecutionDelay)
                {
                    this.DelayChanged.Notify();
                }
                else
                {
                    return;
                }

                this.minExecutionDelay = value;
            }
        }

        public virtual bool Execute()
        {
            return this.orderedEntries.Any(orderedEntry => orderedEntry.Value.Execute());
        }

        public void AddEntry(IComboEntry entry)
        {
            this.dictionary.Add(entry.Skill.SkillHandle, entry);
            this.orderedEntries = this.dictionary.OrderBy(pair => pair.Value.ExecutionDelay);
            this.MinExecutionDelay = this.orderedEntries.First().Value.ExecutionDelay;
        }

        public void RemoveEntry(IComboEntry entry)
        {
            this.dictionary.Remove(entry.Skill.SkillHandle);
            this.orderedEntries = this.dictionary.OrderBy(pair => pair.Value.ExecutionDelay);
            this.MinExecutionDelay = this.orderedEntries.First().Value.ExecutionDelay;
        }
    }
}
