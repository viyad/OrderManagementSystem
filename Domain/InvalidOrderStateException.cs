using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Class name - InvalidOrderStateException
    /// It extends the Exception class
    /// InvalidOrderStateException
    /// o   InvalidOrderStateException(string message) 
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class InvalidOrderStateException : Exception
    {
        /// <summary>
        /// The interface of Exception class
        /// </summary>
        /// <param name="message">string, message to be thrown and displayed</param>
        public InvalidOrderStateException(string message) : base(message)
        {
            
        }
    }
}