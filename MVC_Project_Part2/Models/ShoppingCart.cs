using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project_Part2.Models
{
    public class ShoppingCart
    {
        //Create an object of MenuContext
        private MenuContext db = new MenuContext();
        //Properties
        public int ShoppingCartID { get; set; }
        public const string CartSessionKey = "CartId";
        private int ShoppingCartId { get; set; }

        ///<summary>
        ///This method returns the cart
        ///Static method
        /// </summary>
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartId(context);
            return cart;
        }
        ///<summary>
        ///This method returns the cart
        ///Static method
        /// </summary>
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        ///<summary>
        ///This method adds an item to the cart
        ///Static method
        /// </summary>
        public void AddToCart(Menu_List mennuItem)
        {
            // Get the matching cart and mennuItem instances
            var cartItem = db.Carts.SingleOrDefault(
                c => c.CartID == ShoppingCartId
                && c.ItemID == mennuItem.ItemId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ItemID = mennuItem.ItemId,
                    CartID = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            db.SaveChanges();
        }
        ///<summary>
        ///This method removes an item to the cart
        ///Static method
        /// </summary>
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = db.Carts.Single(
                cart => cart.CartID == ShoppingCartId
                && cart.CartID == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }
                // Save changes
                db.SaveChanges();
            }
            return itemCount;
        }
        ///<summary>
        ///This method makes the cart empty
        ///Static method
        /// </summary>
        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(
                cart => cart.CartID == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            // Save changes
            db.SaveChanges();
        }
        ///<summary>
        ///This method returns the cart items
        /// </summary>
        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(
                cart => cart.CartID == ShoppingCartId).ToList();
        }
        ///<summary>
        ///This method returns the number of cart items in the cart
        /// </summary>
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Carts
                          where cartItems.CartID == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        ///<summary>
        ///This method returns the total amount to be billed
        /// </summary>
        public decimal GetTotal()
        {
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartID == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.ItemID).Sum();

            return total ?? decimal.Zero;
        }

    }
}