using AutoMapper;
using Escola.API.Data.Entities;
using Escola.API.Domain.Models.Request;
using Escola.API.Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.API.Profiles
{
    public class UnidadeProfile : Profile
    {
        public UnidadeProfile()
        {
            CreateMap<UnidadeRequest, UnidadeEntity>().ReverseMap();
            CreateMap<UnidadeEntity, UnidadeResponse>().ReverseMap();
            CreateMap<UnidadeUpdateRequest, UnidadeEntity>().ReverseMap();
        }
    }
}
