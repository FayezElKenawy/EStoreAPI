using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class OrderFromBasketManager : IOrderFromBasketService
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly IBasketDetailService _basketDetailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public OrderFromBasketManager(IOrderDetailService orderDetailService, IOrderService orderService, IBasketDetailService basketDetailService, IHttpContextAccessor httpContextAccessor, IProductService productService, IBasketService basketService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _basketDetailService = basketDetailService;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            _basketService = basketService;
        }

        [ValidationAspect(typeof(OrderFromBasketDtoValidator))]
        public IResult Order(OrderFromBasketDto dto)
        {
            var result = BusinessRules.Run(CreateNewOrder(dto));

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            int userId = GetUserIdFromToken();
            int basketId = _basketService.GetByUserIdActive(userId).Data.Id;
            var basketDetails = _basketDetailService.GetAllByBasketId(basketId).Data;

            foreach (var basketDto in basketDetails)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    ProductId = basketDto.ProductId,
                    Count = basketDto.Count,
                    OrderId = GetOrderIdForUser(userId),
                    SalePrice = GetSalePriceForProduct(basketDto.ProductId, basketDto.Count),
                    CreateDate = DateTime.Now,
                    Active = true
                };

                _orderDetailService.Add(orderDetail);
            }

            _basketService.SetPassive(basketId);
            return new SuccessResult(BusinessMessages.ProductsOrdered);
        }

        // Business Rules

        private IResult CreateNewOrder(OrderFromBasketDto dto)
        {
            int userId = GetUserIdFromToken();
            if (userId == 0)
            {
                return new ErrorResult(SystemMessages.WrongTokenSent);
            }

            AddressDto addressDto = new AddressDto
            {
                UserId = userId,
                CityId = dto.CityId,
                AddressDetail = dto.AddressDetail,
                PostalCode = dto.PostalCode
            };

            _orderService.AddAsDto(addressDto);
            return new SuccessResult(BusinessMessages.OrderAdded);
        }

        private int GetUserIdFromToken()
        {
            try
            {
                return Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.ElementAt(0).Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private decimal GetSalePriceForProduct(int productId, int count)
        {
            var result = _productService.GetById(productId).Data;
            return result.Price * count;
        }

        private int GetOrderIdForUser(int userId)
        {
            var result = _orderService.GetByUserIdActive(userId).Data;
            return result.Id;
        }

    }
}
