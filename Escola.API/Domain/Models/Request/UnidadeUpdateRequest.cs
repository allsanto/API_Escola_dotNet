using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.API.Domain.Models.Request
{
    public class UnidadeUpdateRequest : UnidadeRequest
    {
        public int IdUnidade { get; set; }
    }
}
