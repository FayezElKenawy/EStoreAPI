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
    public class AddressManager : IAddressService
    {
        private readonly IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }

        [ValidationAspect(typeof(AddressValidator))]
        public IResult Add(Address address)
        {
            _addressDal.Add(address);
            return new SuccessResult(BusinessMessages.AddressAdded);
        }

        [ValidationAspect(typeof(AddressValidator))]
        public IResult Delete(Address address)
        {
            _addressDal.Delete(address);
            return new SuccessResult(BusinessMessages.AddressDeleted);
        }

        public IDataResult<List<Address>> GetAll()
        {
            return new SuccessDataResult<List<Address>>(_addressDal.GetAll(), BusinessMessages.AddressesListed);
        }

        public IDataResult<Address> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<Address>(_addressDal.Get(c => c.Id == id), BusinessMessages.AddressDetailsListed);
            }

            return new ErrorDataResult<Address>();
        }

        [ValidationAspect(typeof(AddressValidator))]
        public IResult Update(Address address)
        {
            _addressDal.Update(address);
            return new SuccessResult(BusinessMessages.AddressUpdated);
        }

        //Business Rules

        private IResult CheckIfEntityIdValid(int id)
        {
            var result = _addressDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}