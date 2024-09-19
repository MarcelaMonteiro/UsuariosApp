using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models.Entities;
using UsuariosApp.Infra.Data.Mappings;

namespace UsuariosApp.Infra.Data.Contexts
{
    /// <summary>
    /// Classe para a conexão do EF com o banco de dados
    /// </summary>

    public class DataContext : DbContext
    {
       


        /// <summary>
        /// Método para definir o tipo de conexão de banco de dados
        /// </summary>      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

           optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDUsuariosApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        
        }

        /// <summary>
        /// Método para adicionarmos as classes de mapeamento das entidades
        /// </summary>
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new UsuarioPerfilMap());
        }
    }
}
