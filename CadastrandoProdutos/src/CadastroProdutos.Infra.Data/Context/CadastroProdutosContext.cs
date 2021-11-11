using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroProdutos.Infra.Data.Context
{
    public class CadastroProdutosContext : DbContext
    {
        public CadastroProdutosContext(DbContextOptions<CadastroProdutosContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadastroProdutosContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);


        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now.Date;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataCadastro").IsModified = false;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
