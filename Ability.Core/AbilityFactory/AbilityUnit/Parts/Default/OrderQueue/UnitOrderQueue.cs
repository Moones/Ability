// <copyright file="UnitOrderQueue.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.GetValue;
    using Ability.Core.Utilities;

    using Ensage;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    internal class UnitOrderQueue : IUnitOrderQueue
    {
        #region Fields

        private uint count;

        private uint lastId;

        private DrawText orderText;

        private IUnitOrder processedOrder;

        private bool queueEmpty = true;

        private Sleeper sleeper = new Sleeper();

        #endregion

        #region Constructors and Destructors

        public UnitOrderQueue(IAbilityUnit unit)
        {
            this.Unit = unit;
            this.Unit.AddOrderIssuer(this);

            this.orderText = new DrawText
                                 {
                                    Shadow = true, TextSize = new Vector2(this.Unit.ScreenInfo.HealthBarSize.X / 3) 
                                 };
        }

        #endregion

        #region Public Properties

        public GetValue<bool, bool> DrawOrder { get; set; }

        public bool Enabled { get; set; }

        public uint Id { get; set; }

        public bool IsWorking { get; set; }

        public DataProvider<IUnitOrder> NewOrderQueued { get; } = new DataProvider<IUnitOrder>();

        public IUnitOrder ProcessedOrder
        {
            get
            {
                return this.processedOrder;
            }

            private set
            {
                this.processedOrder = value;
                this.queueEmpty = this.processedOrder == null;
            }
        }

        public Notifier QueueEmpty { get; } = new Notifier();

        public DataProvider<IUnitOrder> StartedExecution { get; } = new DataProvider<IUnitOrder>();

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Properties

        private Dictionary<uint, IUnitOrder> OrdersDictionary { get; } = new Dictionary<uint, IUnitOrder>();

        private IOrderedEnumerable<KeyValuePair<uint, IUnitOrder>> Queue { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.OrdersDictionary.Clear();
            this.Queue = null;
            this.NewOrderQueued.Dispose();
            this.QueueEmpty.Dispose();
        }

        public void EnqueueOrder(IUnitOrder order)
        {
            this.lastId++;
            order.Id = this.lastId;
            order.Enqueue();
            if (this.queueEmpty)
            {
                if (this.IssueNow(order))
                {
                    //Console.WriteLine("Enqueued: " + order.Name);
                    this.ProcessedOrder = order;

                    // this.queueEmpty = false;
                    this.NewOrderQueued.Next(order);
                }
            }
            else
            {
                this.count++;
                if (this.ProcessedOrder.Priority < order.Priority)
                {
                    this.OrdersDictionary.Add(this.ProcessedOrder.Id, this.ProcessedOrder);
                    this.ProcessedOrder = order;
                    if (this.ProcessedOrder.PrintInLog)
                    {
                        Console.WriteLine("Started Executing: " + this.ProcessedOrder.Name);
                    }

                    this.NewOrderQueued.Next(order);
                }
                else
                {
                    this.OrdersDictionary.Add(this.lastId, order);
                }
            }
        }

        public bool GetNextOrder(bool removeProcessed)
        {
            if (removeProcessed)
            {
                this.ProcessedOrder.Dequeue();
                this.ProcessedOrder = null;
            }

            if (this.count == 0)
            {
                // this.queueEmpty = true;
                this.QueueEmpty.Notify();
                this.ProcessedOrder = null;
                return false;
            }
            else if (this.count == 1 && this.OrdersDictionary.Count == 1)
            {
                this.ProcessedOrder = this.OrdersDictionary.First().Value;
                this.count--;
                this.OrdersDictionary.Remove(this.ProcessedOrder.Id);
                return true;
            }
            else if (this.count > 0 && this.OrdersDictionary.Count > 0)
            {
                this.ProcessedOrder = this.GetHighestPriorityOrder();
                this.count--;
                return true;
            }

            return false;
        }

        public void Initialize()
        {
        }

        public bool Issue()
        {
            //Console.WriteLine("queue update " + this.Unit.PrettyName);
            if (this.queueEmpty)
            {
                return false;
            }

            if (this.sleeper.Sleeping)
            {
                return false;
            }

            if (!this.ProcessedOrder.CanExecute() || this.ProcessedOrder.Canceled)
            {
                if (this.ProcessedOrder.PrintInLog)
                {
                    Console.WriteLine("Finished Executing: " + this.ProcessedOrder.Name);
                }

                if (!this.GetNextOrder(true))
                {
                    return false;
                }

                if (this.ProcessedOrder.PrintInLog)
                {
                    Console.WriteLine("Started Executing: " + this.ProcessedOrder.Name);
                }
            }

            // Console.WriteLine("processing order " + this.processedOrder.OrderType);
            var delay = this.ProcessedOrder.Execute();

            if (delay > 0)
            {
                this.sleeper.Sleep(delay + Game.Ping);
            }

            if (this.ProcessedOrder.ExecuteOnce)
            {
                if (this.GetNextOrder(true))
                {
                    if (this.ProcessedOrder.PrintInLog)
                    {
                        Console.WriteLine("Started Executing: " + this.ProcessedOrder.Name);
                    }
                }
            }

            return true;
        }

        public bool IssueNow(IUnitOrder order)
        {
            if (this.sleeper.Sleeping)
            {
                //Console.WriteLine("sleeping");
                return true;
            }

            if (!order.CanExecute() || order.Canceled)
            {
                //Console.WriteLine("cant execute");
                return false;
            }

             //Console.WriteLine("processing order " + order.OrderType);
            var delay = order.Execute();
            if (order.PrintInLog)
            {
                Console.WriteLine("Started Executing: " + order.Name);
            }

            if (delay > 0)
            {
                this.sleeper.Sleep(delay + Game.Ping);
            }

            if (order.ExecuteOnce)
            {
                return false;
            }

            return true;
        }

        public bool PreciseIssue()
        {
            if (this.queueEmpty)
            {
                return false;
            }

            // if (this.ProcessedOrder.PrintInLog && this.DrawOrder.Value)
            // {
            // this.orderText.Text = this.ProcessedOrder.Name;
            // this.orderText.Color = this.ProcessedOrder.Color;
            // this.orderText.CenterOnRectangle(
            // this.Unit.ScreenInfo.HealthBarPosition,
            // this.Unit.ScreenInfo.HealthBarSize);
            // this.orderText.Position += new Vector2(0, this.Unit.ScreenInfo.HealthBarSize.Y / 2);
            // this.orderText.Draw();
            // }
            if (!this.ProcessedOrder.ShouldExecuteFast)
            {
                return false;
            }

            if (this.sleeper.Sleeping)
            {
                return false;
            }

            if (!this.ProcessedOrder.CanExecute() || this.ProcessedOrder.Canceled)
            {
                if (this.ProcessedOrder.PrintInLog)
                {
                    Console.WriteLine("Finished Executing: " + this.ProcessedOrder.Name);
                }

                if (!this.GetNextOrder(true))
                {
                    return false;
                }

                if (this.ProcessedOrder.PrintInLog)
                {
                    Console.WriteLine("Started Executing: " + this.ProcessedOrder.Name);
                }
            }

            // Console.WriteLine("processing order " + this.processedOrder.OrderType);
            var delay = this.ProcessedOrder.ExecuteFast();

            if (delay > 0)
            {
                this.sleeper.Sleep(delay + Game.Ping);
            }

            if (this.ProcessedOrder.ExecuteOnce)
            {
                if (this.GetNextOrder(true))
                {
                    if (this.ProcessedOrder.PrintInLog)
                    {
                        Console.WriteLine("Started Executing: " + this.ProcessedOrder.Name);
                    }
                }
            }

            return true;
        }

        public void Update()
        {
        }

        #endregion

        #region Methods

        private IUnitOrder GetHighestPriorityOrder()
        {
            var priority = -1u;
            IUnitOrder order = null;

            foreach (var unitOrder in this.OrdersDictionary)
            {
                if (unitOrder.Value.Priority > priority)
                {
                    order = unitOrder.Value;
                    priority = unitOrder.Value.Priority;
                }
            }

            if (order != null)
            {
                this.OrdersDictionary.Remove(order.Id);
            }

            // if (order != null)
            // {
            // this.OrdersDictionary.Remove(order.Id);
            // }
            return order;
        }

        #endregion
    }
}