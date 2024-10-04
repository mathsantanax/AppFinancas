using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Enuns;
using ApiFinanceiro.Domain.Interfaces;
using ApiFinanceiro.Infraestrutura.Db;

namespace ApiFinanceiro.Domain.Servicos
{
    public class ValoresSaidaService : IValoresSaida
    {
        private readonly DbContexto dbContexto;

        public ValoresSaidaService(DbContexto db)
        {
            this.dbContexto = db;
        }
        public void Apagar(ValoresSaida valores)
        {
            dbContexto.ValoresSaida.Remove(valores);
            dbContexto.SaveChanges();
        }

        public List<ValoresSaida> BuscarPorCategoria(Categoria categoria)
        {  
            var categoriaStr = categoria.ToString();
            return dbContexto.ValoresSaida.Where(x => x.Categoria == categoriaStr).ToList();
        }

        public List<ValoresSaida> BuscarPorTipo(Tipo tipo)
        {
            var tipoStr = tipo.ToString();
            return dbContexto.ValoresSaida.Where(x => x.Tipo == tipoStr).ToList();
        }

        public List<ValoresSaida> BuscarPorData(DateTime date)
        {
            return dbContexto.ValoresSaida.Where(x => x.Date == date).ToList();
        }

        public void Incluir(ValoresSaida valores)
        {
            dbContexto.ValoresSaida.Add(valores);
            dbContexto.SaveChanges();
        }

        public ValoresSaida? BuscarPorId(int id)
        {
            return dbContexto.ValoresSaida.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<ValoresSaida> BuscarTodos(int? pagina = 1)
        {
            var query = dbContexto.ValoresSaida.AsQueryable();

            int itensPorPagina = 10;
            if(pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }

            return query.ToList();
        }
    }
}