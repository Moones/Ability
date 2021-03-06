﻿// <copyright file="IAbilityUnit.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityTeam;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimation;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackDamage;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackRange;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DisableManager;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Health;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.IconDrawer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Interaction;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ItemManager;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Level;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Mana;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.MovementManager;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.MovementTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Pathfinder;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Position;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ScreenInfo;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TargetSelector;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TurnRate;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.UnitControl;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.UnitDataReceiver;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Visibility;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits;

    using Ensage;

    /// <summary>
    ///     The AbilityUnit interface.
    /// </summary>
    public interface IAbilityUnit : IDisposable
    {
        #region Public Properties

        IAttackAnimation AttackAnimation { get; set; }

        IAttackAnimationTracker AttackAnimationTracker { get; set; }

        IAttackDamage AttackDamage { get; set; }

        IUnitAttackRange AttackRange { get; set; }

        IControllableUnits ControllableUnits { get; set; }

        IDamageManipulation DamageManipulation { get; set; }

        /// <summary>
        ///     Gets the data receiver.
        /// </summary>
        IUnitDataReceiver DataReceiver { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether debug draw.
        /// </summary>
        bool DebugDraw { get; set; }

        IDisableManager DisableManager { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether draw.
        /// </summary>
        bool Draw { get; set; }

        bool Fighting { get; set; }

        /// <summary>
        ///     Gets the health.
        /// </summary>
        IHealth Health { get; set; }

        /// <summary>
        ///     Gets the drawer.
        /// </summary>
        IUnitIconDrawer IconDrawer { get; set; }

        /// <summary>
        ///     Gets the interaction.
        /// </summary>
        IUnitInteraction Interaction { get; set; }

        /// <summary>Gets a value indicating whether is creep.</summary>
        bool IsCreep { get; }

        /// <summary>
        ///     Gets a value indicating whether is enemy.
        /// </summary>
        bool IsEnemy { get; }

        bool IsHero { get; }

        /// <summary>Gets a value indicating whether is local hero.</summary>
        bool IsLocalHero { get; set; }

        IItemManager ItemManager { get; set; }

        /// <summary>
        ///     Gets the level.
        /// </summary>
        IUnitLevel Level { get; set; }

        /// <summary>
        ///     Gets the mana.
        /// </summary>
        IMana Mana { get; set; }

        /// <summary>
        ///     Gets the modifiers.
        /// </summary>
        IModifiers Modifiers { get; set; }

        IMovementManager MovementManager { get; set; }

        IMovementTracker MovementTracker { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        IUnitOrbwalker Orbwalker { get; set; }

        /// <summary>Gets the order issuers.</summary>
        IReadOnlyDictionary<uint, IOrderIssuer> OrderIssuers { get; }

        /// <summary>
        ///     Gets the overlay.
        /// </summary>
        // IUnitOverlay Overlay { get; set; }
        /// <summary>Gets or sets the order queue.</summary>
        IUnitOrderQueue OrderQueue { get; set; }

        IAbilityUnit Owner { get; set; }

        /// <summary>
        ///     Gets the overlay entry provider.
        /// </summary>
        // IOverlayEntryProvider OverlayEntryProvider { get; set; }
        /// <summary>Gets the parts.</summary>
        IReadOnlyDictionary<Type, IAbilityUnitPart> Parts { get; }

        /// <summary>Gets the pathfinder.</summary>
        IPathfinder Pathfinder { get; set; }

        /// <summary>
        ///     Gets the position.
        /// </summary>
        IPosition Position { get; set; }

        string PrettyName { get; }

        /// <summary>
        ///     Gets the screen position.
        /// </summary>
        IScreenInfo ScreenInfo { get; set; }

        /// <summary>
        ///     Gets the skill book.
        /// </summary>
        ISkillBook<IAbilitySkill> SkillBook { get; set; }

        Hero SourceHero { get; }

        /// <summary>
        ///     Gets or sets the source unit.
        /// </summary>
        Unit SourceUnit { get; set; }

        /// <summary>
        ///     Gets the particle tracker.
        /// </summary>
        IUnitTargetSelector TargetSelector { get; set; }

        /// <summary>
        ///     Gets or sets the team.
        /// </summary>
        IAbilityTeam Team { get; set; }

        IUnitTurnRate TurnRate { get; set; }

        IUnitCombo UnitCombo { get; set; }

        /// <summary>Gets or sets the unit composer.</summary>
        IAbilityUnitHeroComposer UnitComposer { get; set; }

        /// <summary>
        ///     Gets the unit control.
        /// </summary>
        IUnitControl UnitControl { get; set; }

        /// <summary>
        ///     Gets or sets the unit handle.
        /// </summary>
        uint UnitHandle { get; set; }

        /// <summary>
        ///     Gets or sets the unit handle string.
        /// </summary>
        string UnitHandleString { get; set; }

        /// <summary>
        ///     Gets the visibility.
        /// </summary>
        IVisibility Visibility { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add order issuer.</summary>
        /// <param name="orderIssuer">The order issuer.</param>
        void AddOrderIssuer(IOrderIssuer orderIssuer);

        /// <summary>The add part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <param name="partFactory">The part Factory.</param>
        void AddPart<T>(Func<IAbilityUnit, T> partFactory) where T : IAbilityUnitPart;

        /// <summary>The get part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        T GetPart<T>() where T : IAbilityUnitPart;

        /// <summary>The initialize.</summary>
        void Initialize();

        /// <summary>
        ///     The on draw.
        /// </summary>
        void OnDraw();

        /// <summary>The remove order issuer.</summary>
        /// <param name="orderIssuer">The order issuer.</param>
        void RemoveOrderIssuer(IOrderIssuer orderIssuer);

        /// <summary>The remove part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        void RemovePart<T>() where T : IAbilityUnitPart;

        #endregion
    }
}