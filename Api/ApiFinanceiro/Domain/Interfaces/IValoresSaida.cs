using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Enuns;

namespace ApiFinanceiro.Domain.Interfaces
{
    public interface IValoresSaida
    {
        List<ValoresSaida> BuscarTodos(int? pagina = 1);
        List<ValoresSaida> BuscarPorData(DateTime date);
        List<ValoresSaida> BuscarPorTipo(Tipo tipo);
        List<ValoresSaida> BuscarPorCategoria(Categoria categoria);
        ValoresSaida? BuscarPorId(int id);
        void Incluir(ValoresSaida valores);
        void Apagar(ValoresSaida valores);
    }
}