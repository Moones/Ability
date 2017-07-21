using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ItemManager
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public class ItemObserver
    {
        public AbilityId ItemId { get; }

        public ItemObserver(AbilityId itemId)
        {
            this.ItemId = itemId;
        }

        public Notifier ItemEquipped { get; } = new Notifier();

        private float count;

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

        public IAbilitySkill Item { get; set; }

        public bool Equipped { get; set; }

        public bool Multiple { get; set; }

        public Dictionary<uint, IAbilitySkill> Items { get; set; }
    }
}
