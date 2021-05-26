using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class BasketDetailDtoValidator : AbstractValidator<BasketDetailDto>
    {
        public BasketDetailDtoValidator()
        {
            RuleFor(b => b.ProductId).NotEmpty();
            RuleFor(b => b.ProductId).GreaterThan(0);
            RuleFor(b => b.Count).NotEmpty();
            RuleFor(b => b.Count).GreaterThan(0);
        }
    }
}
