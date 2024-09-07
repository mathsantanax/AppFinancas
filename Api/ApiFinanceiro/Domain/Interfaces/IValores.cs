using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Enuns;

namespace ApiFinanceiro.Domain.Interfaces
{
    public interface IValores
    {
        List<ValoresDTO> BuscarPorData(DateTime date);
        List<ValoresDTO> BurcarPorTipo(Tipo tipo);
        List<string> BurcarPorCategoria(Categoria categoria);
        void Incluir(Valores valores);
        void Apagar(Valores valores);
    }
}