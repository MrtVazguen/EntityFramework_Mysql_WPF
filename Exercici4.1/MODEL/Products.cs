using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Exercici4._1.MODEL
{
    public partial class Products
    { 

        public Products()
        {
            Orderdetails = new HashSet<Orderdetails>();
        }
      
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductLine { get; set; }
        public string ProductScale { get; set; }
        public string ProductVendor { get; set; }
        public short QuantityInStock { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal Msrp { get; set; }
        public string ProductDescription { get; set; }
        public virtual Productlines ProductLineNavigation { get; set; }
        public virtual ICollection<Orderdetails> Orderdetails { get; set; }
        
    }
}
