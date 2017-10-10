using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Lycan.Modifiers
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;

    using Ensage;

    public class LycanModifiers : Modifiers
    {
        public LycanModifiers(IAbilityUnit unit)
            : base(unit)
        {
        }

        public bool Shapeshift { get; private set; }

        public override void AddModifier(Modifier modifier)
        {
            if (modifier.Name == "modifier_lycan_shapeshift")
            {
                this.Shapeshift = true;
            }

            base.AddModifier(modifier);
        }

        public override void RemoveModifier(Modifier modifier)
        {
            if (modifier.Name == "modifier_lycan_shapeshift")
            {
                this.Shapeshift = false;
            }

            base.RemoveModifier(modifier);
        }
    }
}
