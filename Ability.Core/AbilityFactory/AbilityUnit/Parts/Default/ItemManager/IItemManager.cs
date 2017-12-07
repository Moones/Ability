// <copyright file="IItemManager.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ItemManager
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilitySkill;

    using Ensage;

    public interface IItemManager : IAbilityUnitPart
    {
        #region Public Properties

        ItemObserver AbyssalBlade { get; }

        ItemObserver AghanimScepter { get; }

        ItemObserver Blademail { get; }

        ItemObserver Blink { get; }

        ItemObserver Bloodthorn { get; }

        ItemObserver Bottle { get; }

        ItemObserver DiffusalBlade { get; }

        ItemObserver DrumOfEndurance { get; }

        ItemObserver IronTalon { get; }

        Dictionary<AbilityId, ItemObserver> ItemObservers { get; }

        IReadOnlyDictionary<double, IAbilitySkill> Items { get; }

        ItemObserver MantaStyle { get; }

        ItemObserver MaskOfMadness { get; }

        ItemObserver MedallionOfCourage { get; }

        ItemObserver Mjollnir { get; }

        ItemObserver Necronomicon { get; }

        ItemObserver Orchid { get; }

        ItemObserver PhaseBoots { get; }

        ItemObserver SolarCrest { get; }

        ItemObserver TravelBoots { get; }

        #endregion

        #region Public Methods and Operators

        void ItemAdded(IAbilitySkill item);

        void ItemRemoved(IAbilitySkill item);

        #endregion
    }
}