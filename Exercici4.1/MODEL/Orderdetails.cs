using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Exercici4._1.MODEL
{
    public partial class Orderdetails
    {
        public int OrderNumber { get; set; }
        public string ProductCode { get; set; }
        public int QuantityOrdered { get; set; }
        public decimal PriceEach { get; set; }
        public short OrderLineNumber { get; set; }

        public virtual Orders OrderNumberNavigation { get; set; }
        public virtual Products ProductCodeNavigation { get; set; }
    }
}
