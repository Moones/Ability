// <copyright file="ItemManager.cs" company="EnsageSharp">
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
    using Ability.Core.Utilities;

    using Ensage;

    public class ItemManager : IItemManager
    {
        #region Fields

        private readonly Dictionary<double, IAbilitySkill> items = new Dictionary<double, IAbilitySkill>();

        private Dictionary<AbilityId, ItemObserver> all = new Dictionary<AbilityId, ItemObserver>();

        private Sleeper sleeper = new Sleeper();

        private int updateId;

        private Dictionary<AbilityId, ItemObserver> waitingForRemove = new Dictionary<AbilityId, ItemObserver>();

        #endregion

        #region Constructors and Destructors

        public ItemManager(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public ItemObserver AbyssalBlade { get; } = new ItemObserver(AbilityId.item_abyssal_blade);

        public IReadOnlyDictionary<double, IAbilitySkill> Items
        {
            get
            {
                return this.items;
            }
        }

        public ItemObserver Mjollnir { get; } = new ItemObserver(AbilityId.item_mjollnir);

        public ItemObserver PhaseBoots { get; } = new ItemObserver(AbilityId.item_phase_boots);

        public ItemObserver TravelBoots { get; } = new ItemObserver(AbilityId.item_travel_boots);

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.Unit.DataReceiver.Updates.Unsubscribe(this.updateId);
        }

        public void Initialize()
        {
            foreach (var propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(ItemObserver))
                {
                    var obj = propertyInfo.GetValue(this) as ItemObserver;
                    this.all.Add(obj.ItemId, obj);
                }
            }

            this.updateId = this.Unit.DataReceiver.Updates.Subscribe(this.Update);
        }

        public void ItemAdded(IAbilitySkill item)
        {
            var newDict = new Dictionary<AbilityId, ItemObserver>(this.all);
            foreach (var itemObserver in newDict)
            {
                if (itemObserver.Key == item.SourceItem.Id)
                {
                    itemObserver.Value.ItemAdded(item);
                    break;
                }
            }
        }

        public void ItemRemoved(IAbilitySkill item)
        {
            var newDict = new Dictionary<AbilityId, ItemObserver>(this.all);
            foreach (var itemObserver in newDict)
            {
                if (itemObserver.Value.Equipped && itemObserver.Key == item.SourceItem.Id)
                {
                    itemObserver.Value.ItemRemoved(item);
                    break;
                }
            }
        }

        #endregion

        #region Methods

        private void Update()
        {
            if (this.sleeper.Sleeping)
            {
                return;
            }

            this.sleeper.Sleep(1000);

            if (!this.Unit.SourceUnit.IsAlive)
            {
                // Console.WriteLine("not alive");
                return;
            }

            var dict = new Dictionary<AbilityId, ItemObserver>(this.all);
            foreach (var itemObserver in dict)
            {
                if (!itemObserver.Value.Equipped)
                {
                    continue;
                }

                if (itemObserver.Value.Multiple)
                {
                    var found = false;
                    foreach (var abilitySkill in itemObserver.Value.Items)
                    {
                        if (abilitySkill.Value.SourceItem.Owner != null && abilitySkill.Value.SourceItem.Owner.IsValid
                            && !abilitySkill.Value.SourceItem.Owner.Name.Equals(
                                this.Unit.Name,
                                StringComparison.InvariantCultureIgnoreCase))
                        {
                            var owner =
                                this.Unit.Team.UnitManager.Units.FirstOrDefault(
                                    x => x.Value.UnitHandle == abilitySkill.Value.SourceItem.Owner.Handle).Value;
                            Console.WriteLine(
                                "switching " + abilitySkill.Value.Name + " from " + this.Unit.Name + " to " + owner.Name);
                            itemObserver.Value.Item.Owner = owner;
                            owner.SkillBook.AddSkill(abilitySkill.Value);
                            this.Unit.SkillBook.DeleteItem(abilitySkill.Value);
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        continue;
                    }
                }

                if (itemObserver.Value.Item.SourceItem.Owner != null && itemObserver.Value.Item.SourceItem.Owner.IsValid
                    && !itemObserver.Value.Item.SourceItem.Owner.Name.Equals(
                        this.Unit.Name,
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    var owner =
                        this.Unit.Team.UnitManager.Units.FirstOrDefault(
                            x => x.Value.UnitHandle == itemObserver.Value.Item.SourceItem.Owner.Handle).Value;
                    Console.WriteLine(
                        "switching " + itemObserver.Value.Item.Name + " from " + this.Unit.Name + " to " + owner.Name);
                    itemObserver.Value.Item.Owner = owner;
                    owner.SkillBook.AddSkill(itemObserver.Value.Item);
                    this.Unit.SkillBook.DeleteItem(itemObserver.Value.Item);
                }
            }
        }

        #endregion
    }
}