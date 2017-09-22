using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    public class RunForRune<T> : UnitOrderBase where T : IAbilityRune
    {
        public RunForRune(IAbilityUnit unit, RunePosition<T> rune, List<Vector3> path)
            : base(OrderType.TakeRune, unit, "Running for rune")
        {
            this.Path = path;
            this.RunePosition = rune;
            this.ShouldExecuteFast = true;
            this.Color = Color.LightPink;
        }

        public List<Vector3> Path { get; set; }

        public RunePosition<T> RunePosition { get; }

        public bool hadRune;

        public IAbilityRune rune;

        public override bool CanExecute()
        {
            if (this.Unit.Fighting && this.LastDistanceToRune / this.Unit.SourceUnit.MovementSpeed > 3.5)
            {
                Console.WriteLine("Canceled because fighting");
                return false;
            }

            if (this.hadRune
                && (!this.RunePosition.HasRune || this.rune == null || this.rune.Disposed
                    || this.RunePosition.CurrentRune == null || this.RunePosition.CurrentRune.Disposed))
            {
                Console.WriteLine("Canceled because no rune");
                return false;
            }

            return true;
        }

        public override float ExecuteFast()
        {
            if (this.LastDistanceToRune < 400)
            {
                if (this.RunePosition.HasRune && !this.RunePosition.CurrentRune.Disposed)
                {
                    this.hadRune = true;
                    this.rune = this.RunePosition.CurrentRune;
                    this.Unit.SourceUnit.PickUpRune(this.RunePosition.CurrentRune.SourceRune);
                    this.Unit.SourceUnit.PickUpRune(this.RunePosition.CurrentRune.SourceRune);
                    this.Unit.SourceUnit.PickUpRune(this.RunePosition.CurrentRune.SourceRune);
                    return 100;
                }
            }

            return 0;
        }

        private Sleeper sleeper = new Sleeper();

        public float LastDistanceToRune { get; private set; } = 999999;

        public override float Execute()
        {
            if (this.LastDistanceToRune < 400)
            {
                if (this.RunePosition.HasRune)
                {
                    this.hadRune = true;
                    this.rune = this.RunePosition.CurrentRune;
                    this.Unit.SourceUnit.PickUpRune(this.RunePosition.CurrentRune.SourceRune);
                    this.Unit.SourceUnit.PickUpRune(this.RunePosition.CurrentRune.SourceRune);
                    this.Unit.SourceUnit.PickUpRune(this.RunePosition.CurrentRune.SourceRune);
                    return 100;
                }

                return 0;
            }

            if (this.sleeper.Sleeping)
            {
                return 0;
            }

            this.sleeper.Sleep(300);

            this.LastDistanceToRune = this.Unit.Position.PredictedByLatency.Distance2D(this.RunePosition.Position);

            if (!this.RunePosition.HasRune && this.LastDistanceToRune < 1000
                && (this.LastDistanceToRune / this.Unit.SourceUnit.MovementSpeed) + 0.5
                < this.RunePosition.NextSpawnTime - Game.GameTime)
            {
                //Console.WriteLine("stopping");
                this.Unit.SourceUnit.Stop();
                return 0;
            }

            //Console.WriteLine("moving");
            this.Unit.SourceUnit.Move(this.RunePosition.Position);
            return 0;
        }
    }
}
