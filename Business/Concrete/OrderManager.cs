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
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        [ValidationAspect(typeof(OrderValidator))]
        public IResult Add(Order order)
        {
            _orderDal.Add(order);
            return new SuccessResult(BusinessMessages.OrderAdded);
        }

        [ValidationAspect(typeof(OrderValidator))]
        public IResult Delete(Order order)
        {
            _orderDal.Delete(order);
            return new SuccessResult(BusinessMessages.OrderDeleted);
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(), BusinessMessages.OrdersListed);
        }

        public IDataResult<Order> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<Order>(_orderDal.Get(c => c.Id == id), BusinessMessages.OrderDetailsListed);
            }

            return new ErrorDataResult<Order>();
        }

        [ValidationAspect(typeof(OrderValidator))]
        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult(BusinessMessages.OrderUpdated);
        }

        //Business Rules

        private IResult CheckIfEntityIdValid(int id)
        {
            var result = _orderDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}