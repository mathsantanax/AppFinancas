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

        public List<string> BurcarPorCategoria(Categoria categoria)
        {   
            var lista = dbContexto.Valores.Where(x => x.Categoria == categoria).ToList();
            return lista;
        }

        public List<ValoresDTO> BurcarPorTipo(Tipo tipo)
        {
            throw new NotImplementedException();
        }

        public List<ValoresDTO> BuscarPorData(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Incluir(Valores valores)
        {
            dbContexto.Valores.Add(valores);
            dbContexto.SaveChanges();

        }
    }
}