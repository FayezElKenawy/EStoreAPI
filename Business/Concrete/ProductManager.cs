using Business.Abstract;
using Business.Constants;
using Business.Helpers.CodeGenerator;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICodeGeneratorService _codeGeneratorService;

        public ProductManager(IProductDal productDal, ICodeGeneratorService codeGeneratorService)
        {
            _productDal = productDal;
            _codeGeneratorService = codeGeneratorService;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            product.Code = _codeGeneratorService.GenerateCode(product);
            product.CreateDate = DateTime.Now;
            product.Active = true;
            _productDal.Add(product);
            return new SuccessResult(BusinessMessages.ProductAdded);
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(BusinessMessages.ProductDeleted);
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), BusinessMessages.ProductsListed);
        }

        public IDataResult<Product> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<Product>(_productDal.Get(c => c.Id == id), BusinessMessages.ProductDetailsListed);
            }

            return new ErrorDataResult<Product>();
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(BusinessMessages.ProductUpdated);
        }

        //Business Rules

        private IResult CheckIfEntityIdValid(int id)
        {
            var result = _productDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}