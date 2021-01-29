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
    public class ProfessorBL
    {
        private readonly IMapper _mapper;
        private readonly ProfessorRepository _professorRepository;

        public ProfessorBL(IMapper mapper, ProfessorRepository professorRepository)
        {
            _mapper = mapper;
            _professorRepository = professorRepository;
        }

        public int InsertProfessor(ProfessorRequest professorReq)
        {
            VerificaSeExisteUnidade(professorReq.IdUnidade);

            var professorEntity = _mapper.Map<ProfessorEntity>(professorReq);
            var idProfessor = _professorRepository.Insert(professorEntity);
            
            return idProfessor;
        }

        public void VerificaSeExisteUnidade(int idProfessor)
        {
            var status = _professorRepository.GetStatusById(idProfessor);
            if (status != 1)
            {
                throw new SignaRegraNegocioException("Professor informado não existe.");
            }
        }

        public ProfessorResponse GetById(int id)
        {
            var professorEntity = _professorRepository.GetProfessor(id);
            var professorResponse = _mapper.Map<ProfessorResponse>(professorEntity);

            return professorResponse;
        }
        public IEnumerable<ProfessorResponse> GetAllProfessor()
        {
            var professorEntity = _professorRepository.GetAllProfessor();
            var professorResponse = professorEntity.Select(x => _mapper.Map<ProfessorResponse>(x));

            return professorResponse;
        }

        public int UpdateProfessor(ProfessorRequest professorUpdateReq)
        {
            var professor = _professorRepository.GetProfessor(professorUpdateReq.IdProfessor.Value);

            if (professor == null)
            {
                throw new SignaRegraNegocioException("Nenhum professor encontrado.");
            }

            var professorEntity = _mapper.Map<ProfessorEntity>(professorUpdateReq);
            var linhasAfetadas = _professorRepository.Update(professorEntity);

            return linhasAfetadas;
        }

        public int DeletaProfessor(int idProfessor)
        {
            var professorEntity = _professorRepository.GetProfessor(idProfessor);

            if (professorEntity != null)
            {
                var linhasAfetadas = _professorRepository.Delete(idProfessor);
                return linhasAfetadas;
            }
            else
            {
                throw new SignaRegraNegocioException("Nenhum professor foi encontrado.");
            }
        }
    }
}
