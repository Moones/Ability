// <copyright file="JungleCampBase.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Jungle
{
    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    public class JungleCamp
    {
        #region Constructors and Destructors

        public JungleCamp(Vector3 position, Vector3 waitPosition, Vector3 stackPosition, float maxStacks)
        {
        }

        #endregion

        #region Public Properties

        public Vector3 Position { get; }

        #endregion

        #region Public Methods and Operators

        public bool CreepBelongHere(Creep entity)
        {
            if (entity.Team == Team.Neutral && entity.IsAlive)
            {
                var infront = entity.InFront(200);
                var infrontDistane = infront.Distance2D(this.Position);
                if (infrontDistane < 1000 && infrontDistane <= entity.NetworkPosition.Distance2D(this.Position))
                {
                    return true;
                }
            }

            return false;
        }

        public void Update()
        {
        }

        #endregion
    }
}