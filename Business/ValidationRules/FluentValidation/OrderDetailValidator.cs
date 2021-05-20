using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class OrderDetailValidator : AbstractValidator<OrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(od => od.OrderId).NotEmpty();
            RuleFor(od => od.OrderId).GreaterThan(0);
            RuleFor(od => od.ProductId).NotEmpty();
            RuleFor(od => od.ProductId).GreaterThan(0);
            RuleFor(od => od.SalePrice).NotEmpty();
            RuleFor(od => od.SalePrice).GreaterThan(0);
            RuleFor(od => od.Count).NotEmpty();
            RuleFor(od => od.Count).GreaterThan(0);
        }
    }
}
