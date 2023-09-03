using Mongo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLabBackend.Domain
{
    public class Empresa : MongoEntity
    {
        public string? Cnpj { get; set; }

        public string? NomeFantasia { get; set; }

        public string? RazaoSocial { get; set; }
    }
}
