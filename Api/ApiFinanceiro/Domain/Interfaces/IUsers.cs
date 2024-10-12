using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;

namespace ApiFinanceiro.Domain.Interfaces
{
    public interface IUsers
    {

        void Incluir (User user);
        void Atualizar(User user);
        User? Logar(UserDTO userDTO);

    }
}