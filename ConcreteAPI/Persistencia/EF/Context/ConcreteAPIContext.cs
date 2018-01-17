using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Persistence.EF.EntitiesConfig;

namespace ConcreteAPI.Persistence.EF.Context
{
    public class ConcreteAPIContext: DbContext
    {
        public ConcreteAPIContext()
            : base("ConcreteAPIConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        //DbSet das entities
        public DbSet<Phone> Phones { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove algumas convensões padrões do modelo
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Usar varchar quando a variável for do tipo String
            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("varchar"));

            //Usar as configurações(fluent api) para definir o Banco de Dados
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new PhoneConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
