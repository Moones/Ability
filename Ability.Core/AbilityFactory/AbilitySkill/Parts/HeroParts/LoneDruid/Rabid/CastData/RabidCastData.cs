// <copyright file="RabidCastData.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.Rabid.CastData
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class RabidCastData : SkillCastData
    {
        #region Constructors and Destructors

        public RabidCastData(IAbilitySkill skill)
            : base(skill)
        {
        }

        #endregion

        #region Public Properties

        public bool BearAffected { get; set; }

        public bool LocalHeroAffected { get; set; }

        #endregion

        #region Public Methods and Operators

        public override void Initialize()
        {
            this.Skill.Owner.Modifiers.ModifierRemoved.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (modifier.Name == "modifier_lone_druid_rabid")
                            {
                                this.LocalHeroAffected = false;
                            }
                        }));

            this.LocalHeroAffected = this.Skill.Owner.SourceUnit.HasModifier("modifier_lone_druid_rabid");

            this.Skill.Owner.Modifiers.ModifierAdded.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (modifier.Name == "modifier_lone_druid_rabid")
                            {
                                this.LocalHeroAffected = true;
                            }
                        }));

            base.Initialize();
        }

        #endregion
    }
}