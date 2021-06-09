using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IAddressService _addressService;

        public OrderManager(IOrderDal orderDal, IAddressService addressService)
        {
            _orderDal = orderDal;
            _addressService = addressService;
        }

        [ValidationAspect(typeof(OrderValidator))]
        public IResult AddAsEntity(Order order)
        {
            _orderDal.Add(order);
            return new SuccessResult(BusinessMessages.OrderAdded);
        }

        [ValidationAspect(typeof(AddressDtoValidator))]
        public IResult AddAsDto(AddressDto dto)
        {
            Order order = new Order
            {
                UserId = dto.UserId,
                AddressId = GetAddressId(dto),
                OrderStatusId = 1,
                CreateDate = System.DateTime.Now,
                Active = true
            };

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

        public IDataResult<Order> GetByUserIdActive(int userId)
        {
            return new SuccessDataResult<Order>(_orderDal.Get(c => c.UserId == userId && c.Active == true));
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

        private int GetAddressId(AddressDto dto)
        {
            return CreateNewAddress(dto).Data.Id;
        }

        private IDataResult<Address> CreateNewAddress(AddressDto dto)
        {
            Address address = new Address
            {
                UserId = dto.UserId,
                CityId = dto.CityId,
                AddressDetail = dto.AddressDetail,
                PostalCode = dto.PostalCode,
                CreateDate = System.DateTime.Now,
                Active = true
            };

            _addressService.Add(address);
            return new SuccessDataResult<Address>(address, BusinessMessages.AddressAdded);
        }
    }
}