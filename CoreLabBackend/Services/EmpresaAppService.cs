using Cache;
using DomainLabBackend.Domain;
using Mongo.Repository;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using static DomainLabBackend.Mapper.Mappping;
using Newtonsoft.Json.Linq;

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

        public async Task<List<EmpresaDto>> SearchEnterprisesDtoAsync(string term)
        {
            List<Empresa>? empresas =
                await Repository?.TakeListAsync(x => x.NomeFantasia!.Contains(term) ||
                                                x.RazaoSocial!.Contains(term))
                                                .ContinueWith(x => x.Result.ToList());

            if (empresas != null &&
                empresas.Count > 0)
            {
                return mappingEmpresaDto.Mapper.Map<List<Empresa>, List<EmpresaDto>>(empresas);
            }

            return new List<EmpresaDto>();
        }


        public async Task<EmpresaDto?> GetEnterpriseDtoAsync(string id)
        {
            string? stringEmpresaCache = Redis.TakeInstance().ReadCache(id);

            if (string.IsNullOrWhiteSpace(stringEmpresaCache))
            {
                EmpresaDto? empresaDto = new EmpresaDto();

                try
                {
                    JObject? jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(stringEmpresaCache);
                    Newtonsoft.Json.JsonReader jsonReader = jsonObject.CreateReader();
                    JsonSerializer serializer = new JsonSerializer();
                    empresaDto = serializer.Deserialize<EmpresaDto>(jsonReader);
                }
                catch (JsonException)
                {
                    return new EmpresaDto();
                }

                return empresaDto;
            }

            Empresa? empresa = await Repository?.TakeListAsync(x => x.Id == id).ContinueWith(x => x.Result.FirstOrDefault());

            if (empresa != null)
            {
                return mappingEmpresaDto.Mapper.Map<Empresa, EmpresaDto>(empresa);
            }

            return new EmpresaDto();
        }

        public async Task<int> GetCountEnterprisesAsync()
        {
            string? contador = await Redis.TakeInstance().ReadCacheAsync("Contador");

            if (contador != null)
            {
                bool conversao = int.TryParse(contador, out int contadorInt);

                if (conversao)
                {
                    return contadorInt;
                }
            }

            return await Repository?.TakeListAsync().ContinueWith(x => x.Result.Count());
        }

        public async Task<List<EmpresaDto>>? GetEnterprisesDtoAsync()
        {
            List<Empresa>? empresas = await Repository?.TakeListAsync().ContinueWith(x => x.Result.ToList());

            if (empresas != null &&
                empresas.Count() > 0)
            {
                return mappingEmpresaDto.Mapper.Map<List<Empresa>, List<EmpresaDto>>(empresas);
            }

            return new List<EmpresaDto>();
        }

        public async Task<Empresa> InsertEnterpriseAsync(EmpresaDto empresadto)
        {
            Empresa empresa = new Empresa()
            {
                Cnpj = empresadto.Cnpj,
                NomeFantasia = empresadto.NomeFantasia,
                RazaoSocial = empresadto.RazaoSocial
            };

            await Repository.InsertAsync(empresa);
            empresadto.Id = empresa.Id;
            await Redis.TakeInstance().InsertCacheAsync(empresa.Id, empresadto, new TimeSpan(5, 0, 0));
            string? contador = await Redis.TakeInstance().ReadCacheAsync("Contador");

            if (contador != null)
            {
                bool conversao = int.TryParse(contador, out int contadorInt);

                if (conversao)
                {
                    contadorInt++;
                    Redis.TakeInstance().InsertCache(contador, contadorInt.ToString(), new TimeSpan(5, 0, 0));
                }
            }

            return empresa;
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

                if (validaCnpj && validateRazaoSocial && validateNomeFantasia) { return true; }

                return false;
            }

            return false;
        }

        public bool ValidateCnpj(string cnpj)
        {
            cnpj = Regex.Replace(cnpj, @"[^\d]", "");

            if (cnpj.Length != 14)
                return false;

            int[] peso1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma1 = 0;
            for (int i = 0; i < 12; i++)
                soma1 += int.Parse(cnpj[i].ToString()) * peso1[i];
            int resto1 = soma1 % 11;
            int digitoVerificador1 = resto1 < 2 ? 0 : 11 - resto1;

            int[] peso2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma2 = 0;
            for (int i = 0; i < 13; i++)
                soma2 += int.Parse(cnpj[i].ToString()) * peso2[i];
            int resto2 = soma2 % 11;
            int digitoVerificador2 = resto2 < 2 ? 0 : 11 - resto2;

            return int.Parse(cnpj[12].ToString()) == digitoVerificador1 && int.Parse(cnpj[13].ToString()) == digitoVerificador2;
        }

        public bool ValidateRazaoSocial(string? razaoSocial)
        {
            if (string.IsNullOrWhiteSpace(razaoSocial)) return false;

            return true;
        }

        public bool ValidateNomeFantasia(string? nomeFantasia)
        {
            if (string.IsNullOrWhiteSpace(nomeFantasia)) return false;

            return true;
        }
    }
}
