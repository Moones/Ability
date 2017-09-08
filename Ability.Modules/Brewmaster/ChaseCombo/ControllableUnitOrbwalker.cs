namespace Ability.Brewmaster.ChaseCombo
{
    using System;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.LoneDruid.ControllableUnits;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.ControllableUnits;

    using Ensage;

    public class ControllableUnitOrbwalker : UnitOrbwalkerBase
    {
        public ControllableUnitOrbwalker(IAbilityUnit unit)
            : base(unit)
        {
            this.IssueSleep = 150;
            this.controllableUnits = this.Unit.ControllableUnits;
        }

        private IControllableUnits controllableUnits;

        public override bool Move()
        {
            //Console.WriteLine(this.Unit.Owner == null);
            if (this.Unit.Owner.ControllableUnits.Units.Any(
                    x => x.Value.UnitHandle != this.Unit.UnitHandle && this.RunAround(x.Value, this.Target)))
            {
                return true;
            }
            
            return this.Bodyblocker.Bodyblock() || this.MoveToMouse();
        }

        public override void MoveBeforeAttack()
        {
            this.Move();
        }

        public override bool NoTarget()
        {
            if (this.Unit.Owner.ControllableUnits.Units.Any(
                    x => x.Value.UnitHandle != this.Unit.UnitHandle && this.RunAround(x.Value, Game.MousePosition)))
            {
                return true;
            }
            
            this.Unit.SourceUnit.Move(Game.MousePosition);
            return true;
        }


    }
}
