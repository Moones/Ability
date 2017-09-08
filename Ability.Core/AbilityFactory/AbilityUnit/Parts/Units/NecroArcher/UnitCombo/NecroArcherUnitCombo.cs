using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.NecroArcher.UnitCombo
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;

    using Ensage;

    public class NecroArcherUnitCombo : UnitCombo
    {
        public NecroArcherUnitCombo(IAbilityUnit unit)
            : base(unit)
        {
        }

        public ComboEntry ManaBurn;

        public override void Initialize()
        {
            this.ManaBurn =
                new ComboEntry(
                    this.Unit.SkillBook.AllSkills.FirstOrDefault(
                        x => x.Value.SourceAbility.Id == AbilityId.necronomicon_archer_mana_burn).Value);

        }

        public override bool CastAllSpellsOnTarget()
        {
            return this.ManaBurn.Cast();
        }
    }
}
