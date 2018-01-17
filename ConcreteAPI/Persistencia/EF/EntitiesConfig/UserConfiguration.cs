using System.Data.Entity.ModelConfiguration;
using ConcreteAPI.Core.Constants;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Persistence.EF.Extensions;

namespace ConcreteAPI.Persistence.EF.EntitiesConfig
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            // Primary Key
            HasKey(x => x.Id);

            // Properties
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(UserProperties.NameMaxLen);

            Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(UserProperties.EmailMaxLen)
                .IsUniqueKey("unq_Users_Email");

            Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(UserProperties.PasswordMaxLen);

            Property(x => x.CreationDate)
                .IsRequired();

            Property(x => x.UpdateDate)
                .IsOptional();

            Property(x => x.LastLoginDate)
                .IsOptional();

            Property(x => x.Token)
                .IsOptional()
                .HasMaxLength(UserProperties.TokenMaxLen);

            // Relationships
            //Relacionamento de (N)Phones para (1)User
            HasMany(x => x.Phones)
                .WithRequired(x => x.User);
        }
    }

}