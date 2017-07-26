// <copyright file="ItemObserver.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ItemManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public class ItemObserver
    {
        #region Fields

        private float count;

        #endregion

        #region Constructors and Destructors

        public ItemObserver(AbilityId itemId)
        {
            this.ItemId = itemId;
        }

        #endregion

        #region Public Properties

        public bool Equipped { get; set; }

        public IAbilitySkill Item { get; set; }

        public Notifier ItemEquipped { get; } = new Notifier();

        public AbilityId ItemId { get; }

        public Dictionary<uint, IAbilitySkill> Items { get; set; }

        public bool Multiple { get; set; }

        #endregion

        #region Public Methods and Operators

        public void ItemAdded(IAbilitySkill item)
        {
            if (this.Equipped)
            {
                Console.WriteLine("adding multiple");
                this.count++;
                if (this.Multiple)
                {
                    this.Items.Add(item.SkillHandle, item);
                    return;
                }

                this.Items = new Dictionary<uint, IAbilitySkill> { { item.SkillHandle, item } };
                this.Multiple = true;
                return;
            }

            this.Equipped = true;
            this.Item = item;
            Console.WriteLine("adding item " + this.Item.Name + " to " + this.Item.Owner.Name);
            this.ItemEquipped.Notify();
        }

        public void ItemRemoved(IAbilitySkill item)
        {
            if (this.Multiple)
            {
                Console.WriteLine("removing multiple");
                this.count--;
                if (this.Item.SkillHandle.Equals(item.SkillHandle))
                {
                    this.Item = this.Items.First().Value;
                    return;
                }

                this.Items.Remove(item.SkillHandle);
                if (this.count == 0)
                {
                    this.Multiple = false;
                }

                return;
            }

            this.Equipped = false;
            this.Item = null;
        }

        #endregion
    }
}