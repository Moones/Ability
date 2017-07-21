using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DisableManager
{
    using Ability.Core.Utilities;

    public class DisableManager : IDisableManager
    {
        public DisableManager(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public void Initialize()
        {
        }

        private Sleeper sleeper = new Sleeper();

        public void CastingDisable(float delay)
        {
            this.sleeper.Sleep(delay);
        }

        public bool CanDisable(float delay)
        {
            return !this.sleeper.Sleeping
                   && (!this.Unit.Modifiers.Immobile || this.Unit.Modifiers.ImmobileDuration <= delay);
        }

        public bool WillGetDisabled => this.sleeper.Sleeping;
    }
}
