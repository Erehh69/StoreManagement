using Microsoft.AspNetCore.Mvc;
using StoreManagementApp.Models;
using StoreManagementApp.Data;
using StoreManagementApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

public class POSController : Controller
{
    private readonly StoreContext _context;

    public POSController(StoreContext context)
    {
        _context = context;
    }

    // Display available products
    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products);
    }

    // Add item to the cart
    public IActionResult AddToCart(int productId, int quantity)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == productId);

        if (product == null)
            return BadRequest("Product not found.");

        if (product.Stock < quantity)
            return BadRequest("Insufficient stock.");

        // Get or create the cart
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        // Check if the product already exists in the cart
        var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity; // Update quantity
        }
        else
        {
            // Add new item to cart
            var cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                ProductName = product.Name,
                Price = product.Price
            };
            cart.Add(cartItem);
        }

        // Save the updated cart back to the session
        HttpContext.Session.SetObjectAsJson("Cart", cart);

        return RedirectToAction("Cart");
    }

    // View the cart
    public IActionResult Cart()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        return View(cart);
    }

    // Remove item from the cart
    public IActionResult RemoveFromCart(int productId)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        // Find and remove the item from the cart
        var itemToRemove = cart.FirstOrDefault(c => c.ProductId == productId);
        if (itemToRemove != null)
        {
            cart.Remove(itemToRemove);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }

        return RedirectToAction("Cart");
    }

    //Get the count of items in the cart
    public JsonResult GetCartItemCount()
    {
        // Get the cart from the session
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        // Return the number of items in the cart
        return Json(cart.Sum(c => c.Quantity));
    }



    // Process checkout
    [HttpPost]
    public IActionResult Checkout()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        if (!cart.Any())
            return BadRequest("No items in cart.");

        foreach (var cartItem in cart)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
            if (product == null || product.Stock < cartItem.Quantity)
                return BadRequest("Insufficient stock for some products.");

            // Update stock
            product.Stock -= cartItem.Quantity;

            // Record transaction
            var transaction = new Transaction
            {
                ProductId = cartItem.ProductId,
                Date = DateTime.Now,
                Quantity = cartItem.Quantity,
                IsPurchase = false // Sale transaction
            };
            _context.Transactions.Add(transaction);
        }

        // Save changes to the database and clear cart
        _context.SaveChanges();
        HttpContext.Session.Remove("Cart");

        return RedirectToAction("Index");
    }
}
