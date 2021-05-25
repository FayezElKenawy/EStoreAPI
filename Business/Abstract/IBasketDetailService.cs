using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IBasketDetailService
    {
        IDataResult<List<BasketDetail>> GetAll();
        IDataResult<BasketDetail> GetById(int id);
        IResult Add(BasketDetail basketDetail);
        IResult Update(BasketDetail basketDetail);
        IResult Delete(BasketDetail basketDetail);
    }
}