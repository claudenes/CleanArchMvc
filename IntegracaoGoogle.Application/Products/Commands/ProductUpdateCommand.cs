using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegracaoGoogle.Application.Products.Commands;

namespace IntegracaoGoogle.Application.Products.Commands
{
    public class ProductUpdateCommand : ProductCommand
    {
        public int Id { get; set; }
        public ProductUpdateCommand(int id)
        {
            Id = id;
        }
    }
}
