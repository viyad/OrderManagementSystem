using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Class name - OrderPending
    /// A child class that derived from  OrderState class
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class OrderPending : OrderState
    {
        /// <summary>
        /// The OrderPending class constructor, using base class
        /// </summary>
        /// <param name="orderHeader">OrderHeader, an order header object</param>
        public OrderPending(OrderHeader orderHeader) : base(orderHeader) { }

        /// <summary>
        /// OrderStates property - an override property that has its own implementation, to set it to Pending
        /// </summary>
        /// <value>
        /// OrderStates
        /// </value>
        public override OrderStates State
        {
            get { return OrderStates.Pending; }
        }

        /// <summary>
        /// Complete method - an override method that has its own implementation, to set it to Complete
        /// </summary>
        /// <param name="OrderState">A reference to _state</param>
        public override void Complete(ref OrderState _state)
        {
            _state = new OrderComplete(_orderHeader);
        }

        /// <summary>
        /// Reject method - an override method that has its own implementation, to set it to Reject
        /// </summary>
        /// <param name="OrderState">A reference to _state</param>
        public override void Reject(ref OrderState _state)
        {
            _state = new OrderRejected(_orderHeader);
        }

        /// <summary>
        /// Submit method - an override method that has its own implementation, to throw an exception
        /// </summary>
        /// <param name="OrderState">A reference to _state</param>
        public override void Submit(ref OrderState _state)
        {
            throw new InvalidOrderStateException("Order with 'Pending' status cannot be changed to 'Pending'.");
        }
    }
}