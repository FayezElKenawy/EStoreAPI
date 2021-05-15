using Business.Helpers.CodeGenerator;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.CodeGenerator
{
    public class CodeGeneratorManager : ICodeGeneratorService
    {
        public string GenerateCode(Product product)
        {
            string code = $"{product.CategoryId}{product.BrandId}{product.Name.Replace(" ", "").ToLower()}";
            return code;
        }
    }
}
