using Escola.API.Business;
using Escola.API.Data.Entities;
using Escola.API.Domain.Models.Request;
using Escola.API.Domain.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Escola.API.Controllers
{
    [Route("api/alunos")]
    public class AlunosController : Controller
    {
        private readonly AlunoBL _alunoBL;
        public AlunosController(AlunoBL alunoBL)
        {
            _alunoBL = alunoBL;
        }

        /// <summary>
        /// Cadastrar alunos
        /// </summary>
        /// <param name="alunoRequest">JSON</param>
        /// <returns>JSON</returns>
        // Função para cadastro de aluno
        [HttpPost]
        [Route("insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AlunoRequest alunoRequest)
        {
            var idAluno = _alunoBL.InsertAluno(alunoRequest);

            return CreatedAtAction(nameof(GetById), new { id = idAluno }, alunoRequest); // Status code 201
            // return Ok(new Response { Message = "Usuario cadastrado com sucesso"});

        }


        // Função para atualizar o cadastro do Aluno
        [HttpPut]
        [Route("update")]
        public IActionResult Put([FromBody] AlunoRequest alunoUpdateReq)
        {
            if (alunoUpdateReq.IdAluno.GetValueOrDefault(0) <= 0)
            {
                return BadRequest(new { message = "Informe um aluno" });
            }

            var linhasAfedatas = _alunoBL.UpdateAluno(alunoUpdateReq);

            if (linhasAfedatas == 1)
            {
                return Ok(new Response { Message = "Usuario atualizado com sucesso" });
            }
            else
            {
                return BadRequest(new { message = "Erro ao atualizar o cadastro de aluno, contate o administrador" });
            }
        }

        //Função para recuperar o Aluno
        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(AlunoResponse), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetById(int id)
        {
            // Acesso a camada de persistencia
            var alunoResponse = _alunoBL.GetById(id);

            if (alunoResponse != null)
            {
                return Ok(alunoResponse);
            }
            else
            {
                return NotFound(new Response { Message = "Nenhum usuario foi encontrado." });
            }
        }

        //Função para recuperar o Aluno
        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(IEnumerable<AlunoResponse>), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetAll()
        {
            var alunoResponse = _alunoBL.GetAllAlunos();

            if (alunoResponse.Any()) // Any - Retorna true ou false
            {
                return Ok(alunoResponse);
            }
            else
            {
                return NotFound(new Response { Message = "Nenhum aluno foi encontrado." });
            }
        }

        //Função para Excluir o Aluno
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(AlunoEntity), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult Delete(int id)
        {
            var linhasAfetadas = _alunoBL.DeleteAluno(id);

            if (linhasAfetadas == 1)
            {
                return Ok(new { message = "aluno excluido com sucesso." });
            }
            else
            {
                return BadRequest(new { message = "erro ao excluir o aluno, contate o administrador." });
            }
        }

    }
}
