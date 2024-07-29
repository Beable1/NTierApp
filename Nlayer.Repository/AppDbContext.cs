using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
	{

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Product { get; set; }
        
        public DbSet<ProductFeature> ProductFeatures { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach( var item in ChangeTracker.Entries())
            {
                if(item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                        {
                                entityReference.CreatedTime= DateTime.Now;
                                break;                        
                        }
                        case EntityState.Modified:
                        {
                                Entry(entityReference).Property(x => x.CreatedTime).IsModified = false;

                            entityReference.UpdatedTime= DateTime.Now; 
                            break;
                        }


                    }

                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedTime = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.UpdatedTime = DateTime.Now;
                                break;
                            }


                    }

                }
            }

            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);

		}
	}
}
