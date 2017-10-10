// <copyright file="LycanComposer.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Lycan
{
    using System.ComponentModel.Composition;
    
    using Ability.Core.AbilityFactory.AbilityUnit.Metadata;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Lycan.ControllableUnits;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Lycan.Modifiers;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits;

    using Ensage;

    [Export(typeof(IAbilityUnitHeroComposer))]
    [AbilityUnitHeroMetadata((uint)HeroId.npc_dota_hero_lycan)]
    public class LycanComposer : AbilityUnitComposer
    {
        #region Constructors and Destructors

        public LycanComposer()
        {
            this.AssignPart<IControllableUnits>(
                unit =>
                    {
                        if (unit.IsLocalHero)
                        {
                            return new LycanControllableUnits(unit);
                        }

                        return null;
                    });
            this.AssignPart<IModifiers>(unit => new LycanModifiers(unit));
            this.AssignControllablePart<IUnitCombo>(unit => new UnitCombo(unit));
        }

        #endregion
    }
}