using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<Order>> GetAll();
        IDataResult<Order> GetById(int id);
        IDataResult<Order> GetByUserIdActive(int userId);
        IResult AddAsEntity(Order order);
        IResult AddAsDto(AddressDto dto);
        IResult Update(Order order);
        IResult Delete(Order order);

    }
}