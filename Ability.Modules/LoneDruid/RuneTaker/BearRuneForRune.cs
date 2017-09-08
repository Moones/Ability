using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.LoneDruid.RuneTaker
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;

    using SharpDX;

    public class BearRuneForRune : RunForRune<BountyRune>
    {
        public BearRuneForRune(IAbilityUnit unit, RunePosition<BountyRune> rune, List<Vector3> path)
            : base(unit, rune, path)
        {
        }

        public override bool CanExecute()
        {
            if (this.Unit.Owner.Fighting && this.LastDistanceToRune / this.Unit.SourceUnit.MovementSpeed > 3.5)
            {
                Console.WriteLine("Canceled because fighting");
                return false;
            }

            return base.CanExecute();
        }
    }
}
