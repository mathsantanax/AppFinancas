using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.Enuns;

namespace ApiFinanceiro.Domain.DTOs
{
    public class ValoresDTO
    {
        public DateTime Date{ get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set;} = default!;
        public Tipo? Tipo{ get; set; } = default!;
        public Categoria? Categoria{ get; set; } = default!;
        public int IdUser{ get; set; } = default!;
    }
}