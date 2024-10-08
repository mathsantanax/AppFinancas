using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Interfaces;
using ApiFinanceiro.Infraestrutura.Db;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceiro.Domain.Servicos
{
    public class ValoresService : IValores
    {

        private readonly DbContexto _dbContexto;

        public ValoresService(DbContexto contexto)
        {
            _dbContexto = contexto;
        }
        public void ApagarEntrada(ValoresEntrada entrada)
        {
            throw new NotImplementedException();
        }

        public void ApagarSaida(ValoresSaida saida)
        {
            throw new NotImplementedException();
        }

        public List<ValoresEntrada> BuscarEntradaPorCategoria(ValoresEntrada valoresEntrada)
        {
            throw new NotImplementedException();
        }

        public List<ValoresEntrada> BuscarEntradasPorData(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<ValoresEntrada> BuscarEntradasPorTipo(ValoresEntrada valoresEntrada)
        {
            throw new NotImplementedException();
        }

        public List<ValoresSaida> BuscarSaidaPorCategoria(ValoresSaida valoresSaida)
        {
            throw new NotImplementedException();
        }

        public List<ValoresSaida> BuscarSaidaPorTipo(ValoresSaida valoresSaida)
        {
            throw new NotImplementedException();
        }

        public List<ValoresSaida> BuscarSaidasPorData(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void IncluirEntrada(ValoresEntrada entrada)
        {
            throw new NotImplementedException();
        }

        public void IncluirSaida(ValoresSaida saida)
        {
            throw new NotImplementedException();
        }
    }
}