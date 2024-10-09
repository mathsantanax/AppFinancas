using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.DTOs;
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
        public bool Incluir(ValoresDTO valoresDTO)
        {
            if(valoresDTO.Tipo.Equals("Entrada"))
            {
                ValoresEntrada entrada = new ValoresEntrada{
                    Date = valoresDTO.Date,
                    Valor = valoresDTO.Valor,
                    Descricao = valoresDTO.Descricao,
                    Tipo = valoresDTO.Tipo,
                    Categoria = valoresDTO.Categoria,
                    IdUser = valoresDTO.IdUser,
                };
                if(entrada != null)
                {
                    _dbContexto.ValoresEntrada.Add(entrada);
                    _dbContexto.SaveChanges();
                    return true;
                }
                {
                    return false;
                }
            }
            else if(valoresDTO.Tipo.Equals("Saida"))
            {
                    ValoresSaida saida = new ValoresSaida{
                    Date = valoresDTO.Date,
                    Valor = valoresDTO.Valor,
                    Descricao = valoresDTO.Descricao,
                    Tipo = valoresDTO.Tipo,
                    Categoria = valoresDTO.Categoria,
                    IdUser = valoresDTO.IdUser,
                };
                if(saida != null)
                {
                    _dbContexto.ValoresSaida.Add(saida);
                    _dbContexto.SaveChanges();
                    return true;
                }
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Apagar(ValoresDTO valoresDTO)
        {
            if(valoresDTO.Tipo.Equals("Entrada"))
            {
                var entrada = _dbContexto.ValoresEntrada.FirstOrDefault(x => x.Id == valoresDTO.Id);
                if(entrada != null)
                {
                    _dbContexto.ValoresEntrada.Remove(entrada);
                    _dbContexto.SaveChanges();
                    return true;
                }
                {
                    return false;
                }
            }
            else if(valoresDTO.Tipo.Equals("Saida"))
            {
                var saida = _dbContexto.ValoresSaida.FirstOrDefault(x => x.Id == valoresDTO.Id);
                if(saida != null)
                {
                    _dbContexto.ValoresSaida.Remove(saida);
                    _dbContexto.SaveChanges();
                    return true;
                }
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<ValoresDTO> BuscaCategoria(ValoresDTO valoresDTO)
        {
            var buscaEntradaPorCategoria = _dbContexto.ValoresEntrada.Where(x => x.Categoria == valoresDTO.Categoria).ToList();
            var buscaSaidaPorCategoria = _dbContexto.ValoresSaida.Where(x => x.Categoria == valoresDTO.Categoria).ToList();

            List<ValoresDTO> valores = new List<ValoresDTO>();

            foreach(var entrada in buscaEntradaPorCategoria)
            {
                valores.Add(new ValoresDTO{
                    Date = entrada.Date,
                    Valor = entrada.Valor,
                    Descricao = entrada.Descricao,
                    Tipo = entrada.Tipo,
                    Categoria = entrada.Categoria,
                    IdUser = entrada.IdUser
                });
            }
            foreach(var saida in buscaSaidaPorCategoria)
            {
                valores.Add(new ValoresDTO{
                    Date = saida.Date,
                    Valor = saida.Valor,
                    Descricao = saida.Descricao,
                    Tipo = saida.Tipo,
                    Categoria = saida.Categoria,
                    IdUser = saida.IdUser
                });
            }
            return valores;
        }

        public List<ValoresDTO> BuscaPorData(ValoresDTO valoresDTO)
        {
            var buscaEntradaPorData = _dbContexto.ValoresEntrada.Where(x => x.Date == valoresDTO.Date).ToList();
            var buscaSaidaPorData = _dbContexto.ValoresSaida.Where(x => x.Date == valoresDTO.Date).ToList();

            List<ValoresDTO> valores = new List<ValoresDTO>();

            foreach(var entrada in buscaEntradaPorData)
            {
                valores.Add(new ValoresDTO{
                    Date = entrada.Date,
                    Valor = entrada.Valor,
                    Descricao = entrada.Descricao,
                    Tipo = entrada.Tipo,
                    Categoria = entrada.Categoria,
                    IdUser = entrada.IdUser
                });
            }
            foreach(var saida in buscaSaidaPorData)
            {
                valores.Add(new ValoresDTO{
                    Date = saida.Date,
                    Valor = saida.Valor,
                    Descricao = saida.Descricao,
                    Tipo = saida.Tipo,
                    Categoria = saida.Categoria,
                    IdUser = saida.IdUser
                });
            }
            return valores;
        }

        public List<ValoresDTO> BuscaTipo(ValoresDTO valoresDTO)
        {
            var buscaEntradaPorTipo = _dbContexto.ValoresEntrada.Where(x => x.Tipo == valoresDTO.Tipo).ToList();
            var buscaSaidaPorTipo = _dbContexto.ValoresSaida.Where(x => x.Tipo == valoresDTO.Tipo).ToList();

            List<ValoresDTO> valores = new List<ValoresDTO>();

            foreach(var entrada in buscaEntradaPorTipo)
            {
                valores.Add(new ValoresDTO{
                    Date = entrada.Date,
                    Valor = entrada.Valor,
                    Descricao = entrada.Descricao,
                    Tipo = entrada.Tipo,
                    Categoria = entrada.Categoria,
                    IdUser = entrada.IdUser
                });
            }
            foreach(var saida in buscaSaidaPorTipo)
            {
                valores.Add(new ValoresDTO{
                    Date = saida.Date,
                    Valor = saida.Valor,
                    Descricao = saida.Descricao,
                    Tipo = saida.Tipo,
                    Categoria = saida.Categoria,
                    IdUser = saida.IdUser
                });
            }
            return valores;
        }

        public ValoresDTO BuscarPorId(ValoresDTO valoresDTO)
        {
            if(valoresDTO.Tipo.Equals("Entrada"))
            {
                var entrada = _dbContexto.ValoresEntrada.Where(x => x.Id == valoresDTO.Id).FirstOrDefault();

                if(entrada == null) return valoresDTO;
                
                ValoresDTO valoresEntrada = new ValoresDTO{
                    Id = entrada.Id,
                    Date = entrada.Date,
                    Valor = entrada.Valor,
                    Descricao = entrada.Descricao,
                    Tipo = entrada.Tipo,
                    Categoria = entrada.Categoria,
                    IdUser = entrada.IdUser
                };
                return valoresEntrada;
            }
            else if(valoresDTO.Tipo.Equals("Saida"))
            {
                var Saida = _dbContexto.ValoresSaida.Where(x => x.Id == valoresDTO.Id).FirstOrDefault();
                if(Saida == null) return valoresDTO;
                ValoresDTO valoresSaida = new ValoresDTO{
                    Id = Saida.Id,
                    Date = Saida.Date,
                    Valor = Saida.Valor,
                    Descricao = Saida.Descricao,
                    Tipo = Saida.Tipo,
                    Categoria = Saida.Categoria,
                    IdUser = Saida.IdUser
                };
                return valoresSaida;
            }
            else
            {
                return valoresDTO;
            }
        }
    }
}