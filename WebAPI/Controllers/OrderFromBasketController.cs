using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderFromBasketController : ControllerBase
    {
        private readonly IOrderFromBasketService _orderFromBasketService;

        public OrderFromBasketController(IOrderFromBasketService orderFromBasketService)
        {
            _orderFromBasketService = orderFromBasketService;
        }

        [HttpPost("order")]
        public IActionResult Order(OrderFromBasketDto dto)
        {
            var result = _orderFromBasketService.Order(dto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
