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
    [Route("api/unidades")]
    public class UnidadesController : Controller
    {
        private readonly UnidadeBL _unidadeBL;
        public UnidadesController(UnidadeBL unidadeBL)
        {
            _unidadeBL = unidadeBL;
        }

        /// <summary>
        /// Cadastro Unidade
        /// </summary>
        /// <param name="unidadeRequest">JSON</param>
        /// <returns>JSON</returns>

        #region Método POST - Inserir Unidade
        [HttpPost]
        [Route("insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] UnidadeRequest unidadeRequest)
        {
            var idUnidade = _unidadeBL.InsertUnidade(unidadeRequest);

            return CreatedAtAction(nameof(GetById), new { id = idUnidade }, unidadeRequest);
        }
        #endregion



        #region Método GET - Buscar por Unidade
        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(typeof(UnidadeResponse), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetById(int id)
        {
            var unidadeResponse = _unidadeBL.GetById(id);

            if (unidadeResponse != null)
            {
                return Ok(unidadeResponse);
            }
            else
            {
                return NotFound(new Response { Message = "Nenhuma unidade encontrada" });
            }
        }

        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(IEnumerable<UnidadeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var unidadeResponse = _unidadeBL.GetAllUnidades();

            if (unidadeResponse.Any())
            {
                return Ok(unidadeResponse);
            }
            else
            {
                return NotFound(new Response { Message = "Nenhuma unidade foi encontrada." });
            }
        }
        #endregion



        #region Método UPDATE - Atualizar Unidade
        [HttpPut]
        [Route("update")]

        public IActionResult Put([FromBody] UnidadeRequest unidadeUpdateReq)
        {
            if(unidadeUpdateReq.IdUnidade.GetValueOrDefault(0) <= 0)
            {
                return BadRequest(new { message = "Unidade não encontrada" });
            }

            var linhasAfetadas = _unidadeBL.UpdateUnidade(unidadeUpdateReq);

            if (linhasAfetadas == 1)
            {
                return Ok(new Response { Message = "Unidade atualizada com sucesso" });
            }
            else
            {
                return BadRequest(new { message = "Erro ao atualizar o cadastro de unidade, contate o administrador" });
            }
        }
        #endregion



        #region Método Delete - Deleta Unidade - Delete
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(UnidadeEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var linhasAfetadas = _unidadeBL.DeleteUnidade(id);

            if (linhasAfetadas == 1 )
            {
                return Ok(new { message = "Unidade excluida com sucesso." });
            }
            else
            {
                return BadRequest(new { Message = "Erro ao excluir a unidade, contate o administrador." });
            }
        }
        #endregion

    }
}
