using Business.Abstract;
using Business.Constants;
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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Add(Category category)
        {
            category.CreateDate = DateTime.Now;
            category.Active = true;
            _categoryDal.Add(category);
            return new SuccessResult(BusinessMessages.CategoryAdded);
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(BusinessMessages.CategoryDeleted);
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(), BusinessMessages.CategoriesListed);
        }

        public IDataResult<Category> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<Category>(_categoryDal.Get(c => c.Id == id), BusinessMessages.CategoryDetailsListed);
            }

            return new ErrorDataResult<Category>();
        }

        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(BusinessMessages.CategoryUpdated);
        }

        //Business Rules

        private IResult CheckIfEntityIdValid(int id)
        {
            var result = _categoryDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}