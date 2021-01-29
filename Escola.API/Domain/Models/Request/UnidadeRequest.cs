using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.API.Domain.Models.Request
{
    public class UnidadeRequest
    {
            public int ? IdUnidade { get; set; }
            public string Nome { get; set; }
            public string Endereco { get; set; }
    }
}
