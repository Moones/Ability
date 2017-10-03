using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Units.PrimalSplit.Storm.UnitCombo
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;

    using Ensage;
    using Ensage.Common;

    public class StormCombo : UnitComboNoItems
    {
        public StormCombo(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override void Initialize()
        {
            //Console.WriteLine("allspells");
            //foreach (var skillBookAllSkill in this.Unit.SourceUnit.Spellbook.Spells)
            //{
            //    Console.WriteLine(
            //        skillBookAllSkill.Name + " " + skillBookAllSkill.Id + " " + AbilityId.brewmaster_storm_wind_walk);
            //}

            this.Entries = new List<ComboEntry>
                                      {
                                          new ComboEntry(
                                              this.Unit.SkillBook.AllSkills.FirstOrDefault(
                                                  x =>
                                                      x.Value.SourceAbility.Id
                                                      == AbilityId.brewmaster_storm_dispel_magic).Value),

                                          new ComboEntry(
                                              this.Unit.SkillBook.AllSkills.FirstOrDefault(
                                                  x =>
                                                      x.Value.SourceAbility.Id
                                                      == AbilityId.brewmaster_storm_wind_walk).Value)
                                      };

            if (this.Unit.Owner.SkillBook.HasAghanim)
            {
                this.DisableEntries.Add(
                    new ComboEntry(
                        this.Unit.SkillBook.AllSkills.FirstOrDefault(
                            x => x.Value.SourceAbility.Id == AbilityId.brewmaster_drunken_haze).Value));
            }
        }
    }
}
