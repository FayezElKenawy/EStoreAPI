using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class BasketDetailManager : IBasketDetailService
    {
        private readonly IBasketDetailDal _basketDetailDal;

        public BasketDetailManager(IBasketDetailDal basketDetailDal)
        {
            _basketDetailDal = basketDetailDal;
        }

        public IResult Add(BasketDetail basketDetail)
        {
            _basketDetailDal.Add(basketDetail);
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

        public IDataResult<BasketDetail> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<BasketDetail>(_basketDetailDal.Get(c => c.Id == id), BusinessMessages.BasketDetailInfoListed);
            }

            return new ErrorDataResult<BasketDetail>();
        }

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
    }
}