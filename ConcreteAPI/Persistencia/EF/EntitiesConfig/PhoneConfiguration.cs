using System.Data.Entity.ModelConfiguration;
using ConcreteAPI.Core.Constants;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Persistence.EF.Extensions;

namespace ConcreteAPI.Persistence.EF.EntitiesConfig
{
    public class PhoneConfiguration : EntityTypeConfiguration<Phone>
    {
        public PhoneConfiguration()
        {
            ToTable("Phones");

            // Primary Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.UserId)
                .IsRequired()
                .IsUniqueKey("unq_Phone", order: 0);

            Property(x => x.Ddd)
                .IsRequired()
                .HasMaxLength(PhoneProperties.DddMaxLen)
                .IsUniqueKey("unq_Phone", order: 1);

            Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(PhoneProperties.PhoneMaxLen)
                .IsUniqueKey("unq_Phone", order: 2);

            // Relationships
            //Relacionamento de (1)User para (N)Phones
            HasRequired(x => x.User)
                .WithMany(x => x.Phones)
                .HasForeignKey(x => x.UserId);
        }
    }
}