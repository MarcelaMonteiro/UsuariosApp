using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models.Entities;

namespace UsuariosApp.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //nome da tabela
            builder.ToTable("USUARIO");
                

                //chave primária 
                builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("ID");

            //mapeamento do campo 'Nome'
            builder.Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();
                
            //mapeamento do campo 'Email'
            builder.Property(u=> u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(50)
                .IsRequired();

            //mapeamento do email como campo único
            builder.HasIndex(u => u.Email)
                .IsUnique();

            //mapeamento do campo 'Senha'
            builder.Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(100)
                .IsRequired();



        }
    }
}
