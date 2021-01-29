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
    [Route("api/professores")]
    public class ProfessoresController : Controller
    {
        private readonly ProfessorBL _professorBL;
        public ProfessoresController(ProfessorBL professorBL)
        {
            _professorBL = professorBL;
        }
    

    /// <summary>
    /// Cadastrar Professor
    /// </summary>
    /// <param name="professorRequest">JSON</param>
    /// <returns>JSON</returns>
    // Função para cadastro de Professor
    [HttpPost]
    [Route("insert")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] ProfessorRequest professorRequest)
        {
            var idProfessor = _professorBL.InsertProfessor(professorRequest);
            return CreatedAtAction(nameof(GetById), new { id = idProfessor }, professorRequest);
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(ProfessorResponse), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetById(int id)
        {
            var professorResponse = _professorBL.GetById(id);

            if (professorResponse != null)
            {
                return Ok(professorResponse);
            }
            else
            {
                return NotFound(new Response { Message = "Nenhum usuario foi encontrado." });
            }
        }

        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(IEnumerable<ProfessorResponse>), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetAll()
        {
            var professorResponse = _professorBL.GetAllProfessor();

            if (professorResponse.Any())
            {
                return Ok(professorResponse);
            }
            else
            {
                return NotFound(new Response { Message = "Nenhum professor foi encontrado" });
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Put([FromBody] ProfessorRequest professorUpdateReq)
        {
            if (professorUpdateReq.IdProfessor.GetValueOrDefault(0) <= 0)
            {
                return BadRequest(new { message = "Informe um professor." });
            }

            var linhasAfetadas = _professorBL.UpdateProfessor(professorUpdateReq);

            if (linhasAfetadas == 1)
            {
                return Ok(new Response { Message = "Professor atualizado com sucesso." });
            }
            else
            {
                return BadRequest(new { message = "Erro ao atualizar o professor, contate o administrador" });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(ProfessorEntity), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult Delete(int id)
        {
            var linhasAfetadas = _professorBL.DeletaProfessor(id);

            if (linhasAfetadas == 1)
            {
                return Ok(new { message = "Professor excluido com sucesso." });
            }
            else
            {
                return BadRequest(new { message = "Erro ao excluir o professor, contate o administrador." });
            }
        }



    }
}
