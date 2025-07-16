using Microsoft.EntityFrameworkCore;
using casa_codigo_cursos.Models;


namespace casa_codigo_cursos.Context
{
    public class CasaCodigoDbContext : DbContext
    {
        public CasaCodigoDbContext(DbContextOptions<CasaCodigoDbContext>options): base(options) 
        {
        }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoCurso> CarritoCursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Inscripcion>().ToTable("Inscripcion");
            modelBuilder.Entity<Curso>().ToTable("Curso");
            modelBuilder.Entity<Carrito>().ToTable("Carrito");
            modelBuilder.Entity<CarritoCurso>().ToTable("CarritoCurso");
        }
    }
}
