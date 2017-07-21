using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ItemManager
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Enums;

    using AbilityId = Ensage.AbilityId;

    public class ItemManager : IItemManager
    {
        public ItemManager(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        
        private Dictionary<AbilityId, ItemObserver> waitingForRemove = new Dictionary<AbilityId, ItemObserver>();
        private Dictionary<AbilityId, ItemObserver> all = new Dictionary<AbilityId, ItemObserver>();

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

            this.Unit.DataReceiver.Updates.Subscribe(this.Update);
        }

        private Sleeper sleeper = new Sleeper();

        private readonly Dictionary<double, IAbilitySkill> items = new Dictionary<double, IAbilitySkill>();

        private void Update()
        {
            if (this.sleeper.Sleeping)
            {
                return;
            }

            this.sleeper.Sleep(1000);

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
                    && !itemObserver.Value.Item.SourceItem.Owner.Name.Equals(this.Unit.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    var owner =
                        this.Unit.Team.UnitManager.Units.FirstOrDefault(
                            x => x.Value.UnitHandle == itemObserver.Value.Item.SourceItem.Owner.Handle).Value;
                    owner.SkillBook.AddSkill(itemObserver.Value.Item);
                    this.Unit.SkillBook.DeleteItem(itemObserver.Value.Item);
                }
            }
        }

        public ItemObserver PhaseBoots { get; } = new ItemObserver(AbilityId.item_phase_boots);

        public ItemObserver AbyssalBlade { get; } = new ItemObserver(AbilityId.item_abyssal_blade);

        public ItemObserver Mjollnir { get; } = new ItemObserver(AbilityId.item_mjollnir);

        public ItemObserver TravelBoots { get; } = new ItemObserver(AbilityId.item_travel_boots);
        

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

        public IReadOnlyDictionary<double, IAbilitySkill> Items
        {
            get
            {
                return this.items;
            }
        }
    }
}
