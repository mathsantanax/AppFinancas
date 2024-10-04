using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Enuns;

namespace ApiFinanceiro.Domain.Interfaces
{
    public interface IValoresEntrada
    {
        List<ValoresEntrada> BuscarTodos(int? pagina = 1);
        List<ValoresEntrada> BuscarPorData(DateTime date);
        List<ValoresEntrada> BuscarPorTipo(Tipo tipo);
        List<ValoresEntrada> BuscarPorCategoria(Categoria categoria);
        ValoresEntrada? BuscarPorId(int id);
        void Incluir(ValoresEntrada valores);
        void Apagar(ValoresEntrada valores);
    }
}