using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Class name - OrderNew
    /// A child class that derived from  OrderState class
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class OrderNew : OrderState
    {
        /// <summary>
        /// The OrderNew class constructor, using base class
        /// </summary>
        /// <param name="orderHeader">OrderHeader, an order header object</param>
        public OrderNew(OrderHeader orderHeader) : base(orderHeader) {}

        /// <summary>
        /// OrderStates property - an override property that has its own implementation, to set it to New
        /// </summary>
        /// <value>
        /// OrderStates
        /// </value>
        public override OrderStates State 
        {
            get { return OrderStates.New; }
        }

        /// <summary>
        /// Complete method - an override method that has its own implementation, to throw an exception
        /// </summary>
        /// <param name="OrderState">A reference to _state</param>
        public override void Complete(ref OrderState _state)
        {
            throw new InvalidOrderStateException("Order with 'New' status cannot be changed to 'Complete'.");
        }

        /// <summary>
        /// Reject method - an override method that has its own implementation, to throw an exception
        /// </summary>
        /// <param name="OrderState">A reference to _state</param>
        public override void Reject(ref OrderState _state)
        {
            throw new InvalidOrderStateException("Order with 'New' status cannot be changed to 'Rejected'.");
        }

        /// <summary>
        /// Submit method - an override method that has its own implementation, to set the state to pending
        /// </summary>
        /// <param name="OrderState">A reference to _state</param>
        public override void Submit(ref OrderState _state)
        {
            _state = new OrderPending(_orderHeader);
        }

        /// <summary>
        /// StateId property - always be 1 for a new order
        /// </summary>
        /// <value>
        /// int
        /// </value>
        public int StateId
        {
            get { return 1; }
        }
    }
}