using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IBasketService
    {
        IDataResult<List<Basket>> GetAll();
        IDataResult<Basket> GetById(int id);
        IDataResult<Basket> GetByUserIdActive(int userId);
        IResult SetPassive(int id);
        IResult Add(Basket basket);
        IResult Update(Basket basket);
        IResult Delete(Basket basket);
    }
}