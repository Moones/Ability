// <copyright file="LoneDruidAttackRange.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.AttackRange
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackRange;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class LoneDruidAttackRange : UnitAttackRange
    {
        #region Constructors and Destructors

        public LoneDruidAttackRange(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        public bool TrueForm { get; set; }
        public bool PikeLance { get; set; }
        public bool TalentBonus { get; set; }
        public int attackRange { get; set; }

        #endregion

        #region Public Methods and Operators
        
        public override void Initialize()
        {
            base.Initialize();

            this.TrueForm =
                this.Unit.SourceUnit.HasModifiers(
                    new[] { "modifier_lone_druid_true_form", "modifier_lone_druid_true_form_transform" },
                    false);
            this.PikeLance = this.Unit.SourceUnit.HasModifiers(
                    new[] { "modifier_item_hurricane_pike", "modifier_item_dragon_lance" },
                    false);
            this.TalentBonus = this.Unit.SourceUnit.HasModifier("modifier_special_bonus_attack_range");

                                
            this.attackRange = 550;
            Console.WriteLine(this.attackRange + "Default");

            if (this.PikeLance)
            {
                this.attackRange += 140;
                this.Value = attackRange;
                Console.WriteLine(this.attackRange + "PikeLance");
            }

            if(this.TalentBonus)
            {
                this.attackRange += 175;
                this.Value = attackRange;
                Console.WriteLine(this.attackRange + "TalentBonus");
            }

            if (this.TrueForm)
            {
                Console.WriteLine("trueform " + this.TrueForm);
                this.Value = 150;
                Console.WriteLine(this.attackRange + "TrueForm");
            }

            this.Unit.Modifiers.ModifierAdded.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (this.TrueForm && modifier.Name == "modifier_lone_druid_druid_form_transform")
                            {
                                this.Value = attackRange;
                                this.TrueForm = false;
                                Console.WriteLine("trueform " + this.TrueForm);
                                Console.WriteLine("ranged mode" + this.Value);
                            }
                            

                            if(!this.TalentBonus && modifier.Name == "modifier_special_bonus_attack_range")
                            {
                                attackRange += 175;
                                this.TalentBonus = true;
                                Console.WriteLine(this.attackRange + "Talent bonus leveled.");
                                if (!this.TrueForm) this.Value = attackRange;
                            }

                            if(!this.PikeLance && modifier.Name == "modifier_item_dragon_lance")
                            {
                                attackRange += 140;
                                this.PikeLance = true;
                                Console.WriteLine(this.attackRange + "Dragon Lance Purchased");
                                if (!this.TrueForm) this.Value = attackRange;
                            }

                            if (!this.PikeLance && modifier.Name == "modifier_item_hurricane_pike")
                            {
                                attackRange += 140;
                                this.PikeLance = true;
                                Console.WriteLine(this.attackRange + "Hurricane Pike Purchased");
                                if (!this.TrueForm) this.Value = attackRange;
                            }



                        }));

            this.Unit.Modifiers.ModifierRemoved.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (!this.TrueForm && modifier.Name == "modifier_lone_druid_true_form_transform")
                            {
                                this.Value = 150;
                                this.TrueForm = true;
                                Console.WriteLine("trueform " + this.TrueForm);
                                Console.WriteLine("attack 2range: " + this.Value);
                            }

                            if(this.PikeLance && modifier.Name == "modifier_item_dragon_lance")
                            {                                
                                attackRange -= 140;
                                this.PikeLance = false;
                                Console.WriteLine(this.attackRange + "Dragon Lance Removed");
                                if (!this.TrueForm) this.Value = attackRange;
                            }

                            if(this.PikeLance && modifier.Name == "modifier_item_hurricane_pike")
                            {                                
                                attackRange -= 140;
                                this.PikeLance = false;
                                Console.WriteLine(this.attackRange + "Pike removed");
                                if (!this.TrueForm) this.Value = attackRange;
                            }


                        }));
        }

        #endregion
    }
}
