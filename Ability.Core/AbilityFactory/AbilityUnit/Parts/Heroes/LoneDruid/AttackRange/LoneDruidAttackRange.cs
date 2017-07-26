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

        #endregion

        #region Public Methods and Operators

        public override void Initialize()
        {
            base.Initialize();

            this.TrueForm =
                this.Unit.SourceUnit.HasModifiers(
                    new[] { "modifier_lone_druid_true_form", "modifier_lone_druid_true_form_transform" },
                    false);

            if (this.TrueForm)
            {
                Console.WriteLine("trueform " + this.TrueForm);
                this.Value -= 423;
            }

            this.Unit.Modifiers.ModifierAdded.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (this.TrueForm && modifier.Name == "modifier_lone_druid_druid_form_transform")
                            {
                                this.Value += 423;
                                this.TrueForm = false;
                                Console.WriteLine("trueform " + this.TrueForm);
                            }
                        }));

            this.Unit.Modifiers.ModifierRemoved.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (!this.TrueForm && modifier.Name == "modifier_lone_druid_true_form_transform")
                            {
                                this.Value -= 423;
                                this.TrueForm = true;
                                Console.WriteLine("trueform " + this.TrueForm);
                            }
                        }));
        }

        #endregion
    }
}