using Microsoft.EntityFrameworkCore;
using MinhasReceitas.Domain.Entidades;

namespace MinhasReceitas.Infrastructure.AcessoRepositorio;

public class MinhasReceitasContext : DbContext
{
	public MinhasReceitasContext(DbContextOptions<MinhasReceitasContext> options) : base(options){}

	public DbSet<Usuario> Usuarios { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(MinhasReceitasContext).Assembly);
	}
}
