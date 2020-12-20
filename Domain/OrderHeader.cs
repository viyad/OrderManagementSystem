using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Class name - OrderHeader
    /// OrderHeader is a class to keep data of an order header and its order item
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class OrderHeader
    {
        /// <summary>
        /// Store for its order items
        /// </summary>
        private List<OrderItem> _orderItems = new List<OrderItem>();

        /// <summary>
        /// Store for its state
        /// </summary>
        private OrderState _state;

        /// <summary>
        /// Store for its id
        /// </summary>
        private int _id;

        /// <summary>
        /// Store for its date and time of creation
        /// </summary>
        private DateTime _dateTime;

        /// <summary>
        /// The OrderHeader class constructor without any passing parameters.
        /// </summary>
        public OrderHeader(){}

        /// <summary>
        /// The OrderHeader class constructor.
        /// </summary>
        /// <param name="id">An integer, its order header id</param>
        /// <param name="dateTime">A date and time, its date and time of creation</param>
        /// <param name="stateId">Its state id, the id of its state</param>
        public OrderHeader(int id, DateTime dateTime, int stateId)
        {
            _id = id;
            _dateTime = dateTime;
            setState(stateId);
        }

        /// <summary>
        /// Id property - get and set the value of the order header's id
        /// </summary>
        /// <value>
        /// Integer
        /// </value>
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        /// <summary>
        /// DateTime property - get and set the value of the order header's date and time
        /// </summary>
        /// <value>
        /// DateTime
        /// </value>
        public DateTime DateTime
        {
            get { return _dateTime; }
            private set { _dateTime = value; }
        }

        /// <summary>
        /// State property - get and set the value of the order header's state
        /// </summary>
        /// <value>
        /// OrderState
        /// </value>
        public OrderState State
        {
            get { return _state; }
        }

        /// <summary>
        /// OrderItems property - get the list of all the order items of the order header
        /// </summary>
        /// <value>
        /// List<OrderItem>
        /// </value>
        public List<OrderItem> OrderItems
        {
            get { return _orderItems; }
        }

        /// <summary>
        /// Total property - get the total number of order items belong to this order header
        /// </summary>
        /// <value>
        /// decimal
        /// </value>
        public decimal Total
        {
            get
            {
                decimal total = 0;
                if (_orderItems.Any())
                    foreach (OrderItem item in _orderItems)
                        total += item.Total;

                return total;
            }
        }

        /// <summary>
        /// To add an order item into the order header's order item.
        /// </summary>
        /// <param name="stockItemId">An integer, the stock id of this order item</param>
        /// <param name="price">A deecimal, the price of this order item</param>
        /// <param name="quantity">An integer, the quantity of this order item</param>
        /// <param name="description">A string, the description of this order item</param>
        public void AddOrderItem(int stockItemId, decimal price, int quantity, string description)
        {
            OrderItem orderItem = new OrderItem(this, stockItemId, price, quantity, description);
            _orderItems.Add(orderItem);
        }

        /// <summary>
        /// To set its state to Complete.
        /// </summary>
        public void Complete()
        {
            _state.Complete(ref _state);
        }

        /// <summary>
        /// To set its state to Reject.
        /// </summary>
        public void Reject()
        {
            _state.Reject(ref _state);
        }

        /// <summary>
        /// To set its state to Pending.
        /// </summary>
        public void Submit()
        {
            if (_orderItems.Count > 0)
                _state.Submit(ref _state);
            else
                throw new InvalidOrderStateException("An order without items cannot be submitted.");
        }

        /// <summary>
        /// To set its state to the OrderState according to the given state id.
        /// </summary>
        /// <param name="stateId">An integer, the id of the state</param>
        private void setState(int stateId)
        {
            switch (stateId)
            {
                case 1: // New
                    _state = new OrderNew(this);
                    break;
                case 2: // Pending
                    _state = new OrderPending(this);
                    break;
                case 3: // Rejected
                    _state = new OrderRejected(this);
                    break;
                case 4: // Complete
                    _state = new OrderComplete(this);
                    break;
                default:
                    throw new InvalidOrderStateException("Invalid State Id: " + stateId.ToString());
            }
        }
    }
}