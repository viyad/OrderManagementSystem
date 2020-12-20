using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// enum - OrderStates
    /// To keep the constant values of the states
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public enum OrderStates
    {
        New = 1,
        Pending = 2,
        Rejected = 3,
        Complete = 4
    }
}