using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class OrderStatusManager : IOrderStatusService
    {
        private readonly IOrderStatusDal _orderStatusDal;

        public OrderStatusManager(IOrderStatusDal orderStatusDal)
        {
            _orderStatusDal = orderStatusDal;
        }

        [ValidationAspect(typeof(OrderStatusValidator))]
        public IResult Add(OrderStatus orderStatus)
        {
            _orderStatusDal.Add(orderStatus);
            return new SuccessResult(BusinessMessages.OrderStatusAdded);
        }

        [ValidationAspect(typeof(OrderStatusValidator))]
        public IResult Delete(OrderStatus orderStatus)
        {
            _orderStatusDal.Delete(orderStatus);
            return new SuccessResult(BusinessMessages.OrderStatusDeleted);
        }

        public IDataResult<List<OrderStatus>> GetAll()
        {
            return new SuccessDataResult<List<OrderStatus>>(_orderStatusDal.GetAll(), BusinessMessages.OrderStatusesListed);
        }

        public IDataResult<OrderStatus> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<OrderStatus>(_orderStatusDal.Get(c => c.Id == id), BusinessMessages.OrderStatusDetailsListed);
            }

            return new ErrorDataResult<OrderStatus>();
        }

        [ValidationAspect(typeof(OrderStatusValidator))]
        public IResult Update(OrderStatus orderStatus)
        {
            _orderStatusDal.Update(orderStatus);
            return new SuccessResult(BusinessMessages.OrderStatusUpdated);
        }


        //Business Rules

        private IResult CheckIfEntityIdValid(int id)
        {
            var result = _orderStatusDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}