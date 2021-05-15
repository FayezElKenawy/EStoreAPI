using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.CodeGenerator
{
    public interface ICodeGeneratorService
    {
        string GenerateCode(Product product);
    }
}
