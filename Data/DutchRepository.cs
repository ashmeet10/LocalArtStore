using DutchTreat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx,ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            try
            { if (includeItems)
                {
                    _logger.LogInformation("GetAllOrders got called");

                    //return _ctx.Orders.ToList();
                    return _ctx.Orders
                                .Include(o => o.Items)
                                .ThenInclude(i => i.Product)
                                .ToList();
                }
                else
                {
                    return _ctx.Orders
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all orders{ex}");
                return null;
            }
        }


        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts got called");

                return _ctx.Products
                            .OrderBy(p => p.Title)
                            .ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get all products{ex}");
                return null;
            }
        }

        public Order GetOrderById(string username , int id)
        {
            return _ctx.Orders
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .Where(o =>o.Id == id && o.User.UserName == username)
                        .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                    .Where(p => p.Category == category)
                    .ToList();
        }
        public bool SaveAll()
        {
           return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);

        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            try
            {
                if (includeItems)
                {
                    _logger.LogInformation("GetAllOrders got called");

                    //return _ctx.Orders.ToList();
                    return _ctx.Orders
                                .Where(o=>o.User.UserName == username)
                                .Include(o => o.Items)
                                .ThenInclude(i => i.Product)
                                .ToList();
                }
                else
                {
                    return _ctx.Orders
                               .Where(o => o.User.UserName == username)
                               .ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all orders{ex}");
                return null;
            }
        }

        
    }
    
}
