using System;

namespace Escola.API.Domain.Models.Request
{
    public class ProfessorRequest
    {
        public int ? IdProfessor { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }
        public int IdUnidade { get; set; }
    }
}
