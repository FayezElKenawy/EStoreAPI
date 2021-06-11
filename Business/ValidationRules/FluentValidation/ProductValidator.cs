using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.BrandId).NotEmpty();
            RuleFor(c => c.BrandId).GreaterThan(0);
            RuleFor(c => c.CategoryId).NotEmpty();
            RuleFor(c => c.CategoryId).GreaterThan(0);
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MinimumLength(2);
            RuleFor(c => c.Name).MaximumLength(20);
            RuleFor(c => c.Code).MinimumLength(2);
            RuleFor(c => c.Code).MaximumLength(25);
            RuleFor(c => c.Price).NotEmpty();
            RuleFor(c => c.Price).GreaterThan(0);
            RuleFor(c => c.Stock).NotEmpty();
            RuleFor(c => c.Stock).GreaterThan(0);
        }
    }
}
