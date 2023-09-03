using AutoMapper;
using DomainLabBackend.Domain;

namespace DomainLabBackend.Mapper
{
    public class Mappping
    {
        public class MappingDtoEmpresa : Profile
        {
            public MappingDtoEmpresa()
            {
                MapperConfiguration config = new(cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.CreateMap<EmpresaDto, Empresa>();
                });

                Mapper = config.CreateMapper();
            }

            public IMapper Mapper { get; }
        }

        public class MappingEmpresaDto : Profile
        {
            public MappingEmpresaDto()
            {
                MapperConfiguration config = new(cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.CreateMap<Empresa, EmpresaDto>();
                });

                Mapper = config.CreateMapper();
            }

            public IMapper Mapper { get; }
        }
    }
}
