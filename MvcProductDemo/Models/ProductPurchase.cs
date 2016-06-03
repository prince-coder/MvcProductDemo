using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcProductDemo.Models
{
    public class ProductPurchase
    { 
        [Key]
   
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CustId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}