using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.DiffusalBlade
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.DiffusalBlade.CastingFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Medallion.CastingFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;

    using Ensage;

    [Export(typeof(IAbilitySkillItemComposer))]
    [AbilitySkillItemMetadata((uint)AbilityId.item_diffusal_blade)]
    internal class DiffusalBladeItemComposer : DefaultSkillComposer
    {
        internal DiffusalBladeItemComposer()
        {
            this.AssignControllablePart<ICastFunction>(skill => new DiffusalCastFunction(skill));
        }
    }
}
