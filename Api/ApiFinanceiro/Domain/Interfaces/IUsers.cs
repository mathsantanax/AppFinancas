using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.Entities;

namespace ApiFinanceiro.Domain.Interfaces
{
    public interface IUsers
    {
        void Incluir (User user);
        void Deletar (User user);
        void Atualizar(User user);
        List<User> ListarUsuarios();
        User? BuscarPorEmail(string email);
        User? BuscarPorId(int id);

    }
}