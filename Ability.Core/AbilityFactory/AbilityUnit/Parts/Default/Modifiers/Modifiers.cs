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

        public bool AbleToIssueAttack { get; set; }

        public bool Alacrity { get; set; }

        public bool AphoticShield { get; set; }

        public bool Attackable { get; set; }

        public bool AttackImmune { get; set; }

        public bool Bloodrage { get; set; }

        public Dictionary<string, Action<bool>> Buffs { get; set; }

        public bool ChillingTouch { get; set; }

        public bool ConsumedAghanim { get; set; }

        public bool Disarmed { get; set; }

        public bool Empower { get; set; }

        public bool FlameGuard { get; set; }

        public bool FrostArmor { get; set; }

        public bool GuardianAngel { get; set; }

        public bool HasBuffs { get; set; }

        public bool HasDebuffs { get; set; }

        public bool Immobile => this.immobile;

        public float ImmobileDuration
            => this.immobile && this.immobileModifier.IsValid ? this.immobileModifier.RemainingTime : 0;

        public bool InnerVitality { get; set; }

        public bool Invul { get; set; }

        public bool IonShell { get; set; }

        public bool MagicImmune { get; set; }

        public DataProvider<Modifier> ModifierAdded { get; } = new DataProvider<Modifier>();

        public DataProvider<Modifier> ModifierRemoved { get; } = new DataProvider<Modifier>();

        public bool Overpower { get; set; }

        public bool QuadrupleTap { get; set; }

        public bool Rabid { get; set; }

        public bool Recall { get; set; }

        public bool RocketBarrage { get; set; }

        public bool Rooted { get; set; }

        public bool SadistActive { get; set; }

        public bool Silenced { get; set; }

        public bool Sprint { get; set; }

        public bool Strafe { get; set; }

        public bool Stunned { get; set; }

        public bool Surge { get; set; }

        public bool TestOfFaithTeleport { get; set; }

        public bool Transform { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        public bool Windrun { get; set; }

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
            Action<bool> modifierAction;
            if (modifier.IsStunDebuff || this.DisableModifiers.Contains(modifier.Name))
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
            else if (this.Buffs.TryGetValue(modifier.Name, out modifierAction))
            {
                modifierAction(true);
            }

            this.AttackImmune = this.Unit.SourceUnit.IsAttackImmune();
            this.Invul = this.Unit.SourceUnit.IsInvul();
            this.MagicImmune = this.Unit.SourceUnit.IsMagicImmune();
            this.Rooted = this.Unit.SourceUnit.IsRooted();
            this.Stunned = this.Unit.SourceUnit.IsStunned();
            this.Silenced = this.Unit.SourceUnit.IsSilenced();
            this.Disarmed = this.Unit.SourceUnit.IsDisarmed();

            this.HasBuffs = this.AphoticShield || this.ChillingTouch || this.Bloodrage || this.TestOfFaithTeleport
                            || this.Strafe || this.IonShell || this.Surge || this.FlameGuard || this.RocketBarrage
                            || this.InnerVitality || this.Alacrity || this.Recall || this.FrostArmor || this.Rabid
                            || this.Empower || this.QuadrupleTap || this.Transform || this.SadistActive
                            || this.GuardianAngel || this.Sprint || this.Overpower || this.Windrun;

            this.Attackable = !this.AttackImmune && !this.Invul;
            this.AbleToIssueAttack = !this.Disarmed && !this.Stunned && !this.Invul;

            this.ModifierAdded.Next(modifier);
        }

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
            this.Buffs = new Dictionary<string, Action<bool>>
                             {
                                 { "modifier_item_ultimate_scepter_consumed", b => this.ConsumedAghanim = b },
                                 { "modifier_abaddon_aphotic_shield", b => this.AphoticShield = b },
                                 { "modifier_chilling_touch", b => this.ChillingTouch = b },
                                 { "modifier_bloodseeker_bloodrage", b => this.Bloodrage = b },
                                 { "modifier_chen_test_of_faith_teleport", b => this.TestOfFaithTeleport = b },
                                 { "modifier_clinkz_strafe", b => this.Strafe = b },
                                 { "modifier_dark_seer_ion_shell", b => this.IonShell = b },
                                 { "modifier_dark_seer_surge", b => this.Surge = b },
                                 { "modifier_ember_spirit_flame_guard", b => this.FlameGuard = b },
                                 { "modifier_gyrocopter_rocket_barrage", b => this.RocketBarrage = b },
                                 { "modifier_huskar_inner_vitality", b => this.InnerVitality = b },
                                 { "modifier_invoker_alacrity", b => this.Alacrity = b },
                                 { "modifier_keeper_of_the_light_recall", b => this.Recall = b },
                                 { "modifier_lich_frost_armor", b => this.FrostArmor = b },
                                 { "modifier_lone_druid_rabid", b => this.Rabid = b },
                                 { "modifier_magnataur_empower", b => this.Empower = b },
                                 { "modifier_monkey_king_quadruple_tap_bonuses", b => this.QuadrupleTap = b },
                                 { "modifier_monkey_king_transform", b => this.Transform = b },
                                 { "modifier_necrolyte_sadist_active", b => this.SadistActive = b },
                                 { "modifier_omninight_guardian_angel", b => this.GuardianAngel = b },
                                 { "modifier_slardar_sprint", b => this.Sprint = b },
                                 { "modifier_ursa_overpower", b => this.Overpower = b },
                                 { "modifier_windrunner_windrun", b => this.Windrun = b }
                             };

            this.AttackImmune = this.Unit.SourceUnit.IsAttackImmune();
            this.Invul = this.Unit.SourceUnit.IsInvul();
            this.MagicImmune = this.Unit.SourceUnit.IsMagicImmune();
            this.Rooted = this.Unit.SourceUnit.IsRooted();
            this.Stunned = this.Unit.SourceUnit.IsStunned();
            this.Silenced = this.Unit.SourceUnit.IsSilenced();
            this.Disarmed = this.Unit.SourceUnit.IsDisarmed();

            this.Attackable = !this.AttackImmune && !this.Invul;
            this.AbleToIssueAttack = !this.Disarmed && !this.Stunned && !this.Invul;

            this.HasBuffs = this.AphoticShield || this.ChillingTouch || this.Bloodrage || this.TestOfFaithTeleport
                            || this.Strafe || this.IonShell || this.Surge || this.FlameGuard || this.RocketBarrage
                            || this.InnerVitality || this.Alacrity || this.Recall || this.FrostArmor || this.Rabid
                            || this.Empower || this.QuadrupleTap || this.Transform || this.SadistActive
                            || this.GuardianAngel || this.Sprint || this.Overpower || this.Windrun;
        }

        /// <summary>
        ///     The modifier removed.
        /// </summary>
        /// <param name="modifier">
        ///     The modifier.
        /// </param>
        public virtual void RemoveModifier(Modifier modifier)
        {
            Action<bool> modifierAction;
            if (this.immobile && modifier.Handle.Equals(this.immobileModifier.Handle))
            {
                this.immobile = false;
            }
            else if (this.Buffs.TryGetValue(modifier.Name, out modifierAction))
            {
                modifierAction(false);
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

            this.HasBuffs = this.AphoticShield || this.ChillingTouch || this.Bloodrage || this.TestOfFaithTeleport
                            || this.Strafe || this.IonShell || this.Surge || this.FlameGuard || this.RocketBarrage
                            || this.InnerVitality || this.Alacrity || this.Recall || this.FrostArmor || this.Rabid
                            || this.Empower || this.QuadrupleTap || this.Transform || this.SadistActive
                            || this.GuardianAngel || this.Sprint || this.Overpower || this.Windrun;

            this.ModifierRemoved.Next(modifier);
        }

        #endregion
    }
}