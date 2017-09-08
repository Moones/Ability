using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.LoneDruid.RuneTaker
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.RuneTaker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.SpiritBear.SkillBook;
    using Ability.Core.MenuManager.Menus.AbilityMenu;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Submenus;

    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    public class BearRuneTaker : RuneTaker
    {
        public BearRuneTaker(IAbilityUnit unit, IAbilityMapData abilityMapData)
            : base(unit, abilityMapData, true, new AbilitySubMenu("BearRuneTaker"))
        {
            this.TakeBountyOnStart = true;
        }

        public override IAbilityUnit Unit
        {
            get
            {
                return this.unit;
            }

            set
            {
                this.unit = value;
                if (this.unit != null)
                {
                    this.Return = (this.unit.SkillBook as SpiritBearSkillBook).Return;
                }
            }
        }

        public IAbilitySkill Return;

        private IAbilityUnit unit;

        public override bool ShouldTakeRune(RunePosition<BountyRune> rune)
        {
            var time = Game.GameTime;
            if (time < 0)
            {
                if (rune.Team == this.Unit.Team.Name)
                {
                    return false;
                }
            }
            
            if (rune != null && rune.HasRune
                && this.Unit.Owner.Position.Current.Distance2D(rune.Position)
                < 300)
            {
                return false;
            }

            return true;
        }

        public override bool ShouldTakeRune(RunePosition<PowerUpRune> rune)
        {
            if (rune.HasRune && rune.CurrentRune.SourceRune.RuneType == RuneType.Illusion)
            {
                return false;
            }

            return false;
        }

        public override void EnqueueRunForRune(List<Vector3> path)
        {
            this.Unit.OrderQueue.EnqueueOrder(new BearRuneForRune(this.Unit, this.ClosestBounty, path));
            this.Return.CastFunction.Cast();
        }
    }

}
