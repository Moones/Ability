using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Brewmaster.Combo
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Brewmaster.Skillbook;

    public class BrewmasterCombo : UnitCombo
    {
        public BrewmasterCombo(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var skillbook = this.Unit.SkillBook as BrewmasterSkillbook;
            this.Entries.Add(new ComboEntry(skillbook.ThunderClap));
            this.Entries.Add(new ComboEntry(skillbook.DrunkenHaze));
        }
    }
}
