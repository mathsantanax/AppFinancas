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
    public class ValoresEntradaService : IValores
    {
        private readonly DbContexto dbContexto;

        public ValoresEntradaService(DbContexto db)
        {
            this.dbContexto = db;
        }
        public void Apagar(Valores valores)
        {
            dbContexto.ValoresEntrada.Remove(valores);
            dbContexto.SaveChanges();
        }

        public List<Valores> BuscarPorCategoria(Categoria categoria)
        {  
            var categoriaStr = categoria.ToString();
            return dbContexto.ValoresEntrada.Where(x => x.Categoria == categoriaStr).ToList();
        }

        public List<Valores> BuscarPorTipo(Tipo tipo)
        {
            var tipoStr = tipo.ToString();
            return dbContexto.ValoresEntrada.Where(x => x.Tipo == tipoStr).ToList();
        }

        public List<Valores> BuscarPorData(DateTime date)
        {
            return dbContexto.ValoresEntrada.Where(x => x.Date == date).ToList();
        }

        public void Incluir(Valores valores)
        {
            dbContexto.ValoresEntrada.Add(valores);
            dbContexto.SaveChanges();
        }

        public Valores? BuscarPorId(int id)
        {
            return dbContexto.ValoresEntrada.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Valores> BuscarTodos(int? pagina = 1)
        {
            var query = dbContexto.ValoresEntrada.AsQueryable();

            int itensPorPagina = 10;
            if(pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }

            return query.ToList();
        }
    }
}