using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Medallion
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Medallion.CastingFunction;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;

    using Ensage;

    [Export(typeof(IAbilitySkillItemComposer))]
    [AbilitySkillItemMetadata((uint)AbilityId.item_medallion_of_courage, (uint)AbilityId.item_solar_crest)]
    internal class MedallionItemComposer : DefaultSkillComposer
    {
        internal MedallionItemComposer()
        {
            this.AssignControllablePart<ICastFunction>(
                skill =>
                {
                    if (skill.Owner.SourceUnit.IsControllable)
                    {
                        if (!skill.Owner.IsEnemy)
                        {
                            return new MedallionCastingFunction(skill);
                        }
                    }

                    return null;
                });
        }
    }
}
