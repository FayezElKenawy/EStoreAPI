using Business.Abstract;
using Entities.Concrete;
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
    public class BasketDetailsController : ControllerBase
    {
        private readonly IBasketDetailService _basketDetailService;

        public BasketDetailsController(IBasketDetailService basketDetailService)
        {
            _basketDetailService = basketDetailService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _basketDetailService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _basketDetailService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(BasketDetail basketDetail)
        {
            var result = _basketDetailService.Add(basketDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(BasketDetail basketDetail)
        {
            var result = _basketDetailService.Update(basketDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(BasketDetail basketDetail)
        {
            var result = _basketDetailService.Delete(basketDetail);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
