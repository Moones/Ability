using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.MaskOfMadness
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.MaskOfMadness.CastingFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Mjollnir.CastFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;

    using Ensage;

    [Export(typeof(IAbilitySkillItemComposer))]
    [AbilitySkillItemMetadata((uint)AbilityId.item_mask_of_madness)]
    internal class MaskOfMadnessItemComposer : DefaultSkillComposer
    {
        internal MaskOfMadnessItemComposer()
        {
            this.AssignControllablePart<ICastFunction>(
                skill =>
                {
                    if (skill.Owner.SourceUnit.IsControllable)
                    {
                        if (!skill.Owner.IsEnemy)
                        {
                            return new MaskOfMadnessCastingFunction(skill);
                        }
                    }

                    return null;
                });
        }
    }
}
