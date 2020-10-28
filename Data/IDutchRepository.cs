using DutchTreat.Data.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(string username, int id);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);


        void AddEntity(object model);

        bool SaveAll();
    }
}