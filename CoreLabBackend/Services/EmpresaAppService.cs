using DomainLabBackend.Domain;
using Mongo.Repository;
using System.Text.RegularExpressions;
using static DomainLabBackend.Mapper.Mappping;

namespace CoreLabBackend.Services
{
    public class EmpresaAppService
    {
        public IMongoRepository<Empresa>? Repository { get; }
        private MappingEmpresaDto mappingEmpresaDto;

        public EmpresaAppService()
        {
            Repository = new MongoRepository<Empresa>("labbackend", "Empresa");
            mappingEmpresaDto = new MappingEmpresaDto();
        }

        public List<Empresa>? GetAll()
        {
            return Repository?.TakeList().ToList();
        }

        public List<EmpresaDto> SearchEnterprisesDto(string term)
        {
            List<Empresa>? empresas = Repository?.TakeList(x => x.NomeFantasia!.Contains(term) || x.RazaoSocial!.Contains(term)).ToList();

            if (empresas != null &&
                empresas.Count > 0)
            {
                return mappingEmpresaDto.Mapper.Map<List<Empresa>, List<EmpresaDto>>(empresas);
            }

            return new List<EmpresaDto>();
        }


        public EmpresaDto? GetEnterpriseDto(string id)
        {
            Empresa? empresa = Repository?.TakeList(x => x.Id == id).FirstOrDefault();

            if (empresa != null)
            {
                return mappingEmpresaDto.Mapper.Map<Empresa, EmpresaDto>(empresa);
            }

            return null;
        }

        public List<EmpresaDto>? GetEnterprisesDto()
        {
            List<Empresa>? empresas = Repository?.TakeList().ToList();

            if (empresas != null &&
                empresas.Count > 0)
            {
                return mappingEmpresaDto.Mapper.Map<List<Empresa>, List<EmpresaDto>>(empresas);
            }

            return new List<EmpresaDto>();
        }

        public Empresa InsertEnterprise(EmpresaDto empresadto)
        {
            Empresa empresa = new Empresa()
            {
                Cnpj = empresadto.Cnpj,
                NomeFantasia = empresadto.NomeFantasia,
                RazaoSocial = empresadto.RazaoSocial
            };

            Repository?.Insert(empresa);

            return empresa;
        }

        public void UpdateEnterprise(Empresa empresa)
        {
            Empresa? empresaDatabase = Repository?.TakeList(x => x.Id == empresa.Id).FirstOrDefault();

            if (empresaDatabase != null)
            {
                empresaDatabase = empresa;
                Repository?.Update(empresaDatabase);
            }
        }

        public bool ValidateEmpresa(EmpresaDto empresaDto)
        {
            if (empresaDto != null &&
                empresaDto.Cnpj != null &&
                empresaDto.RazaoSocial != null &&
                empresaDto.NomeFantasia != null)
            {
                bool validaCnpj = ValidateCnpj(empresaDto.Cnpj);
                bool validateRazaoSocial = ValidateRazaoSocial(empresaDto.RazaoSocial);
                bool validateNomeFantasia = ValidateNomeFantasia(empresaDto.NomeFantasia);

                if (validaCnpj && validateRazaoSocial  && validateNomeFantasia) { return true; }

                return false;
            }

            return false;
        }

        public bool ValidateCnpj(string cnpj)
        {
            cnpj = Regex.Replace(cnpj, @"[^\d]", "");

            if (cnpj.Length != 14)
                return false;

            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * (5 - i);
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpj[i].ToString()) * (6 - i);
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cnpj[12].ToString()) == digitoVerificador1 && int.Parse(cnpj[13].ToString()) == digitoVerificador2;
        }

        public bool ValidateRazaoSocial(string? razaoSocial)
        {
            if (string.IsNullOrWhiteSpace(razaoSocial)) return false;

            return true;
        }

        public bool ValidateNomeFantasia(string? nomeFantasia)
        {
            if(string.IsNullOrWhiteSpace(nomeFantasia)) return false;

            return true;
        }            
    }
}
