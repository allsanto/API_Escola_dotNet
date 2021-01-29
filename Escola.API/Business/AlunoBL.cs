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
    public class AlunoBL
    {
        private readonly IMapper _mapper;
        private readonly AlunoRepository _alunoRepository;

        public AlunoBL(IMapper mapper, AlunoRepository alunoRepository)
        {
            _mapper = mapper;
            _alunoRepository = alunoRepository;
        }

        public int DeleteAluno(int idAluno)
        {
            var alunoEntity = _alunoRepository.GetAluno(idAluno);

            if (alunoEntity != null)
            {
                var linhasAfetadas = _alunoRepository.Delete(idAluno);
                return linhasAfetadas;
            } 
            else
            {
                throw new SignaRegraNegocioException("Nenhum aluno foi encontrada");
            }
        }

        public int UpdateAluno(AlunoRequest alunoUpdateReq)
        {
            var aluno = _alunoRepository.GetAluno(alunoUpdateReq.IdAluno.Value);

            if (aluno == null)
            {
                throw new SignaRegraNegocioException("Nenhum aluno foi encotrado");
            }

            var alunoEntity = _mapper.Map<AlunoEntity>(alunoUpdateReq);

            var linhasAfedatas = _alunoRepository.Update(alunoEntity);

            return linhasAfedatas;

        }


        public int InsertAluno(AlunoRequest alunoReq)
        {
            VerificaSeUnidadeExiste(alunoReq.IdUnidade);

            var alunoEntity = _mapper.Map<AlunoEntity>(alunoReq);

            var idAluno = _alunoRepository.Insert(alunoEntity);

            return idAluno;
        }

        public IEnumerable<AlunoResponse> GetAllAlunos() 
        {
            var alunoEntities = _alunoRepository.GetAllAlunos(); // Recuperou todos objetos do tipo alunoEntiti
            var retornoAluno = alunoEntities.Select(x => _mapper.Map<AlunoResponse>(x)); // Fez o mapeamente para o alunoEtities para o AlunoResponse

            return retornoAluno;
        }

        public AlunoResponse GetById(int id)
        {
            var alunoEntity = _alunoRepository.GetAluno(id);
            var alunoResponse = _mapper.Map<AlunoResponse>(alunoEntity);

            return alunoResponse;
        }

        private void VerificaSeUnidadeExiste(int idUnidade)
        {   
            var status = _alunoRepository.GetStatusById(idUnidade);
            if (status != 1)
            {
                throw new SignaRegraNegocioException("Usuario informado já existe existe");
            }
        }
    }
}
