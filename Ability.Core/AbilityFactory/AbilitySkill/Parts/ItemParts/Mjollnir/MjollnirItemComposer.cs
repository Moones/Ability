using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Mjollnir
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Mjollnir.CastFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;

    using Ensage;

    [Export(typeof(IAbilitySkillItemComposer))]
    [AbilitySkillItemMetadata((uint)AbilityId.item_mjollnir)]
    internal class MjollnirItemComposer : DefaultSkillComposer
    {
        public MjollnirItemComposer()
        {
            this.AssignControllablePart<ICastFunction>(
                skill =>
                    {
                        return new MjollnirCastFunction(skill);
                    });
        }
    }
}
