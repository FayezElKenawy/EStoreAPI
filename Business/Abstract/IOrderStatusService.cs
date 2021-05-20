using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IOrderStatusService
    {
        IDataResult<List<OrderStatus>> GetAll();
        IDataResult<OrderStatus> GetById(int id);
        IResult Add(OrderStatus orderStatus);
        IResult Update(OrderStatus orderStatus);
        IResult Delete(OrderStatus orderStatus);
    }
}