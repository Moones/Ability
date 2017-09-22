// <copyright file="Modifiers.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    /// <summary>
    ///     The modifiers.
    /// </summary>
    public class Modifiers : IModifiers
    {
        #region Fields

        private readonly HashSet<string> DisableModifiers = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                                                                {
                                                                    "modifier_shadow_demon_disruption",
                                                                    "modifier_obsidian_destroyer_astral_imprisonment_prison",
                                                                    "modifier_eul_cyclone", "modifier_invoker_tornado",
                                                                    "modifier_bane_nightmare",
                                                                    "modifier_shadow_shaman_shackles",
                                                                    "modifier_crystal_maiden_frostbite",
                                                                    "modifier_ember_spirit_searing_chains",
                                                                    "modifier_axe_berserkers_call",
                                                                    "modifier_lone_druid_spirit_bear_entangle_effect",
                                                                    "modifier_meepo_earthbind",
                                                                    "modifier_naga_siren_ensnare",
                                                                    "modifier_storm_spirit_electric_vortex_pull",
                                                                    "modifier_treant_overgrowth", "modifier_cyclone",
                                                                    "modifier_sheepstick_debuff",
                                                                    "modifier_shadow_shaman_voodoo",
                                                                    "modifier_lion_voodoo", "modifier_sheepstick",
                                                                    "modifier_brewmaster_storm_cyclone",
                                                                    "modifier_puck_phase_shift",
                                                                    "modifier_dark_troll_warlord_ensnare",
                                                                    "modifier_invoker_deafening_blast_knockback",
                                                                    "modifier_pudge_meat_hook"
                                                                };

        private bool immobile;

        private Modifier immobileModifier;

        #endregion

        #region Constructors and Destructors

        internal Modifiers(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public bool AttackImmune { get; set; }

        public bool ConsumedAghanim { get; set; }

        public bool Disarmed { get; set; }

        public bool Immobile => this.immobile;

        public float ImmobileDuration => (this.immobile && this.immobileModifier.IsValid) ? this.immobileModifier.RemainingTime : 0;

        public bool Invul { get; set; }

        public bool MagicImmune { get; set; }

        public DataProvider<Modifier> ModifierAdded { get; } = new DataProvider<Modifier>();

        public DataProvider<Modifier> ModifierRemoved { get; } = new DataProvider<Modifier>();

        public bool Rooted { get; set; }

        public bool Silenced { get; set; }

        public bool Stunned { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The new modifier received.
        /// </summary>
        /// <param name="modifier">
        ///     The modifier.
        /// </param>
        public virtual void AddModifier(Modifier modifier)
        {
            if (modifier.Name == "modifier_item_ultimate_scepter_consumed")
            {
                this.ConsumedAghanim = true;
            }
            else if (modifier.IsStunDebuff || this.DisableModifiers.Contains(modifier.Name))
            {
                var remainingTime = modifier.RemainingTime;
                if (modifier.Name == "modifier_eul_cyclone" || modifier.Name == "modifier_invoker_tornado")
                {
                    remainingTime += 0.07f;
                }

                if (remainingTime > this.ImmobileDuration)
                {
                    this.immobile = true;
                    this.immobileModifier = modifier;
                }
            }

            this.AttackImmune = this.Unit.SourceUnit.IsAttackImmune();
            this.Invul = this.Unit.SourceUnit.IsInvul();
            this.MagicImmune = this.Unit.SourceUnit.IsMagicImmune();
            this.Rooted = this.Unit.SourceUnit.IsRooted();
            this.Stunned = this.Unit.SourceUnit.IsStunned();
            this.Silenced = this.Unit.SourceUnit.IsSilenced();
            this.Disarmed = this.Unit.SourceUnit.IsDisarmed();

            this.Attackable = !this.AttackImmune && !this.Invul;
            this.AbleToIssueAttack = !this.Disarmed && !this.Stunned && !this.Invul;

            this.ModifierAdded.Next(modifier);
        }

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
        }

        /// <summary>
        ///     The modifier removed.
        /// </summary>
        /// <param name="modifier">
        ///     The modifier.
        /// </param>
        public virtual void RemoveModifier(Modifier modifier)
        {
            if (modifier.Name == "modifier_item_ultimate_scepter_consumed")
            {
                this.ConsumedAghanim = false;
            }
            else if (this.immobile && modifier.Handle.Equals(this.immobileModifier.Handle))
            {
                this.immobile = false;
            }

            this.AttackImmune = this.Unit.SourceUnit.IsAttackImmune();
            this.Invul = this.Unit.SourceUnit.IsInvul();
            this.MagicImmune = this.Unit.SourceUnit.IsMagicImmune();
            this.Rooted = this.Unit.SourceUnit.IsRooted();
            this.Stunned = this.Unit.SourceUnit.IsStunned();
            this.Silenced = this.Unit.SourceUnit.IsSilenced();
            this.Disarmed = this.Unit.SourceUnit.IsDisarmed();

            this.ModifierRemoved.Next(modifier);
        }

        public bool Attackable { get; set; }

        public bool AbleToIssueAttack { get; set; }

        #endregion
    }
}