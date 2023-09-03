using Newtonsoft.Json;

namespace DomainLabBackend.Domain
{
    public class EmpresaDto
    {
        [JsonProperty("Id")]
        public string? Id { get; set; }

        [JsonProperty("Cnpj")]
        public string? Cnpj { get; set; }

        [JsonProperty("NomeFantasia")]
        public string? NomeFantasia { get; set; }

        [JsonProperty("RazaoSocial")]
        public string? RazaoSocial { get; set; }
    }
}
