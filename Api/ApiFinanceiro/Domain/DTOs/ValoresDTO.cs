using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.Enuns;

namespace ApiFinanceiro.Domain.DTOs
{
    public class ValoresDTO
    {
        public int Id { get; set; }
        public DateTime Date{ get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set;} = default!;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public string Tipo{ get; set; } = default!;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public string Categoria{ get; set; } = default!;
        public int IdUser{ get; set; } = default!;
    }
}