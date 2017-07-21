using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Combo
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ComboEntry;
    using Ability.Core.AbilityFactory.Utilities;

    public interface IUnitCombo : IAbilityUnitPart
    {
        OrderedComboEntries GetCloserToTargetEntries { get; }

        OrderedComboEntries DisableTargetEntries { get; }

        OrderedComboEntries WeakenTargetEntries { get; }

        OrderedComboEntries DamageTargetEntries { get; }

        FunctionManager BeforeAttack { get; }

        FunctionManager AttackCancel { get; }

        FunctionManager BetweenAttacks { get; }

        FunctionManager TargetStartMoving { get; }

        FunctionManager TargetStartAttacking { get; }

        FunctionManager TargetStartCasting { get; }

        bool TargetIsValid { get; set; }

        bool Attacking { get; set; }

        bool Execute();
    }
}
