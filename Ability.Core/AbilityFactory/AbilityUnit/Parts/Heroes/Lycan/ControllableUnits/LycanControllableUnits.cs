using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Lycan.ControllableUnits
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits;
    using Ability.Core.Utilities;

    using Ensage.Common.Extensions;

    public class LycanControllableUnits : ControllableUnits
    {
        private readonly Dictionary<double, IAbilityUnit> wolves = new Dictionary<double, IAbilityUnit>();

        public LycanControllableUnits(IAbilityUnit unit)
            : base(unit)
        {
        }

        public IReadOnlyDictionary<double, IAbilityUnit> Wolves => this.wolves;

        public override void Initialize()
        {
            base.Initialize();
        }

        private Sleeper closestWolfSleeper = new Sleeper();

        private uint closestWolfHandle;

        public bool IsClosestWolf(IAbilityUnit wolf)
        {
            if (!this.Unit.TargetSelector.TargetIsSet || this.Unit.TargetSelector.Target == null)
            if (this.closestWolfSleeper.Sleeping)
            {
                return wolf.UnitHandle.Equals(this.closestWolfHandle);
            }

            var closestWolf =
                this.wolves.MinOrDefault(
                    x =>
                        x.Value.Position.PredictedByLatency.Distance2D(
                            this.Unit.TargetSelector.Target.Position.PredictedByLatency)).Value;

            if (closestWolf == null)
            {
                return false;
            }

            this.closestWolfHandle = closestWolf.UnitHandle;
            this.closestWolfSleeper.Sleep(500);

            return closestWolf.UnitHandle.Equals(wolf.UnitHandle);
        }

        public override void UnitAdded(IAbilityUnit unit)
        {
            if (unit.Name == "npc_dota_lycan_wolf1" || unit.Name == "npc_dota_lycan_wolf2"
                || unit.Name == "npc_dota_lycan_wolf3" || unit.Name == "npc_dota_lycan_wolf4")
            {
                this.wolves.Add(unit.UnitHandle, unit);
            }

            base.UnitAdded(unit);
        }

        public override void UnitRemoved(IAbilityUnit unit)
        {
            if (unit.Name == "npc_dota_lycan_wolf1" || unit.Name == "npc_dota_lycan_wolf2"
                || unit.Name == "npc_dota_lycan_wolf3" || unit.Name == "npc_dota_lycan_wolf4")
            {
                this.wolves.Remove(unit.UnitHandle);
            }

            base.UnitRemoved(unit);
        }
    }
}
