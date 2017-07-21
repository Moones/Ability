using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackAnimationTracker
{
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    using log4net.Config;

    using PlaySharp.Toolkit.Logging;

    public class AttackAnimationTracker : IAttackAnimationTracker
    {
        public AttackAnimationTracker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public FunctionManager<bool> OnAttackCancel { get; } = new FunctionManager<bool>();

        public FunctionManager<bool> OnAttackReady { get; } = new FunctionManager<bool>();

        public void AttackStarted()
        {
            var time = GlobalVariables.Time * 1000 - 0.01f;
            this.CancelAnimationTime = (this.Unit.AttackAnimation.GetAttackPoint() * 1000f) + time;
            this.NextAttackTime = (this.Unit.AttackAnimation.GetAttackRate() * 1000f) + time;
        }



        public float NextAttackTime { get; set; }

        public float CancelAnimationTime { get; set; }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public void Initialize()
        {
        }
    }
}
