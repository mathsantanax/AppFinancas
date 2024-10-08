using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.Entities;

namespace ApiFinanceiro.Domain.Interfaces
{
    public interface IValores
    {
        List<ValoresEntrada> BuscarEntradasPorData(DateTime date);
        List<ValoresSaida> BuscarSaidasPorData(DateTime date);
        List<ValoresEntrada> BuscarEntradasPorTipo(ValoresEntrada valoresEntrada);
        List<ValoresSaida> BuscarSaidaPorTipo(ValoresSaida valoresSaida);
        List<ValoresEntrada> BuscarEntradaPorCategoria(ValoresEntrada valoresEntrada);
        List<ValoresSaida> BuscarSaidaPorCategoria(ValoresSaida valoresSaida);

        void IncluirEntrada(ValoresEntrada entrada);
        void IncluirSaida(ValoresSaida saida);
        void ApagarEntrada(ValoresEntrada entrada);
        void ApagarSaida(ValoresSaida saida);
    }
}