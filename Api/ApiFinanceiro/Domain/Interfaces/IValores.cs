using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace ApiFinanceiro.Domain.Interfaces
{
    public interface IValores
    {
        bool Apagar (ValoresDTO valoresDTO);
        bool Incluir (ValoresDTO valoresDTO);
        ValoresDTO BuscarPorId(ValoresDTO valoresDTO);
        List<ValoresDTO> BuscaPorData(ValoresDTO valoresDTO);
        List<ValoresDTO> BuscaTipo(ValoresDTO valoresDTO);
        List<ValoresDTO> BuscaCategoria(ValoresDTO valoresDTO);
    }
}