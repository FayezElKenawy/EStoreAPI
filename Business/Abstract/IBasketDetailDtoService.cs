using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface IBasketDetailDtoService
    {
        IDataResult<List<BasketDetailDto>> GetAll();
        IDataResult<List<BasketDetailDto>> GetAllActive();
        IResult Add(List<BasketDetailDto> basketDetailDtos);
    }
}
