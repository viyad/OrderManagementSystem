using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Class name - OrderComplete
    /// A child class that derived from  OrderState class
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class OrderComplete : OrderState
    {
        /// <summary>
        /// The OrderComplete class constructor, using base class
        /// </summary>
        /// <param name="orderHeader">OrderHeader, an order header object</param>
        public OrderComplete(OrderHeader orderHeader) : base(orderHeader) { }

        /// <summary>
        /// OrderStates property - an override property that has its own implementation, to set it to Complete
        /// </summary>
        /// <value>
        /// OrderStates
        /// </value>
        public override OrderStates State
        {
            get { return OrderStates.Complete; }
        }

        /// <summary>
        /// Complete method - an override method that has its own implementation, to throw an exception
        /// </summary>
        /// <param name="OrderState">A reference to _state</param>
        public override void Complete(ref OrderState _state)
        {
            throw new InvalidOrderStateException("Order with 'Complete' status cannot be changed to 'Complete'.");
        }

        /// <summary>
        /// Reject method - an override method that has its own implementation, to throw an exception
        /// </summary>
        /// <param name="OrderState">A reference to _state</param>
        public override void Reject(ref OrderState _state)
        {
            throw new InvalidOrderStateException("Order with 'Complete' status cannot be changed to 'Reject'.");
        }

        /// <summary>
        /// Submit method - an override method that has its own implementation, to throw an exception
        /// </summary>
        /// <param name="OrderState">A reference to _state</param>
        public override void Submit(ref OrderState _state)
        {
            throw new InvalidOrderStateException("Order with 'Complete' status cannot be changed to 'Pending'.");
        }
    }
}