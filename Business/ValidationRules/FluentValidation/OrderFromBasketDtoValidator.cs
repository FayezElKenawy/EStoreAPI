using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class OrderFromBasketDtoValidator : AbstractValidator<OrderFromBasketDto>
    {
        public OrderFromBasketDtoValidator()
        {
            RuleFor(o => o.PostalCode).NotEmpty();
            RuleFor(o => o.PostalCode).MinimumLength(5);
            RuleFor(o => o.PostalCode).MaximumLength(16);
            RuleFor(o => o.AddressDetail).NotEmpty();
            RuleFor(o => o.AddressDetail).MinimumLength(5);
            RuleFor(o => o.AddressDetail).MaximumLength(200);
        }
    }
}
