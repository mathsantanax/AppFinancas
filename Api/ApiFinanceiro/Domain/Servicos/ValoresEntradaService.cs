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
    public class ValoresEntradaService : IValoresEntrada
    {
        private readonly DbContexto dbContexto;

        public ValoresEntradaService(DbContexto db)
        {
            this.dbContexto = db;
        }
        public void Apagar(ValoresEntrada valores)
        {
            dbContexto.ValoresEntrada.Remove(valores);
            dbContexto.SaveChanges();
        }

        public List<ValoresEntrada> BuscarPorCategoria(Categoria categoria)
        {  
            var categoriaStr = categoria.ToString();
            return dbContexto.ValoresEntrada.Where(x => x.Categoria == categoriaStr).ToList();
        }

        public List<ValoresEntrada> BuscarPorTipo(Tipo tipo)
        {
            var tipoStr = tipo.ToString();
            return dbContexto.ValoresEntrada.Where(x => x.Tipo == tipoStr).ToList();
        }

        public List<ValoresEntrada> BuscarPorData(DateTime date)
        {
            return dbContexto.ValoresEntrada.Where(x => x.Date == date).ToList();
        }

        public void Incluir(ValoresEntrada valores)
        {
            dbContexto.ValoresEntrada.Add(valores);
            dbContexto.SaveChanges();
        }

        public ValoresEntrada? BuscarPorId(int id)
        {
            return dbContexto.ValoresEntrada.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<ValoresEntrada> BuscarTodos(int? pagina = 1)
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