// <copyright file="RabidSkillComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.Rabid
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.Rabid.CastData;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.SkillComposer;

    // [Export(typeof(IAbilitySkillComposer))]
    // [AbilitySkillMetadata((uint)AbilityId.lone_druid_rabid)]
    internal class RabidSkillComposer : DefaultSkillComposer
    {
        #region Constructors and Destructors

        public RabidSkillComposer()
        {
            this.AssignPart<ISkillCastData>(skill => new RabidCastData(skill));
        }

        #endregion
    }
}