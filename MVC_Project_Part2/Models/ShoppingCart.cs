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
            cart.ShoppingCartID = Convert.ToInt32(cart.GetCartId(context)); //This line might throw an exception
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
        ///<summary>
        ///This method creates an order during the checkout
        /// </summary>
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ItemId = item.ItemID,
                    OrderID = order.OrderID,
                    UnitPrice = item.ItemID,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.ItemID);

                db.OrderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            db.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderID;
        }
        ///<summary>
        ///This method returns the cart id
        /// </summary>
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        /////<summary>
        /////This method checks user login status to migrate their cart
        ///// </summary>
        //// When a user has logged in, migrate their shopping cart to
        //// be associated with their username
        //public void MigrateCart(string userName)
        //{
        //    var shoppingCart = db.Carts.Where(
        //        c => c.CartID == ShoppingCartId);

        //    foreach (Cart item in shoppingCart)
        //    {
        //        item.CartID = userName;
        //    }
        //    db.SaveChanges();
        //}
    }
}