using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Brewmaster
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Brewmaster.AttackAnimationTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Brewmaster.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Brewmaster.Skillbook;

    using Ensage;

    [Export(typeof(IAbilityUnitHeroComposer))]
    [AbilityUnitHeroMetadata((uint)HeroId.npc_dota_hero_brewmaster)]
    internal class BrewmasterUnitComposer : AbilityUnitComposer
    {
        internal BrewmasterUnitComposer()
        {
            this.AssignPart<ISkillBook<IAbilitySkill>>(unit => new BrewmasterSkillbook(unit));
            this.AssignControllablePart<IUnitCombo>(unit => new BrewmasterCombo(unit));
            this.AssignPart<IAttackAnimationTracker>(unit => new BrewmasterAttackAnimationTracker(unit));
        }
    }
}
