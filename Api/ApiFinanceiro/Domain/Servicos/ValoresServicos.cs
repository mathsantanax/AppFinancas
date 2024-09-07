using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Enuns;
using ApiFinanceiro.Domain.Interfaces;
using ApiFinanceiro.Infraestrutura.Db;

namespace ApiFinanceiro.Domain.Servicos
{
    public class ValoresServicos : IValores
    {
        private readonly DbContexto dbContexto;

        public ValoresServicos(DbContexto db)
        {
            this.dbContexto = db;
        }
        public void Apagar(Valores valores)
        {
            dbContexto.Valores.Remove(valores);
            dbContexto.SaveChanges();
        }

        public List<Valores> BuscarPorCategoria(Categoria categoria)
        {  
            var categoriaStr = categoria.ToString();
            return dbContexto.Valores.Where(x => x.Categoria == categoriaStr).ToList();
        }

        public List<Valores> BuscarPorTipo(Tipo tipo)
        {
            var tipoStr = tipo.ToString();
            return dbContexto.Valores.Where(x => x.Tipo == tipoStr).ToList();
        }

        public List<Valores> BuscarPorData(DateTime date)
        {
            return dbContexto.Valores.Where(x => x.Date == date).ToList();
        }

        public void Incluir(Valores valores)
        {
            dbContexto.Valores.Add(valores);
            dbContexto.SaveChanges();

        }
    }
}