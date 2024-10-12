using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.DTOs;
using ApiFinanceiro.Domain.Entities;
using ApiFinanceiro.Domain.Interfaces;
using ApiFinanceiro.Infraestrutura.Db;

namespace ApiFinanceiro.Domain.Servicos
{
    public class UsersService : IUsers
    {

        private readonly DbContexto dbContexto;

        public UsersService(DbContexto db)
        {
            this.dbContexto = db;
        }

        public void Atualizar(User user)
        {
            dbContexto.Usuario.Update(user);
            dbContexto.SaveChanges();
        }

        public void Incluir(User user)
        {
            dbContexto.Usuario.Add(user);
            dbContexto.SaveChanges();
        }

        public User? Logar(UserDTO userDTO)
        {
            var usuario = dbContexto.Usuario.Where(x => x.Email == userDTO.Email && x.Password == userDTO.Password).FirstOrDefault();
            return usuario;
        }
    }
}