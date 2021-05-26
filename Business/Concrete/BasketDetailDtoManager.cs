using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class BasketDetailDtoManager : IBasketDetailDtoService
    {
        private readonly IBasketDetailDal _basketDetailDal;
        private readonly IBasketDetailService _basketDetailService;
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketDetailDtoManager(IBasketDetailDal basketDetailDal, IBasketService basketService, IBasketDetailService basketDetailService, IHttpContextAccessor httpContextAccessor)
        {
            _basketDetailDal = basketDetailDal;
            _basketService = basketService;
            _basketDetailService = basketDetailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IResult Add(List<BasketDetailDto> basketDetailDtos)
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.ElementAt(0).Value);
            var result = BusinessRules.Run(CreateBasketForUserIfNotHave(userId));

            if (result == null)
            {
                foreach (var basketDetailDto in basketDetailDtos)
                {
                    _basketDetailService.Add(new BasketDetail
                    {
                        BasketId = _basketService.GetByUserId(userId).Data.Id,
                        ProductId = basketDetailDto.ProductId,
                        Count = basketDetailDto.Count
                    });
                }

                return new SuccessResult(BusinessMessages.BasketDetailsAdded);
            }

            return new ErrorResult();
        }

        public IDataResult<List<BasketDetailDto>> GetAll()
        {
            return new SuccessDataResult<List<BasketDetailDto>>(_basketDetailDal.GetAllBasketDetailDtos(), BusinessMessages.AllBasketDetailDtosListed);
        }

        public IDataResult<List<BasketDetailDto>> GetAllActive()
        {
            return new SuccessDataResult<List<BasketDetailDto>>(_basketDetailDal.GetAllBasketDetailDtos(b => b.Active == true), BusinessMessages.AllActiveBasketDetailDtosListed);
        }

        //Business Rules

        private IResult CreateBasketForUserIfNotHave(int userId)
        {
            var result = _basketService.GetByUserId(userId).Data;
            if (result == null)
            {
                _basketService.Add(new Basket { UserId = userId });
            }

            return new SuccessResult();
        }

    }
}
