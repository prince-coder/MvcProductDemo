using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProductDemo.Models
{
    public class Customer
    {
        [Key]
        public int CustId { get; set; }
        public string CustName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Please enter valid E-mail Address")]
        public string emailId { get; set; }
        public string phone { get; set; }

    }
}