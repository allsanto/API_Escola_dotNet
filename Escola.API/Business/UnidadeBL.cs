using AutoMapper;
using Escola.API.Data.Entities;
using Escola.API.Data.Repositories;
using Escola.API.Domain.Models.Request;
using Escola.API.Domain.Models.Response;
using Signa.Library.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Escola.API.Business
{
    public class UnidadeBL
    {
        private readonly IMapper _mapper;
        private readonly UnidadeRepository _unidadeRepository;

        public UnidadeBL(IMapper mapper, UnidadeRepository unidadeRepository)
        {
            _mapper = mapper;
            _unidadeRepository = unidadeRepository;
        }

        #region Método POST - Inserir Unidade - Create
        public int InsertUnidade(UnidadeRequest unidadeReq)
        {
            var unidadeEntity = _mapper.Map<UnidadeEntity>(unidadeReq);
            var idUnidade = _unidadeRepository.Insert(unidadeEntity);


            return idUnidade;
        }
        #endregion

        #region Pegar por ID - Read
        public UnidadeResponse GetById(int id)
        {
            var unidadeEntity = _unidadeRepository.GetUnidade(id);
            var unidadeResponse = _mapper.Map<UnidadeResponse>(unidadeEntity);

            return unidadeResponse;
        }

        public IEnumerable<UnidadeResponse> GetAllUnidades()
        {
            var unidadeEntities = _unidadeRepository.GetAllUnidades();
            var retornoUnidades = unidadeEntities.Select(x => _mapper.Map<UnidadeResponse>(x));

            return retornoUnidades;
        }
        #endregion

        #region Método PUT - Atualizar dados. - Update
        public int UpdateUnidade(UnidadeRequest unidadeUpdateReq)
        {
            var unidade = _unidadeRepository.GetUnidade(unidadeUpdateReq.IdUnidade.Value);

            if (unidade == null)
            {
                throw new SignaRegraNegocioException("Nenhuma unidade encontrada");
            }

            var unidadeEntity = _mapper.Map<UnidadeEntity>(unidadeUpdateReq);
            var linhasAfetadas = _unidadeRepository.Update(unidadeEntity);
            
            return linhasAfetadas;
        }
        #endregion

        #region Método Delete - Deleta uma Unidade - Delete
        public int DeleteUnidade(int idUnidade)
        {
            var unidadeEntity = _unidadeRepository.GetUnidade(idUnidade);

            if (unidadeEntity != null)
            {
                var linhasAfetadas = _unidadeRepository.Delete(idUnidade);
                return linhasAfetadas;
            }
            else
            {
                throw new SignaRegraNegocioException("Nenhuma unidade foi encontrada");
            }
        }
        #endregion
    }
}
