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
    public class ValoresSaidaService : IValores
    {
        private readonly DbContexto dbContexto;

        public ValoresSaidaService(DbContexto db)
        {
            this.dbContexto = db;
        }
        public void Apagar(Valores valores)
        {
            dbContexto.ValoresSaida.Remove(valores);
            dbContexto.SaveChanges();
        }

        public List<Valores> BuscarPorCategoria(Categoria categoria)
        {  
            var categoriaStr = categoria.ToString();
            return dbContexto.ValoresSaida.Where(x => x.Categoria == categoriaStr).ToList();
        }

        public List<Valores> BuscarPorTipo(Tipo tipo)
        {
            var tipoStr = tipo.ToString();
            return dbContexto.ValoresSaida.Where(x => x.Tipo == tipoStr).ToList();
        }

        public List<Valores> BuscarPorData(DateTime date)
        {
            return dbContexto.ValoresSaida.Where(x => x.Date == date).ToList();
        }

        public void Incluir(Valores valores)
        {
            dbContexto.ValoresSaida.Add(valores);
            dbContexto.SaveChanges();
        }

        public Valores? BuscarPorId(int id)
        {
            return dbContexto.ValoresSaida.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Valores> BuscarTodos(int? pagina = 1)
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