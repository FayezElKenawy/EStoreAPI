using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IBasketDetailService
    {
        IDataResult<List<BasketDetail>> GetAll();
        IDataResult<List<BasketDetail>> GetAllByBasketId(int basketId);
        IDataResult<BasketDetail> GetById(int id);
        IResult Add(List<BasketDetailDto> dtos);
        IResult Update(BasketDetail basketDetail);
        IResult Delete(BasketDetail basketDetail);
    }
}