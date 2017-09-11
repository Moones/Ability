// <copyright file="HitDelay.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.HitDelay
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;

    public class HitDelay : IHitDelay
    {
        #region Constructors and Destructors

        public HitDelay(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        public IAbilitySkill Skill { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public float Get(IAbilityUnit target)
        {
            var castPoint = 0d;
            if (this.Skill != null && this.Skill.CastData != null)
            {
                castPoint = this.Skill.CastData.CastPoint * 1000f;
            }

            return (float)(Game.Ping + castPoint);
        }

        public float Get()
        {
            var castPoint = 0d;
            if (this.Skill != null && this.Skill.CastData != null)
            {
                castPoint = this.Skill.CastData.CastPoint * 1000f;
            }

            return (float)(Game.Ping + castPoint);
        }

        public void Initialize()
        {
        }

        #endregion
    }
}