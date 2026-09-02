using Microsoft.EntityFrameworkCore;

public class MeuDiarioSENACContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Registro> Registros { get; set; }

    private readonly string connectionString =
        "server=localhost;database=MeuDIARIOSENAC;uid=root;pwd=#Senac2026;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Registros)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioID);
    }
}
