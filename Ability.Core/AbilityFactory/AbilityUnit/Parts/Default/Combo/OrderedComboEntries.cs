// <copyright file="OrderedComboEntries.cs" company="EnsageSharp">
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
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ComboEntry;
    using Ability.Core.AbilityFactory.Utilities;

    public class OrderedComboEntries
    {
        #region Fields

        private readonly Dictionary<uint, IComboEntry> dictionary = new Dictionary<uint, IComboEntry>();

        private float minExecutionDelay;

        private IOrderedEnumerable<KeyValuePair<uint, IComboEntry>> orderedEntries;

        #endregion

        #region Public Properties

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

        #endregion

        #region Public Methods and Operators

        public void AddEntry(IComboEntry entry)
        {
            this.dictionary.Add(entry.Skill.SkillHandle, entry);
            this.orderedEntries = this.dictionary.OrderBy(pair => pair.Value.ExecutionDelay);
            this.MinExecutionDelay = this.orderedEntries.First().Value.ExecutionDelay;
        }

        public virtual bool Execute()
        {
            return this.orderedEntries.Any(orderedEntry => orderedEntry.Value.Execute());
        }

        public void RemoveEntry(IComboEntry entry)
        {
            this.dictionary.Remove(entry.Skill.SkillHandle);
            this.orderedEntries = this.dictionary.OrderBy(pair => pair.Value.ExecutionDelay);
            this.MinExecutionDelay = this.orderedEntries.First().Value.ExecutionDelay;
        }

        #endregion
    }
}