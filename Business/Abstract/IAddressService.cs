using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IAddressService
    {
        IDataResult<List<Address>> GetAll();
        IDataResult<Address> GetById(int id);
        IResult Add(Address address);
        IResult Update(Address address);
        IResult Delete(Address address);
    }
}