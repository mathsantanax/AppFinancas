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
        List<Valores> BuscarTodos(int? pagina = 1);
        List<Valores> BuscarPorData(DateTime date);
        List<Valores> BuscarPorTipo(Tipo tipo);
        List<Valores> BuscarPorCategoria(Categoria categoria);
        Valores? BuscarPorId(int id);
        void Incluir(Valores valores);
        void Apagar(Valores valores);
    }
}