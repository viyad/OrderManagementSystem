using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Class name - OrderState
    /// An abstract class that has state data of an order header
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public abstract class OrderState
    {
        /// <summary>
        /// Store for the order header, this can be accessed by all its childern classes
        /// </summary>
        protected OrderHeader _orderHeader;

        /// <summary>
        /// The OrderState class constructor.
        /// </summary>
        /// <param name="orderHeader">An OrderHeader, an object of an order header</param>
        public OrderState(OrderHeader orderHeader)
        {
            _orderHeader = orderHeader;
        }

        /// <summary>
        /// State property procedure - an abstract property, the implementation is in each child class
        /// </summary>
        /// <value>
        /// OrderStates
        /// </value>
        public abstract OrderStates State
        {
            get;
        }

        /// <summary>
        /// This method is an abstract so its children must have their own implementation
        /// </summary>
        /// <param name="_state">A reference to its _state</param>
        public abstract void Complete(ref OrderState _state);

        /// <summary>
        /// This method is an abstract so its children must have their own implementation
        /// </summary>
        /// <param name="_state">A reference to its _state</param>
        public abstract void Reject(ref OrderState _state);

        /// <summary>
        /// This method is an abstract so its children must have their own implementation
        /// </summary>
        /// <param name="_state">A reference to its _state</param>
        public abstract void Submit(ref OrderState _state);
    }
}