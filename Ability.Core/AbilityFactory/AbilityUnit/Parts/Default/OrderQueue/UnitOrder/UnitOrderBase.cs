// <copyright file="UnitOrderBase.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;

    using SharpDX;

    public abstract class UnitOrderBase : IUnitOrder
    {
        #region Constructors and Destructors

        protected UnitOrderBase(OrderType orderType, IAbilityUnit unit, string name)
        {
            this.OrderType = orderType;
            this.Priority = (uint)orderType;
            this.Unit = unit;
            this.Name = name;

            this.Color = Color.AliceBlue;
        }

        #endregion

        #region Public Properties

        public bool Canceled { get; set; }

        public Color Color { get; set; }

        public bool ExecuteOnce { get; set; }

        public uint Id { get; set; }

        public string Name { get; }

        public OrderType OrderType { get; }

        public bool PrintInLog { get; set; } = true;

        public uint Priority { get; }

        public bool ShouldExecuteFast { get; set; }

        public IAbilityUnit Unit { get; }

        #endregion

        #region Public Methods and Operators

        public void Cancel()
        {
            this.Canceled = true;
        }

        public abstract bool CanExecute();

        public virtual void Dequeue()
        {
        }

        public virtual void Enqueue()
        {
        }

        public abstract float Execute();

        public virtual float ExecuteFast()
        {
            return 0;
        }

        #endregion
    }
}