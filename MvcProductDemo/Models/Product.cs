using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcProductDemo.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        
        [StringLength(50,MinimumLength =1,ErrorMessage ="Product Name should be between 1 to 50 characters.")]
        public string ProductName { get; set; }
       // [Column("Price")]
       // [UIHint("NumberTemplate")]
        public int Price { get; set; }
        [StringLength(200,ErrorMessage ="Description cannot be longer than 200 characters.")]
        public string  Description { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<ProductPurchase> ProductPurchases { get; set; }
       // public virtual Int32 Number { get; set; }
    }
}