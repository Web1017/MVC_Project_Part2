using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Reference to the Models folder
using MVC_Project_Part2.Models;

namespace MVC_Project_Part2.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}