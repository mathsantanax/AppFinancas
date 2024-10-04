using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceiro.Infraestrutura.Db
{
    public class DbContexto : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbContexto(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Valores> Valores { get; set; }	= default!;
        public DbSet<Valores> ValoresEntrada { get; set; }	= default!;
        public DbSet<Valores> ValoresSaida { get; set; }	= default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var stringConexao = _configuration.GetConnectionString("SqlString")?.ToString();
                if(!string.IsNullOrEmpty(stringConexao))
                {
                    optionsBuilder.UseSqlServer(stringConexao);
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Valores>(entity =>
            {
                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Date)
                .HasColumnType("date");
            });
        }
    }
}