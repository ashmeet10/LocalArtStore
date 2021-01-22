using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManger;

        public OrdersController(IDutchRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper,
            UserManager<StoreUser> userManger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManger = userManger;

        }

        [HttpGet]
        public ActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;
                var results = _repository.GetAllOrdersByUser(username, includeItems);
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders:{ex.Message}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Order>> Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(User.Identity.Name, id);

                if (order != null)
                    return Ok(_mapper.Map<Order, OrderViewModel>(order));
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders : {ex.Message}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderViewModel model)
        {
            //add it to the database

            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    /*new Order()
                {
                    OrderDate = model.OrderDate,
                    OrderNumber=model.OrderNumber,
                    Id=model.OrderId
                };*/
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser = await _userManger.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;

                    // _repository.AddEntity(newOrder);
                    _repository.AddOrder(newOrder);
                    if (_repository.SaveAll())
                    {
                        /* var vm = new OrderViewModel()
                         {
                             OrderId = newOrder.Id,
                             OrderDate = newOrder.OrderDate,
                             OrderNumber = newOrder.OrderNumber
                         };*/
                        // return Created($"/api/orders/{vm.OrderId}", vm);
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));

                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new order: {ex.Message}");
            }
            return BadRequest("Failed to save new order");
        }
    }
}