using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public User? BuscarPorEmail(string email)
        {
            return dbContexto.Usuario.Where(x => x.Email == email).FirstOrDefault();
        }

        public User? BuscarPorId(int id)
        {
            return dbContexto.Usuario.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Deletar(User user)
        {
            dbContexto.Usuario.Remove(user);
            dbContexto.SaveChanges();
        }

        public void Incluir(User user)
        {
            dbContexto.Usuario.Add(user);
            dbContexto.SaveChanges();
        }

        public List<User> ListarUsuarios()
        {
            return dbContexto.Usuario.ToList();
        }
    }
}