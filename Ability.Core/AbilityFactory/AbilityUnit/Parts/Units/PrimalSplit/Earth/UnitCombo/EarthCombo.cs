using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.PrimalSplit.Earth.UnitCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;

    using Ensage;

    public class EarthCombo : UnitComboNoItems
    {
        public EarthCombo(IAbilityUnit unit)
            : base(unit)
        {
        }

        /// <exception cref="NullReferenceException">Happens at the end of game where aghanim is deleted.</exception>
        public override void Initialize()
        {
            this.DisableEntries = new List<ComboEntry>
                                        {
                                            new ComboEntry(
                                                this.Unit.SkillBook.AllSkills.FirstOrDefault(
                                                    x =>
                                                        x.Value.SourceAbility.Id
                                                        == AbilityId.brewmaster_earth_hurl_boulder).Value)
                                        };

            if (this.Unit.Owner.SkillBook.HasAghanim)
            {
                this.DisableEntries.Add(
                    new ComboEntry(
                        this.Unit.SkillBook.AllSkills.FirstOrDefault(
                            x => x.Value.SourceAbility.Id == AbilityId.brewmaster_thunder_clap).Value));
            }
        }
    }
}
