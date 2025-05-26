using Microsoft.EntityFrameworkCore;
using RoomFlowApi.Domain.Entities;
using RoomFlowApi.Infra.Data.Configurations;

namespace RoomFlowApi.Infra.Data.Context
{
    public class RoomFlowContext : DbContext
    {
        public DbSet<Curso> CursoSet { get; set; }
        public DbSet<Sala> SalaSet { get; set; }
        public DbSet<Usuario> UsuarioSet { get; set; }
        public DbSet<Turma> TurmaSet { get; set; }
        public DbSet<Disciplina> DisciplinaSet { get; set; }
        public DbSet<Aula> AulaSet { get; set; }
        
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoConfiguration());
            modelBuilder.ApplyConfiguration(new SalaConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplinaConfiguration());
            modelBuilder.ApplyConfiguration(new AulaConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseMySql("server=localhost;database=DBRoomFlow;port=3306;uid=root", ServerVersion.AutoDetect("server=localhost;database=DBRoomFlow;port=3306;uid=root"));
                optionsBuilder.UseMySql("server=mysql.tccnapratica.com.br;database=tccnapratica09;port=3306;uid=tccnapratica09;password=Etec3h3", ServerVersion.AutoDetect("server=mysql.tccnapratica.com.br;database=tccnapratica09;port=3306;uid=tccnapratica09;password=Etec3h3"));
            }
        }
    }
}
