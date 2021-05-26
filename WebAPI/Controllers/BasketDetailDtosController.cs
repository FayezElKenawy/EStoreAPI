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
    public class BasketDetailDtosController : ControllerBase
    {
        readonly IBasketDetailDtoService _basketDetailDtoService;

        public BasketDetailDtosController(IBasketDetailDtoService basketDetailDtoService)
        {
            _basketDetailDtoService = basketDetailDtoService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _basketDetailDtoService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getallactive")]
        public IActionResult GetAllActive()
        {
            var result = _basketDetailDtoService.GetAllActive();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(List<BasketDetailDto> basketDetailDtos)
        {
            var result = _basketDetailDtoService.Add(basketDetailDtos);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
