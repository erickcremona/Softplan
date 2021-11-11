using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Softplan.CalculoPrecoVenda.Data.Contexto
{
    public class SoftplanDbContext : DbContext
    {
        public SoftplanDbContext(DbContextOptions<SoftplanDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SoftplanDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);

            modelBuilder.Inicializar();


        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCriacao").CurrentValue = DateTime.Now.Date;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataCriacao").IsModified = false;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
