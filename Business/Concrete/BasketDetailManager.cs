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
            var result = BusinessRules.Run(CheckToken(), CreateBasketForUserIfNotHave());

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            foreach (var dto in dtos)
            {
                BasketDetail basketDetail = new BasketDetail
                {
                    BasketId = GetBasketByUserId().Data.Id,
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

        public IDataResult<List<BasketDetail>> GetAllByUserIdActive(int userId)
        {
            return new SuccessDataResult<List<BasketDetail>>(_basketDetailDal.GetAll(b => b.Basket.UserId == userId && b.Active == true), BusinessMessages.ActiveBasketDetailsForUserListed);
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

        private IResult CreateBasketForUserIfNotHave()
        {
            int userId = GetUserIdFromToken();
            if (userId == 0) return new ErrorResult(SystemMessages.WrongTokenSent);

            var result = _basketService.GetByUserId(userId).Data;

            if (result == null)
            {
                _basketService.Add(new Basket { UserId = userId });
            }

            return new SuccessResult();
        }

        private IDataResult<Basket> GetBasketByUserId()
        {
            int userId = GetUserIdFromToken();
            var result = _basketService.GetByUserId(userId).Data;

            return new SuccessDataResult<Basket>(result);
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

    }
}
