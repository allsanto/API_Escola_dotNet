﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.API.Data.Entities
{
    public class ProfessorEntity
    {
        public int IdProfessor { get; set; }
        public string Nome{ get; set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Status { get; set; }
        public int IdUnidade{ get; set; }
    }
}
