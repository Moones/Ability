using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.Combo
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.SkillBook;

    public class LoneDruidCombo : UnitCombo
    {
        public LoneDruidCombo(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var skillbook = this.Unit.SkillBook as LoneDruidSkillBook;
            this.Entries.Add(new ComboEntry(skillbook.BattleCry));
            this.Entries.Add(new ComboEntry(skillbook.Rabid));
        }
    }
}
