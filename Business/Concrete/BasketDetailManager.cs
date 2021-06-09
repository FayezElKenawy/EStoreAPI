using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class BasketDetailManager : IBasketDetailService
    {
        private readonly IBasketDetailDal _basketDetailDal;
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketDetailManager(IBasketDetailDal basketDetailDal, IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            _basketDetailDal = basketDetailDal;
            _basketService = basketService;
            _httpContextAccessor = httpContextAccessor;
        }

        [ValidationAspect(typeof(BasketDetailDtoValidator))]
        public IResult Add(List<BasketDetailDto> dtos)
        {
            var result = BusinessRules.Run(CheckToken());

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            int basketId = CreateNewBasketIfNotHaveActive().Data.Id;
            foreach (var dto in dtos)
            {
                BasketDetail basketDetail = new BasketDetail
                {
                    BasketId = basketId,
                    ProductId = dto.ProductId,
                    Count = dto.Count,
                    CreateDate = DateTime.Now,
                    Active = true
                };

                _basketDetailDal.Add(basketDetail);
            }

            return new SuccessResult(BusinessMessages.BasketDetailAdded);
        }

        public IResult Delete(BasketDetail basketDetail)
        {
            _basketDetailDal.Delete(basketDetail);
            return new SuccessResult(BusinessMessages.BasketDetailDeleted);
        }

        public IDataResult<List<BasketDetail>> GetAll()
        {
            return new SuccessDataResult<List<BasketDetail>>(_basketDetailDal.GetAll(), BusinessMessages.AllBasketDetailsListed);
        }

        public IDataResult<List<BasketDetail>> GetAllByBasketId(int basketId)
        {
            return new SuccessDataResult<List<BasketDetail>>(_basketDetailDal.GetAll(b => b.BasketId == basketId), BusinessMessages.BasketDetailsListedByBasketId);
        }

        public IDataResult<BasketDetail> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<BasketDetail>(_basketDetailDal.Get(c => c.Id == id), BusinessMessages.BasketDetailInfoListed);
            }

            return new ErrorDataResult<BasketDetail>();
        }

        [ValidationAspect(typeof(BasketDetailDtoValidator))]
        public IResult Update(BasketDetail basketDetail)
        {
            _basketDetailDal.Update(basketDetail);
            return new SuccessResult(BusinessMessages.BasketDetailUpdated);
        }

        //Business Rules

        private IResult CheckIfEntityIdValid(int id)
        {
            var result = _basketDetailDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }

        private IResult CheckToken()
        {
            int userId = GetUserIdFromToken();
            if (userId == 0) return new ErrorResult(SystemMessages.WrongTokenSent);

            return new SuccessResult();
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

        private IDataResult<Basket> CreateNewBasketIfNotHaveActive()
        {
            int userId = GetUserIdFromToken();
            var result = _basketService.GetByUserIdActive(userId).Data;

            if(result != null) 
            {
                return new SuccessDataResult<Basket>(result);
            }

            Basket basket = new Basket
            {
                UserId = GetUserIdFromToken()
            };

            _basketService.Add(basket);
            return new SuccessDataResult<Basket>(basket, BusinessMessages.BasketAdded);
        }

    }
}
