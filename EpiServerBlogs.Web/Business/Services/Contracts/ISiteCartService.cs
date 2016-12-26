using System;
using System.Collections.Generic;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Commerce.Order;
using Mediachase.Commerce.Orders;

namespace EpiServerBlogs.Web.Business.Services.Contracts
{
    public interface ISiteCartService
    {
        ICart GetCart(string name);

        bool AddToCart(ICart cart, SiteVariationContent variationContent, out string errorMessage);

        bool RemoveFromCart(ICart cart, string code, out string errorMessage);

        int GetTotalCartQuantity(string name);

        IEnumerable<Cart> GetAllCarts(Guid customerId);
        
        string DefaultCartName { get; }
    }
}